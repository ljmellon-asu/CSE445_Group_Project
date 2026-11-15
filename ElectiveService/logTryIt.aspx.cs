using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ElectiveService.Services;
using Newtonsoft.Json;

namespace ElectiveService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static readonly HttpClient ApiClient = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // Event handler for the log button click event
        protected void logButton_Click(object sender, EventArgs e)
        {
            // Validate the input fields
            string title = TitleTbox.Text.Trim();
            string year = YearTbox.Text.Trim();
            decimal rating;
            if (!decimal.TryParse(RatingTbox.Text, out rating)) 
            {
                StatusLabel.Text = "Enter a valid number for the rating.";
                StatusLabel.Visible = true;
                return;
            }


            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(year) || string.IsNullOrEmpty(RatingTbox.Text)) 
            {
                StatusLabel.Text = "Enter a valid title, year and rating.";
                StatusLabel.Visible = true;
                return;
            }
            // Log the movie asynchronously
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                try
                {
                    var request = new
                    {
                        Title = title,
                        Year = year,
                        UserRating = rating
                    };
                    using (var content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json"))
                    using (var response = await ApiClient.PostAsync(GetLogApiUrl(), content))
                    {
                        response.EnsureSuccessStatusCode();
                    }

                    StatusLabel.Text = "Movie was successfully logged!";
                }
                catch (HttpRequestException ex)
                {
                    StatusLabel.Text = "The log API request failed: " + ex.Message;
                }
                StatusLabel.Visible = true;
            }));
        }

        // Event handler for the show log button click event
        protected void showLogButton_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                try
                {
                    var log = await GetLogFromApi();
                    LogRepeater.DataSource = log;
                    LogRepeater.DataBind();
                    EmptyLogMessage.Visible = (log.Count == 0);
                    StatusLabel.Visible = false;
                }
                catch (HttpRequestException ex)
                {
                    ShowApiError(ex);
                }
            }));
        }

        // Event handler for the clear log button click event
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(async () =>
            {
                try
                {
                    using (var response = await ApiClient.DeleteAsync(GetLogApiUrl()))
                    {
                        response.EnsureSuccessStatusCode();
                    }

                    LogRepeater.DataSource = new List<LogEntry>();
                    LogRepeater.DataBind();
                    EmptyLogMessage.Visible = true;
                    StatusLabel.Visible = false;
                }
                catch (HttpRequestException ex)
                {
                    ShowApiError(ex);
                }
            }));
        }

        private async Task<List<LogEntry>> GetLogFromApi()
        {
            using (var response = await ApiClient.GetAsync(GetLogApiUrl()))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LogEntry>>(json) ?? new List<LogEntry>();
            }
        }

        private string GetLogApiUrl()
        {
            return Request.Url.GetLeftPart(UriPartial.Authority) + ResolveUrl("~/api/log");
        }

        private void ShowApiError(HttpRequestException ex)
        {
            StatusLabel.Text = "The log API request failed: " + ex.Message;
            StatusLabel.Visible = true;
        }
    }

}