using System;

namespace Downloader.Classes.Helper
{
    public class DownloadProgressChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Download speed.
        /// </summary>
        public double BytesPerSecond { get; }

        /// <summary>
        /// Already received bytes.
        /// </summary>
        public long BytesReceived { get; }

        /// <summary>
        /// Elapsed download time.
        /// </summary>
        public TimeSpan ElapsedTime { get; }

        /// <summary>
        /// Gets the percentage of the current download progress.
        /// </summary>
        public int ProgressPercentage { get; }

        /// <summary>
        /// Estimated remaining download time.
        /// </summary>
        public TimeSpan? RemainingTime { get; }

        /// <summary>
        /// Source file size.
        /// </summary>
        public long TotalBytes { get; }

        #endregion

        public DownloadProgressChangedEventArgs(long totalBytes, long bytesReceived, int progressPercentage, double bytesPerSecond, TimeSpan elapsedTime, TimeSpan? remainingTime)
        {
            BytesPerSecond = bytesPerSecond;
            BytesReceived = bytesReceived;
            ElapsedTime = elapsedTime;
            ProgressPercentage = progressPercentage;
            RemainingTime = remainingTime;
            TotalBytes = totalBytes;
        }
    }
}
