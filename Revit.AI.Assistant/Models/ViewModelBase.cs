using System.ComponentModel;

namespace Revit.AI.Assistant.Models;
public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private string resultMessage;
    public string ResultMessage
    {
        get
        {
            return resultMessage;
        }
        set
        {
            resultMessage = value;
            OnPropertyChanged(nameof(ResultMessage));
        }
    }

    private string statusMessage;
    public string StatusMessage
    {
        get
        {
            return statusMessage;
        }
        set
        {
            statusMessage = value;
            OnPropertyChanged(nameof(StatusMessage));
            OnPropertyChanged(nameof(HasStatusMessage));
        }
    }

    private string progressMessage;
    public string ProgressMessage
    {
        get
        {
            return progressMessage;
        }
        set
        {
            progressMessage = value;
            OnPropertyChanged(nameof(ProgressMessage));
        }
    }

    private bool isLoading;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public bool HasStatusMessage => !string.IsNullOrEmpty(StatusMessage);
}
