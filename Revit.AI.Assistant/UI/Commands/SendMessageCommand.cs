using Revit.AI.Assistant.Models;
using Revit.Async;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Revit.AI.Assistant.UI.Commands;
internal class SendMessageCommand : CommandBase
{
    private readonly string modelName = "gpt-oss:20b";
    private readonly AIPanelUserControlDataContext dataContext;
    private readonly Action<Exception> onException;
    private int parameter;
    private readonly Func<Task> callback;

    private readonly HttpClient httpClient = new()
    {
        Timeout = TimeSpan.FromMinutes(10)
    };


    public SendMessageCommand(AIPanelUserControlDataContext dataContext, Action<Exception> onException) : base(dataContext, onException)
    {
        this.dataContext = dataContext;
        this.onException = onException;
        callback = SendToLLM;
    }

    public override bool CanExecute(object parameter) => !IsExecuting;
    protected override async Task ExecuteAsync(object parameter)
    {
        this.parameter = Convert.ToInt16(parameter);
        await callback();
    }

    private async Task SendToLLM()
    {
        string message = dataContext.Message.Trim();
        if (string.IsNullOrEmpty(message)) return;

        dataContext.ChatHistory += $"You: {message}\n";
        dataContext.Message = string.Empty;

        var additionToMessage = "Dynamo code for Revit script. " +
            "Just code snippet no additional text. Revit document should be identified as uiapp.ActiveUIDocument.Document, because I am passin uiapp as argument." +
            "No need to use TransactionManager, just use Revit’s Transaction API t=Transaction(doc,\"Transaction name\"). " +
            "It should be Python code.";
        var reply = await SendToGptOssAsync(string.Concat(additionToMessage, " ", message));
        dataContext.ChatHistory += $"GPT-OSS: {reply}\n";

        await RevitTask.RunAsync(app =>
        {
            dataContext.AIService.RunPythonScript(app, reply);
        });
    }

    private async Task<string> SendToGptOssAsync(string message)
    {
        try
        {
            var request = new
            {
                model = modelName,
                prompt = message,
                stream = false
            };

            var json = JsonSerializer.Serialize(request);
            var response = await httpClient.PostAsync("http://localhost:11434/api/generate", new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("response").GetString() ?? "";
        }
        catch (TaskCanceledException ex)
        {
            return $"Timeout: The request took too long.\n{ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
