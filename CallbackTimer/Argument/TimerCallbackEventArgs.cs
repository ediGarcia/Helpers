namespace Timer.Argument;

public class TimerCallbackEventArgs : EventArgs
{
    #region Properties

    /// <summary>
    /// Gets the returned data from the Elapsed method.
    /// </summary>
    public object ReturnedData { get; }

    #endregion

    public TimerCallbackEventArgs(object returnedData) =>
            ReturnedData = returnedData;
}
