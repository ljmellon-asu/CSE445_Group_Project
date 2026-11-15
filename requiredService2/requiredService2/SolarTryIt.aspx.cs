using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using requiredService2.Services;    

namespace requiredService2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            // Declare variables to hold the latitude and longitude values
            decimal lat;
            decimal lon;

            // Validate the input and parse the latitude and longitude
            if ((Decimal.TryParse(TextBox1.Text, out lat) && Decimal.TryParse(TextBox2.Text, out lon)) && -90 < lat && lat < 90 && -180 < lon && lon < 180)
            {
                RegisterAsyncTask(new PageAsyncTask(async () =>
                {
                    // Create an instance of the SolarService
                    var solarService = new SolarService();
                    // Call the SolarIntensity method to get the solar intensity
                    var intensity = await solarService.SolarIntensity(lat, lon);
                    if (intensity.HasValue)
                    {
                        // Display the solar intensity in the TextBox3
                        TextBox3.Text = $"{intensity.Value} kWh/m²/day";
                    }
                    else
                    {
                        TextBox3.Text = "Could not retrieve solar intensity.";
                    }
                }));
            }
            else
            {
                TextBox3.Text = "Invalid latitude or longitude.";
            }
        }
    }
}