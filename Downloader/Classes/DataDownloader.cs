using System;
using System.Net;
using Downloader.Classes.Helper;
using HelperMethods;

namespace Downloader.Classes
{
    public class DataDownloader
    {
        public delegate void DownloadProgressChangedEventHandler(object sender, Helper.DownloadProgressChangedEventArgs e);

        public event EventHandler DownloadStarted;
        public event DownloadDataCompletedEventHandler DownloadCompleted;
        public event DownloadProgressChangedEventHandler DownloadProgressChanged;

        #region Properties

        /// <summary>
        /// User credentials.
        /// </summary>
        public Credentials.Credentials Credentials { get; set; }

        /// <summary>
        /// Gets the last downloaded data.
        /// </summary>
        public byte[] DownloadedData { get; private set; }

        /// <summary>
        /// Indicates whether a download is still in progress.
        /// </summary>
        public bool IsBusy => _webClient?.IsBusy == true;

        #endregion

        private readonly string _url;

        private DownloadTimeCalculator _downloadTimeCalculator;
        private DateTime _startDownloadTime;
        private WebClient _webClient;

        public DataDownloader(string url) =>
            _url = url;

        #region StartDownload
        /// <summary>
        /// Starts the download.
        /// </summary>
        public void StartDownload()
        {
            if (_webClient?.IsBusy == true)
                throw new WebException("A download is already in progress");

            _webClient = Methods.GetWebClient(Credentials);
            _webClient.DownloadProgressChanged += (o, e) =>
            {
                _downloadTimeCalculator.UpdateDownloadTimes(e.BytesReceived);

                DownloadProgressChanged?.Invoke(this, new Helper.DownloadProgressChangedEventArgs(
                    e.TotalBytesToReceive,
                    e.BytesReceived,
                    e.ProgressPercentage,
                    _downloadTimeCalculator.DownloadSpeed,
                    DateTime.Now - _startDownloadTime,
                    _downloadTimeCalculator.RemainingDownloadTime));
            };
            _webClient.DownloadDataCompleted += (o, e) =>
            {
                DownloadedData = e.Result;
                DownloadCompleted?.Invoke(this, e);
            };

            _startDownloadTime = DateTime.Now;
            _downloadTimeCalculator = new DownloadTimeCalculator(_startDownloadTime, BytesMethods.GetFileSize(_url, Credentials.User, Credentials.Password));
            _webClient.DownloadDataAsync(new Uri(_url));
            DownloadStarted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region StopDownload
        /// <summary>
        /// Stops the download.
        /// </summary>
        public void StopDownload()
        {
            if (_webClient?.IsBusy == false)
                throw new WebException("There is no download in progress.");

            _webClient.CancelAsync();
            _webClient.Dispose();
        }
        #endregion
    }
}