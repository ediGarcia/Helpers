namespace Downloader.Classes.Credentials
{
    public class CookieCredentials : Credentials
    {
        #region Properties

        /// <summary>
        /// Login validation URL.
        /// </summary>
        public string LoginUrl { get; set; }

        /// <summary>
        /// Password cooked key.
        /// </summary>
        public string PasswordKey { get; }

        /// <summary>
        /// User cookie key.
        /// </summary>
        public string UserKey { get; }

        #endregion

        public CookieCredentials(string loginUrl, string userKey, string user, string passwordKey, string password) :
            base(user, password)
        {
            LoginUrl = loginUrl;
            PasswordKey = passwordKey;
            UserKey = userKey;
        }
    }
}