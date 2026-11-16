using System;
using System.Net;

namespace WebDownloadingService
{
    public class WebDownloadService : IWebDownloadService
    {
        // Returns the text of the page at the given address, or an ERROR message.
        public string WebDownload(string url)
        {
            // file://, ftp:// and plain text are refused
            if (url == null || !(url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)))
            {
                return "ERROR: Enter a full address starting with http:// or https://";
            }

            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0"); // plain WebClient sends none and some servers refuse
                return client.DownloadString(url);
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }
    }
}
