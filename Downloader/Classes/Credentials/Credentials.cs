namespace Downloader.Classes.Credentials
{
    public class Credentials
    {
        #region Properties

        public string Password { get; }

        public string User { get; }

        #endregion

        public Credentials(string user, string password)
        {
            Password = password;
            User = user;
        }
    }
}