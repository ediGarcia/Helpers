using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Downloader.Classes;
using Downloader.Classes.Helper;
using Downloader.Properties;
using HelperExtensions;
using HelperMethods;
using DownloadProgressChangedEventArgs = Downloader.Classes.Helper.DownloadProgressChangedEventArgs;

namespace Downloader.Window
{
    public partial class DownloadItem : UserControl
    {
        public event DownloadStatusChangedEventHandler DownloadStatusChanged;

        #region Properties

        /// <summary>
        /// Indicates whether the download should start immediately.
        /// </summary>
        [Category("Behavior"), Browsable(false), Description("Indicates whether the download should start immediately.")]
        public bool AutoStart { get; }

        /// <inheritdoc />
        /// <summary>
        /// The background color of the component.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlLightLight"), Description("The background color of the component.")]
        public override Color BackColor { get; set; } = SystemColors.ControlLightLight;

        /// <inheritdoc />
        /// <summary>
        /// The foreground color of the component, which is used to display text.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(SystemColors), "ControlText"), Description("The foreground color of the component, which is used to display text.")]
        public override Color ForeColor { get; set; } = SystemColors.ControlText;

        /// <summary>
        /// Indicates whether the current item is selected.
        /// </summary>
        [Category("Behavior"), DefaultValue(false), Description("Indicates whether the current item is selected.")]
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                base.BackColor = _isSelected ? SelectedBackColor : BackColor;
            }
        }

        /// <summary>
        /// Gets and sets the back color when the current item is highlighted.
        /// </summary>
        [Category("Appearance"), DefaultValue(typeof(Color), "LightCyan"), Description("Gets and sets the back color when the current item is highlighted.")]
        public Color SelectedBackColor { get; set; } = Color.LightCyan;

        /// <summary>
        /// Current download status.
        /// </summary>
        [Browsable(false)]
        public DownloadStatus DownloadStatus => _downloader.Status;

        /// <summary>
        /// Indicates whether the download can be cancelled by the user (does not affect the StopDownload method).
        /// </summary>
        [Category("Behavior"), Browsable(false), Description("Indicates whether the current item is selected.")]
        public bool SupportsCancellation { get; }

        #endregion

        private readonly FileDownloader _downloader; //Class that downloads the file.
        private bool _isSelected;

        public DownloadItem(DownloadFileStartInfo downloadInfo, bool autoStart = true, bool supportsCancellation = true)
        {
            InitializeComponent();

            AutoStart = autoStart;
            SupportsCancellation = supportsCancellation;

            _downloader = new FileDownloader(downloadInfo);
            _downloader.DownloadStarted += Downloader_OnDownloadStarted;
            _downloader.DownloadProgressChanged += Downloader_OnDownloadProgressChanged;
            _downloader.DownloadCompleted += Downloader_OnDownloadCompleted;
            _downloader.DownloadStatusChanged += (sender, e) => DownloadStatusChanged?.Invoke(this, e);

            LblFileName.Text = Path.GetFileName(downloadInfo.Destination);
        }

        #region Events

        #region BtnCancel_Click
        /// <summary>
        /// Starts/stops the download.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_downloader.IsBusy)
                _downloader.StopDownload();

            else
                _downloader.StartDownload();
        }
        #endregion

        #region Downloader_OnDownloadCompleted
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_OnDownloadCompleted(object sender, DownloadCompletedEventArgs e)
        {
            //Cancelled by the user.
            if (e.Cancelled)
            {
                PrbProgress.ProgressBarColor = Color.OrangeRed;
                BtnCancel.Image = Resources.StartIcon;
                LblDownloadInfo.Text = "Cancelled by the user.";
            }

            //Download error.
            else if (e.Error != null)
            {
                PrbProgress.ProgressBarColor = Color.IndianRed;
                LblDownloadInfo.Text = "Download error.";
                TotTooltip.SetToolTip(LblDownloadInfo, e.Error.Message);
            }

            //Download successful.
            else
            {
                PrbProgress.Value = PrbProgress.Maximum;
                LblDownloadInfo.Text = "Download completed.";
                HideStartStopButton();
            }
        }
        #endregion

        #region Downloader_OnDownloadProgressChanged
        /// <summary>
        /// Updates download speed and remaining time information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            PrbProgress.Value = e.ProgressPercentage;
            LblElapsedTime.Text = $"{e.ElapsedTime.TotalHours:00}:{e.ElapsedTime:mm\\:ss}";

            if (e.RemainingTime.HasValue)
            {
                StringBuilder downloadInfo = new StringBuilder();

                if (e.RemainingTime.Value.TotalHours >= 1)
                    downloadInfo.Append(e.RemainingTime.Value.TotalHours.ToString("0.#")).Append(" hours");
                else if (e.RemainingTime.Value.TotalMinutes >= 1)
                    downloadInfo.Append(e.RemainingTime.Value.TotalMinutes.ToString("0")).Append(" minutes");
                else
                    downloadInfo.Append(e.RemainingTime.Value.TotalSeconds.ToString("0")).Append(" seconds");

                downloadInfo.Append("", " remaining — ", BytesMethods.GenerateString(e.BytesReceived), " of ",
                    BytesMethods.GenerateString(e.TotalBytes),
                    " (", BytesMethods.GenerateString(e.BytesPerSecond, 1), "/s)");

                LblDownloadInfo.Text = downloadInfo.ToString();
            }
            else
                LblDownloadInfo.Text = "Downloading file...";
        }
        #endregion

        #region Downloader_OnDownloadStarted
        /// <summary>
        /// Updates interface on download start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downloader_OnDownloadStarted(object sender, EventArgs e)
        {
            if (SupportsCancellation)
                BtnCancel.Image = Resources.CancelIcon;
            else
                HideStartStopButton();

            TotTooltip.SetToolTip(LblDownloadInfo, null);
            LblElapsedTime.Visible = true;
            LblElapsedTime.Text = "00:00:00";
            PrbProgress.ProgressBarColor = Color.LimeGreen;
            PrbProgress.Value = 0;
            LblDownloadInfo.Text = "Download started...";
        }

        #endregion

        #region DownloadItem_Load
        /// <summary>
        /// Auto-starts the download if needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadItem_Load(object sender, EventArgs e)
        {
            if (AutoStart)
                StartDownload();
        }
        #endregion

        #endregion

        #region Public Methods

        #region StartDownload
        /// <summary>
        /// Starts the download.
        /// </summary>
        public void StartDownload() =>
            _downloader.StartDownload();
        #endregion

        #region StopDownload

        /// <summary>
        /// Stops the download.
        /// </summary>
        public void StopDownload() =>
            _downloader.StopDownload();

        #endregion

        #endregion

        #region Private Methods

        #region HideStartStopButton
        /// <summary>
        /// Hides the start/stop button.
        /// </summary>
        private void HideStartStopButton()
        {
            if (BtnCancel.Visible)
            {
                BtnCancel.Visible = false;
                PrbProgress.Width += 25;
            }
        }
        #endregion

        #endregion
    }
}
