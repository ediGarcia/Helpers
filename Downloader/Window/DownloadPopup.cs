using System;
using System.Linq;
using System.Windows.Forms;
using Downloader.Classes.Helper;
using HelperExtensions;
using HelperMethods;

namespace Downloader.Window
{
    public partial class DownloadPopup : Form
    {
        #region Properties

        /// <summary>
        /// Indicates whether download should start ass soon as they are added.
        /// </summary>
        public bool AutoStart { get; set; } = true;

        /// <summary>
        /// Indicates whether the popup should be closed when all the downloads are finished.
        /// </summary>
        public bool CloseOnCompletion { get; set; }

        /// <summary>
        /// Indicates whether the downloads can be cancelled.
        /// </summary>
        public bool SupportsCancellation
        {
            get => ControlBox;
            set
            {
                ControlBox = value;
                UpdateCancelButtonsState();
            }
        }

        #endregion

        public DownloadPopup(params DownloadFileStartInfo[] downloads)
        {
            InitializeComponent();

            if (downloads.Length > 0)
                Load += (sender, args) => AddDownloads(downloads);

            DlsDownloads.DownloadStatusChanged += DlsDownloads_OnDownloadStatusChanged;
        }

        #region Events

        #region BtnClear_Click
        /// <summary>
        /// Removes all entries without a active download.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e) =>
            DlsDownloads.RemoveIdle();
        #endregion

        #region BtnClose_Click
        /// <summary>
        /// Closes the popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e) =>
            Close();
        #endregion

        #region BtnStartAll_Click
        /// <summary>
        /// Starts/stops all downloads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartAll_Click(object sender, EventArgs e)
        {
            if (DlsDownloads.HasDownloadsInProgress)
            {
                if (WinFormsMethods.ShowQuestionMessage(this, "Stopped downloads cannot be resumed. Stop anyway?"))
                    DlsDownloads.StopAll();
            }
            else
            {
                DlsDownloads.StartAll(restartBroken: true, restartCancelled: true);

                if (!SupportsCancellation)
                    BtnStartAll.Visible = false;
            }
        }
        #endregion

        #region DlsDownloads_OnDownloadStatusChanged
        /// <summary>
        /// Updates window when the downloads are completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DlsDownloads_OnDownloadStatusChanged(object sender, EventArgs e)
        {
            if (DlsDownloads.HasDownloadsInProgress)
                BtnStartAll.Text = "Stop All";

            else if (CloseOnCompletion)
                Close();

            else
                BtnStartAll.Text = "Start All";

            UpdateCancelButtonsState();
        }
        #endregion

        #region DownloadPopup_FormClosing
        /// <summary>
        /// Stops all the downloads before closing the popup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadPopup_FormClosing(object sender, FormClosingEventArgs e) =>
            DlsDownloads.StopAll();
        #endregion

        #endregion

        #region Public methods

        #region AddDownloads
        /// <summary>
        /// Adds downloads to the list.
        /// </summary>
        /// <param name="downloads"></param>
        public void AddDownloads(params DownloadFileStartInfo[] downloads)
        {
            downloads.ForEach(_ => DlsDownloads.AddDownload(_, AutoStart, SupportsCancellation));
            UpdateCancelButtonsState();
        }
        #endregion

        #endregion

        #region Pribate methods

        #region UpdateCancelButtonsState
        /// <summary>
        /// Enables/disables the cancel buttons.
        /// </summary>
        private void UpdateCancelButtonsState()
        {
            if (SupportsCancellation)
                BtnStartAll.Enabled = BtnClose.Enabled = true;

            else
            {
                BtnStartAll.Enabled = DlsDownloads.Items.Any(_ => _.DownloadStatus == DownloadStatus.NotStarted);

                BtnClose.Enabled = DlsDownloads.Items.All(_ =>
                    _.DownloadStatus != DownloadStatus.Downloading && _.DownloadStatus != DownloadStatus.NotStarted);
            }
        }
        #endregion

        #endregion
    }
}
