using System;
using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.Web;
using HashLib;
using WebDownloadingService;

namespace ElectiveService
{
    public partial class kyleTryIt : System.Web.UI.Page
    {
        // Hash the password and copy the hash into the stored hash box so Verify can be tested right away
        protected void btnHash_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                lblHash.Text = "Enter a password first.";
                return;
            }
            string hash = PasswordHasher.Hash(txtPassword.Text);
            lblHash.Text = hash;
            storedHash.Text = hash;
        }

        // Check the typed password against the stored hash
        protected void btnVerify_Click(object sender, EventArgs e)
        {
            lblMatch.Text = PasswordHasher.Verify(txtCheck.Text, storedHash.Text) ? "MATCH" : "NO MATCH";
        }

        // Check the CAPTCHA answer
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            lblCaptcha.Text = captcha1.Verify() ? "Correct" : "Incorrect";
        }

        // Calls the download service on WebStrar
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 10485760;
                binding.ReaderQuotas.MaxStringContentLength = 10485760;
                EndpointAddress address = new EndpointAddress(ConfigurationManager.AppSettings["DownloadServiceUrl"]);

                ChannelFactory<IWebDownloadService> factory = new ChannelFactory<IWebDownloadService>(binding, address);
                IWebDownloadService client = factory.CreateChannel();
                downloadOut.Text = HttpUtility.HtmlEncode(client.WebDownload(txtUrl.Text));
                factory.Close();
            }
            catch (Exception ex)
            {
                downloadOut.Text = "ERROR: " + ex.Message;
            }
        }

        // Shows the JSON the REST service returns
        protected void btnTop10_Click(object sender, EventArgs e)
        {
            try
            {
                string endpoint = ConfigurationManager.AppSettings["Top10Url"];
                lblTop10.Text = new WebClient().DownloadString(endpoint + "/Top10ContentWords?url=" + Uri.EscapeDataString(txtTopUrl.Text));
            }
            catch (Exception ex)
            {
                lblTop10.Text = "ERROR: " + ex.Message;
            }
        }
    }
}
