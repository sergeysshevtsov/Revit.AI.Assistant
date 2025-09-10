using Revit.AI.Assistant.Models;
using Revit.AI.Assistant.Services;
using Revit.AI.Assistant.UI.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Revit.AI.Assistant.UI;
internal class AIPanelUserControlDataContext : ViewModelBase
{

    public AIService AIService { get; }
    public ICommand SendMessage { get; }
    public AIPanelUserControlDataContext()
    {
        LanguageModels = [
           Models.LanguageModels.model_gpt_oss_20b,
            Models.LanguageModels.model_gpt_oss_120b,
            Models.LanguageModels.model_llama3_2_vision
        ];
        LanguageModel = LanguageModels.First();

        AIService = new AIService();
        SendMessage = new SendMessageCommand(this, (exception) => StatusMessage = $"Error: {exception.Message}");
    }

    public ObservableCollection<string> languageModels;
    public ObservableCollection<string> LanguageModels
    {
        get => languageModels;
        set
        {
            languageModels = value;
            OnPropertyChanged(nameof(LanguageModels));
        }
    }

    private string languageModel;
    public string LanguageModel
    {
        get => languageModel;
        set
        {
            languageModel = value;
            OnPropertyChanged(nameof(LanguageModel));
        }
    }

    private string message;
    public string Message
    {
        get => message;
        set
        {
            message = value;
            OnPropertyChanged(nameof(Message));
        }
    }

    private string chatHistory;
    public string ChatHistory
    {
        get => chatHistory;
        set
        {
            chatHistory = value;
            OnPropertyChanged(nameof(ChatHistory));
        }
    }
}
