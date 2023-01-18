using Downloader.Classes.Credentials;
using System.Collections.Specialized;
using System.Net;

namespace Downloader.Classes.Helper
{
    public static class Methods
    {
        #region GetWebClient
        /// <summary>
        /// Retrieves a web client with the credentials data.
        /// </summary>
        /// <param name="credentials"></param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="WebException"></exception>
        /// <returns></returns>
        public static WebClient GetWebClient(Credentials.Credentials credentials = null)
        {
            switch (credentials)
            {
                case null:
                    return new WebClient();

                case CookieCredentials cookieCredentials:
                {
                    WebClient webClient = new CookiesAwareWebClient();
                    NameValueCollection loginData = new NameValueCollection(2)
                    {
                        { cookieCredentials.UserKey, cookieCredentials.User },
                        { cookieCredentials.PasswordKey, cookieCredentials.Password }
                    };

                    webClient.UploadValues(cookieCredentials.LoginUrl, loginData); //Sends login data.

                    return webClient;
                }

                default:
                    return new WebClient { Credentials = new NetworkCredential(credentials.User, credentials.Password) };
            }
        }
        #endregion
    }
}
