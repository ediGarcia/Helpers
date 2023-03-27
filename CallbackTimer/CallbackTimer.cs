using HelperMethods;
using Timer.Argument;
// ReSharper disable UnusedMember.Global
#pragma warning disable CS8600
#pragma warning disable CS8604
#pragma warning disable CS8618
#pragma warning disable CS8625

namespace Timer;

public class CallbackTimer
{
    #region Custom Events

    /// <summary>
    /// Thrown after every <see cref="Elapsed"/> execution.
    /// </summary>
    public event EventHandler<TimerCallbackEventArgs> Callback;

    /// <summary>
    /// Thrown one after each elapsed time.
    /// </summary>
    public event EventHandler<ElapsedTimeEventArgs> Elapsed;

    #endregion

    #region Properties

    /// <summary>
    /// Gets and sets a function that returns the argument for each timer execution.
    /// </summary>
    public Func<object> ArgumentFunction { get; set; }

    /// <summary>
    /// Gets and sets the delay time before the first timer execution.
    /// </summary>
    public int Delay { get; set; }

    /// <summary>
    /// Gets and sets the interval between the timer executions.
    /// </summary>
    public int Interval { get; set; }

    /// <summary>
    /// Gets and sets whether the current timer is enabled.
    /// </summary>
    public bool IsEnabled
    {
        get => _isEnabled;
        set
        {
            bool shouldStartTimer = value && !_isEnabled;
            _isEnabled = value;

            if (shouldStartTimer)
                Start();
        }
    }

    /// <summary>
    /// Gets and sets the data that should be sent to the callback method.
    /// </summary>
    public object CallbackData { get; set; }

    #endregion

    private bool _isEnabled;

    #region Private Methods

    #region Start
    /// <summary>
    /// Runs the timer function and the callback after each interval.
    /// </summary>
    private async void Start()
    {
await FunctionMethods.Wait(Delay);

        while (IsEnabled)
        {
            object parameter = ArgumentFunction?.Invoke();
            CallbackData = null;

            if (!IsEnabled)
                break;

            await Task.Run(() => Elapsed?.Invoke(this, new(parameter)));

            if (!IsEnabled)
                break;

            Callback?.Invoke(this, new(CallbackData));

            if (!IsEnabled)
                break;

            await FunctionMethods.Wait(Interval);
        }
    }
    #endregion

    #endregion
}
