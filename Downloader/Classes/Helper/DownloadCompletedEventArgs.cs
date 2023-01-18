using System;

namespace Downloader.Classes.Helper
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the average download speed.
        /// </summary>
        public double AverageDownloadSpeed { get; set; }

        /// <summary>
        /// Indicate whether the download has been cancelled by the user.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Gets the download elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Gets the error that cause the download to stop.
        /// </summary>
        public Exception Error { get; set; }

        public object UserState { get; set; }

        #endregion

        public DownloadCompletedEventArgs(double averageDownloadSpeed, TimeSpan elapsedTime, bool cancelled, Exception error, object userState)
        {
            AverageDownloadSpeed = averageDownloadSpeed;
            Cancelled = cancelled;
            ElapsedTime = elapsedTime;
            Error = error;
            UserState = userState;
        }
    }
}
