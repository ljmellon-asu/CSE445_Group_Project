using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace requiredService1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //first we check if its a .txt file 
            if (!File1.PostedFile.FileName.EndsWith(".txt")) {
                TextBox2.Text = "Invalid File Type";
                return;
            }

            //we check if the files even real and if its got anything in it
            if (File1.PostedFile != null && File1.PostedFile.ContentLength > 0) {

                //sets up a byte array to put the file data into so its easier to process
                int fileLength = File1.PostedFile.ContentLength;
                byte[] fileBytes = new byte[fileLength];

                File1.PostedFile.InputStream.Read(fileBytes, 0, fileLength);

                //creates a new instance of the web service client and calls the WordCount method with the file bytes
                var client = new WordCountServ.WebService1SoapClient();
                string jsonres = client.WordCount(fileBytes);

                //displays the result in the text box
                jsonres = jsonres.Replace("{", "{\n").Replace("}", "\n}").Replace(",", ",\n").Replace(@"\u0027", "'");
                TextBox2.Text =jsonres;
            }
            else
            {
                TextBox2.Text = "Empty File";
            }
        }
    }
}