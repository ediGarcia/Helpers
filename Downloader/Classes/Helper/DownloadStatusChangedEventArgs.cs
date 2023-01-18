using System;

namespace Downloader.Classes.Helper
{
    public class DownloadStatusChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// New download status.
        /// </summary>
        public DownloadStatus NewStatus { get; }

        /// <summary>
        /// Previous download status.
        /// </summary>
        public DownloadStatus PreviousStatus { get; }

        #endregion

        public DownloadStatusChangedEventArgs(DownloadStatus previousStatus, DownloadStatus newStatus)
        {
            NewStatus = newStatus;
            PreviousStatus = previousStatus;
        }
    }
}
