using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElectiveService.Services
{

    // LogEntry class to represent a movie log entry
    public class LogEntry
    {
        public string Title { get; set; }
        public string ReleaseDate { get; set; }
        public string Runtime { get; set; }
        public string PosterUrl { get; set; }
        public decimal UserRating { get; set; }

    }
    // LogService class to handle logging of movie information
    public class LogService
    {
        // Static HttpClient instance to be reused for all requests
        private static readonly HttpClient myClient = new HttpClient();
        // Lock object to ensure thread-safe access to the log file
        private static readonly object FileLock = new object();

        // Method to log movie information by fetching details from the OMDB API
        public async Task LogMovie(string title, string year, decimal rating) 
        {
            // Retrieve the OMDB API key from the application settings and validate it
            string key = System.Configuration.ConfigurationManager.AppSettings["OmdbApiKey"];

            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException("The OMDB API key is not configured.");

            // Validate the input parameters
            if (string.IsNullOrWhiteSpace(title)) {
                throw new ArgumentException("Movie title is required.", nameof(title));
            }
            if (string.IsNullOrWhiteSpace(year)) {
                throw new ArgumentException("Release year is required.", nameof(year));
            }

            // Construct the URL for the OMDB API request
            string url = "https://www.omdbapi.com/?apikey=" + Uri.EscapeDataString(key)
                + "&t=" + Uri.EscapeDataString(title)
                + "&y=" + Uri.EscapeDataString(year);

            // Make the HTTP GET request to the OMDB API and parse the JSON response
            string json = await myClient.GetStringAsync(url);
            JObject movie = JObject.Parse(json);

            if ((string)movie["Response"] != "True") {
                throw new InvalidOperationException((string)movie["Error"]);
            }
            // Create a new LogEntry object with the retrieved movie details and user rating
            var entry = new LogEntry
            {
                Title = (string)movie["Title"],
                ReleaseDate = (string)movie["Released"],
                Runtime = (string)movie["Runtime"],
                UserRating = rating,
                PosterUrl = (string)movie["Poster"]
            };
            // Lock the file access to ensure thread safety while writing to the log file
            lock (FileLock)
            {
                List<LogEntry> log = GetLog();
                log.Add(entry);

                // Write the updated log to the JSON file in the App_Data directory
                string path = GetLogPath();
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, JsonConvert.SerializeObject(log, Formatting.Indented));
            }

        }
        // Method to retrieve the current log entries from the JSON file
        public List<LogEntry> GetLog() 
        {
            string path = GetLogPath();
            
            // Check if the log file exists; if not, return an empty list
            if (!File.Exists(path)) {
                return new List<LogEntry>();
            }
            // Read the JSON content from the log file and deserialize it into a list of LogEntry objects
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<LogEntry>>(json) ?? new List<LogEntry>();
        }

        // Method to clear the log by overwriting the log file with an empty JSON array
        public void ClearLog()
        {
            lock (FileLock)
            {
                string path = GetLogPath();
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, "[]");
            }
        }

        // Private helper method to get the path to the log file in the App_Data directory
        private string GetLogPath() 
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/Movie-log.json");
        }

    }
 }
    
     