using Downloader.Classes.Credentials;
using Downloader.Classes.Helper;
using Downloader.Window;
using System;
using System.Windows.Forms;
using HelperMethods;

namespace LibraryTest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            SystemMethods.IsDirectory("aa");
        }

        private void BtnDownloadTest_Click(object sender, EventArgs e)
        {
            //DownloadPopup downloadPopup = new DownloadPopup(new DownloadFileStartInfo("http://212.183.159.230/512MB.zip", @"C:\Users\avaz\Desktop\512MB.zip")) { SupportsCancellation = false, AutoStart = false };
            /*DownloadPopup downloadPopup = new DownloadPopup(
                    new DownloadFileStartInfo("http://212.183.159.230/50MB.zip", @"C:\Users\avaz\Desktop\50MB.zip"),
                    new DownloadFileStartInfo("http://212.183.159.230/512MB.zip", @"C:\Users\avaz\Desktop\512MB.zip"),
                    new DownloadFileStartInfo("http://212.183.159.230/10MB.zip", @"C:\Users\avaz\Desktop\10MB.zip"),
                    new DownloadFileStartInfo("http://212.183.159.230/200MB.zip", @"C:\Users\avaz\Desktop\200MB.zip"),
                    new DownloadFileStartInfo("http://212.183.159.230/1GB.zip", @"C:\Users\avaz\Desktop\1GB.zip"),
                    new DownloadFileStartInfo("http://212.183.159.230/5MB.zip", @"C:\Users\avaz\Desktop\5MB.zip"))
                { SupportsCancellation = true, AutoStart = false };*/
            //DownloadPopup downloadPopup = new DownloadPopup(new DownloadFileStartInfo("http://212.183.159.230/50MB.zip", @"C:\Users\avaz\Desktop\50MB.zip")) { SupportsCancellation = false, AutoStart = false };
            /*DownloadPopup downloadPopup = new DownloadPopup(new DownloadFileStartInfo("https://repo.rdisoftware.com/artifactory/eu-localization-develop-local/de/NP6.1.36.DE.UAT.B4314.zip", @"C:\Users\avaz\Desktop\NP6.1.36.DE.UAT.B4314.zip") { Credentials = new Credentials("avaz", "minhaSenha27")})
                { SupportsCancellation = false, AutoStart = false };*/
            DownloadPopup downloadPopup =
                new DownloadPopup(
                        new DownloadFileStartInfo(
                            "https://jira.rdisoftware.com/jira/secure/attachment/727776/DesktopLoad.7z",
                            @"C:\Users\avaz\Desktop\DesktopLoad.7z") { Credentials = new CookieCredentials("https://jira.rdisoftware.com/jira/secure/Dashboard.jspa", "os_username", "avaz", "os_password", "minhaSenha27") })
                    { SupportsCancellation = false, AutoStart = false };
            downloadPopup.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
