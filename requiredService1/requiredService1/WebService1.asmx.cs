using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace WordCountServ
{
    /// <summary>
    /// The web service is designed to count the occurrences of each word in a given text file. It has a single method, WordCount.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService1 : System.Web.Services.WebService
    {
        /// <summary>
        /// The WordCountService takes a byte array representing a text file and returns a JSON string containing the count of each word in the file.
        /// </summary>
        /// <param name="fileBytes">A byte array representing the contents of a text file.</param>
        /// <returns>A JSON string containing the count of each word in the file.</returns>
        [WebMethod]
        public string WordCount(byte[] fileBytes) 
        {
            //if the files empty just return an empty json object
            if (fileBytes == null || fileBytes.Length == 0) { return "{}";}

                //dictonary is c#'s version of a map so we are using it to store the word counts. 
                Dictionary<string, int> wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
               
                //converting to a string so we can do regex operations on it.
                string fileAsString = Encoding.UTF8.GetString(fileBytes);

            //regex matches words and counts contractions as words. For example, "can't" will be counted as a single word.
            MatchCollection matches = Regex.Matches(fileAsString, @"\b\w+(?:'\w+)?\b");

                //loops the matches if the words already in the dictionary it increments the value, if not it adds to dictionary with value of 1.
                foreach (Match match in matches) {

                    string word = match.Value.ToLower();

                    if (wordCounts.ContainsKey(word)) { wordCounts[word]++; }
                    else { wordCounts[word] = 1; }
                }

            //returns the dictionary as a json string. This is the output of the web service.
            return new JavaScriptSerializer().Serialize(wordCounts);
        }
    }
}
