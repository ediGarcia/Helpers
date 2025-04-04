using System.Windows.Input;

#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8625

namespace HelperClasses.Classes;

public class RelayCommand(Action execute, Func<bool> canExecute = null) : ICommand
{
    #region Custom Events

    /// <summary>
    /// Notifies whether the "canExecute" property is changed.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    #endregion

    private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    #region Public Methods

    #region CanExecute
    /// <summary>
    /// Indicates whether the command's action can be executed.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter) =>
        canExecute?.Invoke() != false;
    #endregion

    #region Execute
    /// <summary>
    /// Executes the command's action.
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter) =>
        _execute();
    #endregion

    #endregion
}

public class RelayCommand<T>(Action<T> execute, Func<T, bool> canExecute = null) : ICommand
{
    #region Custom Events

    /// <summary>
    /// Notifies whether the "canExecute" property is changed.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    #endregion

    private readonly Action<T> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    #region Public Methods

    #region CanExecute
    /// <summary>
    /// Indicates whether the command's action can be executed.
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object? parameter) =>
        canExecute?.Invoke((T)parameter) != false;
    #endregion

    #region Execute
    /// <summary>
    /// Executes the command's action.
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object? parameter) =>
        _execute((T)parameter);
    #endregion

    #endregion
}
