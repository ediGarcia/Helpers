using System;
using System.Net;

namespace Downloader.Classes.Helper
{
    public class CookiesAwareWebClient : WebClient
    {
        public CookieContainer CookieContainer = new CookieContainer();

        #region Public Methods

        #region GetWebRequest

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            (request as HttpWebRequest).CookieContainer = CookieContainer;
            return request;
        }

        #endregion

        #endregion
    }
}