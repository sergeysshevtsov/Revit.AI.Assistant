using Revit.AI.Assistant.Models;
using Revit.AI.Assistant.Services;
using Revit.AI.Assistant.UI.Commands;
using System.Windows.Input;

namespace Revit.AI.Assistant.UI;
internal class AIPanelUserControlDataContext : ViewModelBase
{
    
    public AIService AIService { get; }
    public ICommand SendMessage { get; }
    public AIPanelUserControlDataContext()
    {
        AIService = new AIService();
        SendMessage = new SendMessageCommand(this, (exception) => StatusMessage = $"Error: {exception.Message}");
    }

    private string message;
    public string Message
    {
        get
        {
            return message;
        }
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    private string chatHistory;
    public string ChatHistory
    {
        get
        {
            return chatHistory;
        }
        set
        {
            chatHistory = value;
            OnPropertyChanged(nameof(ChatHistory));
        }
    }
}
