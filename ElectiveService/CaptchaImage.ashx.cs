using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace ElectiveService
{
    // Handler draws a random code as a PNG and keeps the code in Session
    public class CaptchaImage : IHttpHandler, IRequiresSessionState
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random rnd = new Random();

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // pick 5 random characters and remember them
            string code = "";
            for (int i = 0; i < 5; i++)
            {
                code += Chars[rnd.Next(Chars.Length)];
            }
            context.Session["captcha"] = code;

            // draw the code then a few noise lines on top
            using (Bitmap bmp = new Bitmap(150, 50))
            using (Graphics g = Graphics.FromImage(bmp))
            using (MemoryStream ms = new MemoryStream())
            {
                g.Clear(Color.White);
                g.DrawString(code, new Font("Arial", 24, FontStyle.Bold), Brushes.Black, 12, 8);
                for (int i = 0; i < 5; i++)
                {
                    g.DrawLine(Pens.Gray, rnd.Next(150), rnd.Next(50), rnd.Next(150), rnd.Next(50));
                }

                bmp.Save(ms, ImageFormat.Png);
                context.Response.ContentType = "image/png";
                context.Response.BinaryWrite(ms.ToArray());
            }
        }
    }
}
