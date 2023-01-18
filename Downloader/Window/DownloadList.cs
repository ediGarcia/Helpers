using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Downloader.Classes.Helper;
using HelperExtensions;

namespace Downloader.Window
{
    public partial class DownloadList : UserControl
    {
        #region Events

        /// <summary>
        /// Event thrown when the download status of ant item is changed.
        /// </summary>
        public event EventHandler DownloadStatusChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether there is a download in progress.
        /// </summary>
        [Browsable(false)]
        public bool HasDownloadsInProgress => _items.Any(_ => _.DownloadStatus == DownloadStatus.Downloading);

        /// <summary>
        /// Indicates whether any download has been stopped by an error.
        /// </summary>
        [Browsable(false)]
        public bool HasErrors => _items.Any(_ => _.DownloadStatus == DownloadStatus.Broken);

        /// <summary>
        /// Download items.
        /// </summary>
        [Browsable(false)]
        public DownloadItem[] Items => _items.ToArray();

        #endregion

        private readonly List<DownloadItem> _items = new List<DownloadItem>();

        public DownloadList() =>
            InitializeComponent();

        #region Public methods

        #region AddDownload
        /// <summary>
        /// Adds a new download to the list.
        /// </summary>
        /// <param name="downloadInfo"></param>
        /// <param name="autoStart"></param>
        /// <param name="supportsCancellation"></param>
        public void AddDownload(DownloadFileStartInfo downloadInfo, bool autoStart, bool supportsCancellation = true)
        {
            DownloadItem newItem = new DownloadItem(downloadInfo, autoStart, supportsCancellation);
            newItem.Top = _items.Count * newItem.Height;
            newItem.Width = _items.Count > 0 ? _items[0].Width : Width;
            newItem.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            newItem.DownloadStatusChanged += (sender, args) => DownloadStatusChanged?.Invoke(this, args);

            Controls.Add(newItem);
            _items.Add(newItem);
        }
        #endregion

        #region RemoveAll
        /// <summary>
        /// Removes all downloads from the list, any running download is cancelled.
        /// </summary>
        public void RemoveAll()
        {
            StopAll();
            Controls.Clear();
            _items.Clear();
        }
        #endregion

        #region RemoveIdle
        /// <summary>
        /// Removes all download that are not running.
        /// </summary>
        public void RemoveIdle()
        {
            _items.InverseForEach((item, index) =>
            {
                if (item.DownloadStatus != DownloadStatus.Downloading)
                    RemoveDownload(index);
            });

            _items.ForEach((item, index) => item.Top = item.Height * index);
        }
        #endregion

        #region RemoveDownload
        /// <summary>
        /// Removes a download from the list.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveDownload(int index)
        {
            StopDownload(index);

            Controls.RemoveAt(index);
            _items.RemoveAt(index);
        }
        #endregion

        #region StartAll
        /// <summary>
        /// Starts all the not started downloads from the list.
        /// </summary>
        public void StartAll(bool restartCompleted = false, bool restartBroken = false, bool restartCancelled = false) =>
            _items.ForEach(_ =>
            {
                if (_.DownloadStatus == DownloadStatus.NotStarted ||
                    _.DownloadStatus == DownloadStatus.Completed && restartCompleted ||
                    _.DownloadStatus == DownloadStatus.Broken && restartBroken ||
                    _.DownloadStatus == DownloadStatus.Cancelled && restartCancelled)
                    _.StartDownload();
            });
        #endregion

        #region StartDownload
        /// <summary>
        /// Starts the selected download, if not running.
        /// </summary>
        /// <param name="index"></param>
        public void StartDownload(int index)
        {
            if (_items[index].DownloadStatus != DownloadStatus.Downloading)
                _items[index].StartDownload();
        }
        #endregion

        #region StopAll
        /// <summary>
        /// Stops all the running downloads from the list.
        /// </summary>
        public void StopAll() =>
            _items.ForEach((item, index) => StopDownload(index));
        #endregion

        #region StopDownload
        /// <summary>
        /// Stops the selected download, if running.
        /// </summary>
        /// <param name="index"></param>
        public void StopDownload(int index)
        {
            if (_items[index].DownloadStatus == DownloadStatus.Downloading)
                _items[index].StopDownload();
        }
        #endregion

        #endregion
    }
}
