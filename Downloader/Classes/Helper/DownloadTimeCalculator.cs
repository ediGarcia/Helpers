using System;

namespace Downloader.Classes.Helper
{
    public class DownloadTimeCalculator
    {
        #region Properties

        /// <summary>
        /// Gets the last calculated download speed.
        /// </summary>
        public double DownloadSpeed { get; private set; }

        /// <summary>
        /// Gets the elapsed download time.
        /// </summary>
        public TimeSpan ElapsedTime => DateTime.Now - StartTime;

        /// <summary>
        /// Gets the last calculated remaining time.
        /// </summary>
        public TimeSpan? RemainingDownloadTime { get; private set; }

        /// <summary>
        /// Start download time.
        /// </summary>
        public DateTime StartTime { get; }

        /// <summary>
        /// Remote file size.
        /// </summary>
        public long TotalBytes { get; set; }

        /// <summary>
        /// Gets and sets the interval (seconds) in which the download speed will be recalculated.
        /// </summary>
        public TimeSpan UpdateInterval { get; set; } = TimeSpan.FromSeconds(2);

        #endregion

        private long _bytesFromLastFetch;
        private DateTime _lastFetchTime;

        public DownloadTimeCalculator(DateTime startTime, long totalBytes)
        {
            _lastFetchTime = StartTime = startTime;
            TotalBytes = totalBytes;
        }

        #region Public Methods

        #region UpdateDownloadTimes
        /// <summary>
        /// Calculates the download speed in bytes per second (b/s) if the update time has been reached, otherwise returns the last calculated value.
        /// </summary>
        /// <param name="bytesReceived"></param>
        /// <returns></returns>
        public void UpdateDownloadTimes(long bytesReceived)
        {
            if (bytesReceived >= TotalBytes)
            {
                DownloadSpeed = 0;
                RemainingDownloadTime = TimeSpan.Zero;
                return;
            }

            DateTime fetchTime = DateTime.Now;
            TimeSpan fetchInterval = fetchTime - _lastFetchTime;
            if (fetchInterval >= UpdateInterval)
            {
                DownloadSpeed = (bytesReceived - _bytesFromLastFetch) / fetchInterval.TotalSeconds;

                if (TotalBytes > 0)
                    RemainingDownloadTime = TimeSpan.FromSeconds((TotalBytes - bytesReceived) / DownloadSpeed);

                _bytesFromLastFetch = bytesReceived;
                _lastFetchTime = fetchTime;
            }
        }
        #endregion

        #endregion
    }
}
