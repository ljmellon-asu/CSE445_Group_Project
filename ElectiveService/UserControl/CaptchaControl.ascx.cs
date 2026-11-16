using System;

namespace ElectiveService.UserControl
{
    // User control with the CAPTCHA image and a textbox.
    public partial class CaptchaControl : System.Web.UI.UserControl
    {
        // True if the typed text matches the code the image handler stored in Session
        public bool Verify()
        {
            return txtCaptcha.Text == (string)Session["captcha"];
        }
    }
}
