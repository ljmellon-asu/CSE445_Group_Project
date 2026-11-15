using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;

namespace requiredService2.Services
{
    public class SolarService
    {
        private readonly HttpClient myClient = new HttpClient();

        // task to get solar intensity from NASA POWER API
        public async Task<decimal?> SolarIntensity(decimal latitude, decimal longitude)
        {
            var latStr = latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var lonStr = longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);

            // NASA POWER API endpoint for solar intensity
            var url =
            $"https://power.larc.nasa.gov/api/temporal/climatology/point" +
            $"?parameters=ALLSKY_SFC_SW_DWN&community=RE" +
            $"&latitude={latStr}&longitude={lonStr}&format=JSON";

            
            try
            {
                if(!myClient.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    myClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                }

                // Fetch the JSON response from the NASA POWER API
                string jsonString = await myClient.GetStringAsync(url);

                // Deserialize the JSON response into C# objects
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };

                var response = JsonConvert.DeserializeObject<NasaPowerResponse>(jsonString, settings);


                // Return the annual average solar intensity value
                return response?.Properties?.Parameter?.AllSkySfcSwDwn?.AnnualAverage;
            }

            // Handle any exceptions that may occur during the HTTP request or JSON deserialization
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching solar intensity: {ex.Message}");
                return null;
            }
        }


        // Classes to deserialize NASA POWER API response
        public class NasaPowerResponse
        {
            public ResponseProperties Properties { get; set; }
        }

        public class  ResponseProperties 
        {
            public ParameterData Parameter { get; set; }
        }

        public class ParameterData 
        {
            [JsonProperty("ALLSKY_SFC_SW_DWN")]
            public SolarData AllSkySfcSwDwn { get; set; }
        }

        public class SolarData 
        {
            [JsonProperty("ANN")]
            public decimal AnnualAverage { get; set; }  
        }
    }
}