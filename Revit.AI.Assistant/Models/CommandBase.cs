using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Revit.AI.Assistant.Models;
public abstract class CommandBase : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly Action<Exception> onException;
    private readonly ViewModelBase viewModelBase;

    public CommandBase(ViewModelBase viewModelBase, Action<Exception> onException)
    {
        this.onException = onException;
        this.viewModelBase = viewModelBase;
    }

    public virtual bool CanExecute(object parameter)
    {
        return !IsExecuting;
    }

    public async void Execute(object parameter)
    {
        try
        {
            IsExecuting = true;
            await ExecuteAsync(parameter);
            IsExecuting = false;
        }
        catch (Exception exception)
        {
            if (IsExecuting)
                IsExecuting = false;
            onException?.Invoke(exception);
        }
    }

    protected abstract Task ExecuteAsync(object parameter);

    private bool isExecuting;
    public bool IsExecuting
    {
        get => isExecuting;
        set
        {
            isExecuting = value;
            viewModelBase.IsLoading = value;
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public void SetProgressMessage(string m)
    {
        viewModelBase.ProgressMessage = m;
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }

    protected void OnCanExecutedChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}
