using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using Downloader.Classes.Helper;
using HelperMethods;
using DownloadProgressChangedEventArgs = System.Net.DownloadProgressChangedEventArgs;
// ReSharper disable UnusedMember.Global

namespace Downloader.Classes
{
    public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventArgs e);
    public delegate void DownloadProgressChangedEventHandler(object sender, Helper.DownloadProgressChangedEventArgs e);
    public delegate void DownloadStatusChangedEventHandler(object sender, DownloadStatusChangedEventArgs e);

    public class FileDownloader
    {
        public event EventHandler DownloadStarted;
        public event DownloadCompletedEventHandler DownloadCompleted;
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event DownloadStatusChangedEventHandler DownloadStatusChanged;

        #region Properties

        /// <summary>
        /// User credentials.
        /// </summary>
        public Credentials.Credentials Credentials { get; set; }

        /// <summary>
        /// Gets the downloaded file's local path.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Gets the file's source url.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Current download status.
        /// </summary>
        public DownloadStatus Status
        {
            get => _status;
            private set
            {
                DownloadStatus previousStatus = _status;
                _status = value;

                if (previousStatus != _status)
                    DownloadStatusChanged?.Invoke(this, new DownloadStatusChangedEventArgs(previousStatus, _status));
            }
        }

        /// <summary>
        /// Gets the time when the last download has started.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Indicates whether a download is still in progress.
        /// </summary>
        public bool IsBusy => _webClient?.IsBusy == true;

        #endregion

        private DownloadTimeCalculator _downloadTimeCalculator;
        private long _fileSize;
        private DownloadStatus _status = DownloadStatus.NotStarted;
        private WebClient _webClient;

        public FileDownloader(DownloadFileStartInfo downloadInfo) : this(downloadInfo.Url, downloadInfo.Destination, downloadInfo.Credentials) { }

        public FileDownloader(string url, string destinationPath, Credentials.Credentials credentials = null)
        {
            Credentials = credentials;
            Destination = destinationPath;
            Source = url;
        }

        #region WebClient_OnDownloadProgressChanged
        /// <summary>
        /// Throws event updating the download progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //Fills remote file size if the GetFileSize method fails.
            if (_downloadTimeCalculator.TotalBytes <= 0)
                _downloadTimeCalculator.TotalBytes = e.TotalBytesToReceive;

            _downloadTimeCalculator.UpdateDownloadTimes(e.BytesReceived);

            DownloadProgressChanged?.Invoke(this, new Helper.DownloadProgressChangedEventArgs(
                e.TotalBytesToReceive,
                e.BytesReceived,
                e.ProgressPercentage,
                _downloadTimeCalculator.DownloadSpeed,
                // ReSharper disable once PossibleInvalidOperationException
                DateTime.Now - StartTime.Value,
                _downloadTimeCalculator.RemainingDownloadTime));
        }
        #endregion

        #region WebClient_OnDownloadFileCompleted
        /// <summary>
        /// Throws event updating the download progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebClient_OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // ReSharper disable once PossibleInvalidOperationException
            TimeSpan elapsedTime = DateTime.Now - StartTime.Value;

            DownloadCompleted?.Invoke(this, new DownloadCompletedEventArgs(
                File.Exists(Destination) && elapsedTime > TimeSpan.Zero
                    ? _fileSize / elapsedTime.TotalSeconds
                    : 0,
                elapsedTime,
                e.Cancelled,
                e.Error,
                e.UserState
            ));

            Status = e.Cancelled ? DownloadStatus.Cancelled : e.Error != null ? DownloadStatus.Broken : DownloadStatus.Completed;
        }
        #endregion

        #region Public events

        #region StartDownload
        /// <summary>
        /// Starts the download.
        /// </summary>
        public void StartDownload()
        {
            if (_webClient?.IsBusy == true)
                throw new WebException("The download is already in progress.");

            _webClient = Methods.GetWebClient(Credentials);
            _webClient.DownloadProgressChanged += WebClient_OnDownloadProgressChanged;
            _webClient.DownloadFileCompleted += WebClient_OnDownloadFileCompleted;

            StartTime = DateTime.Now;
            _downloadTimeCalculator = new DownloadTimeCalculator(StartTime.Value, _fileSize = BytesMethods.GetFileSize(Source, Credentials?.User, Credentials?.Password));
            _webClient.DownloadFileAsync(new Uri(Source), Destination);
            DownloadStarted?.Invoke(this, EventArgs.Empty);
            Status = DownloadStatus.Downloading;
        }

        #endregion

        #region StopDownload
        /// <summary>
        /// Stops the download.
        /// </summary>
        /// <param name="showAsBroken">Indicates whether the download should be marked as broken when stopped.</param>
        public void StopDownload(bool showAsBroken = false)
        {
            if (_webClient?.IsBusy == false)
                throw new WebException("There is no download in progress.");

            _webClient.CancelAsync();
            Status = showAsBroken ? DownloadStatus.Broken : DownloadStatus.Cancelled;
        }
        #endregion

        #endregion
    }
}