using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectiveService
{
    public partial class cookieStemTryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

    

        //This is the button that acutally calls the stemming REST SErvice
        //It takes a input of a string of inputted text
        //outputs the stemmed text derived from user input and is displayed
        protected void btnStem_Click(object sender, EventArgs e)
        {
            txtStemOutput.Text = "";
            lblStatus.Text = "";

            if (string.IsNullOrWhiteSpace(txtStemInput.Text))
            {
                lblStatus.Text = "Please enter some text.";
                return;
            }

            try
            {
                //from web.config, locate the REST Service
                string url = ConfigurationManager.AppSettings["StemmingUrl"];

                // Will take the inputted text and convert it into JSON for the service
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string jsonInput = serializer.Serialize(txtStemInput.Text);

                using (WebClient client = new WebClient())
                {
                    //  Ensure the request content type is set to JSON
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

                    // This will send the text to the Stemming REST Service via POST method
                    string response = client.UploadString(url, "POST", jsonInput);

                    // This will convert the response text back into a string
                    txtStemOutput.Text = serializer.Deserialize<string>(response);
                }

                lblStatus.Text = "Stemming completed.";
            }
            catch (Exception ex)
            {
                lblStatus.Text = Server.HtmlEncode(ex.Message);
            }
        }
    }
}