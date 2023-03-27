namespace Timer.Argument;

public class ElapsedTimeEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the arguments for the Elapsed method.
        /// </summary>
        public object Argument { get; }

    #endregion

    public ElapsedTimeEventArgs(object argument) =>
            Argument = argument;
}