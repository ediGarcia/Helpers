namespace Downloader.Classes.Helper
{
    public class DownloadFileStartInfo
    {
        #region Properties

        /// <summary>
        /// User credentials for the download.
        /// </summary>
        public Credentials.Credentials Credentials { get; set; }

        /// <summary>
        /// Local file destination.
        /// </summary>
        public string Destination { get; }

        /// <summary>
        /// Source URL.
        /// </summary>
        public string Url { get; }

        #endregion

        public DownloadFileStartInfo(string url, string destination)
        {
            Destination = destination;
            Url = url;
        }
    }
}
