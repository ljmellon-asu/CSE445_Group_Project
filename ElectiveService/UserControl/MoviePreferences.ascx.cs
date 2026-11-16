using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace ElectiveService.UserControl
{
    public partial class MoviePreferences : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowProfile();
            }
        }
      

        //this save button utilzises CookieHelper to save and store a user profile
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //ensure a username is 1-20 letters, or numbers or underscores to ensure it does not mess with the cookie
            if (!Regex.IsMatch(txtUsername.Text.Trim(), "^[A-Za-z0-9_]{1,20}$"))
            {
                lblProfile.Text = "Error: username must be 1-20 letters, numbers or underscores.";
                return;
            }
            CookieHelper.SaveProfile(Response, txtUsername.Text.Trim(), ddlGenre.SelectedValue);
            lblProfile.Text = "Profile saved successfully.";
        }

        //Read button to retreive information from a prev saved cookie
        protected void btnRead_Click(object sender, EventArgs e)
        {
            ShowProfile();
        }

        //the bloew label will display the stored user profile from a saved cookie
        private void ShowProfile()
        {
            HttpCookie cookie = CookieHelper.GetProfile(Request);

            if (cookie != null)
            {
                lblProfile.Text = "Username: " + Server.HtmlEncode(cookie["Username"]) + "<br/>Favorite Genre: " + Server.HtmlEncode(cookie["Genre"]) + "<br/>Saved At: " + Server.HtmlEncode(cookie["SavedAt"]);
            }
            else
            {
                lblProfile.Text = "No saved profile found."; //If no cookie with info is found display no profile found
            }
        }
    }
}