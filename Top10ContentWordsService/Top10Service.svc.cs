using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Top10ContentWordsService
{
    public class Top10Service : ITop10Service
    {
        // common English words that are not counted
        private static readonly string[] stopWords =
        {
            "a","an","the","and","or","but","if","then","else","when","while",
            "of","at","by","for","with","about","against","between","into",
            "through","during","before","after","above","below","to","from",
            "up","down","in","out","on","off","over","under","again","further",
            "here","there","all","any","both","each","few","more","most",
            "other","some","such","no","nor","not","only","own","same","so",
            "than","too","very","can","will","just","should","now",
            "is","are","was","were","be","been","being","am",
            "have","has","had","having","do","does","did","doing",
            "i","me","my","myself","we","our","ours","ourselves",
            "you","your","yours","yourself","yourselves",
            "he","him","his","himself","she","her","hers","herself",
            "it","its","itself","they","them","their","theirs","themselves",
            "what","which","who","whom","this","that","these","those",
            "as","until","because"
        };

        // Downloads the page, removes markup and stop words, stems the words,
        // counts them and returns the 10 most frequent as "word (count)"
        public string[] Top10ContentWords(string url)
        {
            Uri target;
            if (!Uri.TryCreate(url, UriKind.Absolute, out target)
                || (target.Scheme != Uri.UriSchemeHttp && target.Scheme != Uri.UriSchemeHttps))
            {
                return new[] { "ERROR: Enter a full address starting with http:// or https://" };
            }

            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0"); // Wikipedia answers 403 without one
                string html = client.DownloadString(target);

                // take out comments, script and style blocks, then all remaining tags
                string text = Regex.Replace(html, "<!--.*?-->", " ", RegexOptions.Singleline);
                text = Regex.Replace(text, "<(script|style)\\b[^>]*>.*?</\\1>", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                text = Regex.Replace(text, "<[^>]+>", " ");
                text = WebUtility.HtmlDecode(text);

                // stem -> number of times it appeared
                var counts = new Dictionary<string, int>();
                foreach (Match match in Regex.Matches(text, "[A-Za-z]+"))
                {
                    string word = match.Value.ToLower();
                    if (word.Length < 2 || stopWords.Contains(word)) continue;

                    // simple stemming: cut ing, ed or a plural s
                    string stem = word;
                    if (word.Length > 5 && word.EndsWith("ing")) stem = word.Substring(0, word.Length - 3);
                    else if (word.Length > 4 && word.EndsWith("ed")) stem = word.Substring(0, word.Length - 2);
                    else if (word.Length > 3 && word.EndsWith("s") && !word.EndsWith("ss")) stem = word.Substring(0, word.Length - 1);

                    // running -> runn -> run
                    int n = stem.Length;
                    if (stem != word && n > 2 && stem[n - 1] == stem[n - 2] && stem[n - 1] != 'l' && stem[n - 1] != 's')
                        stem = stem.Substring(0, n - 1);

                    counts[stem] = counts.ContainsKey(stem) ? counts[stem] + 1 : 1;
                }

                // sort by count, then alphabetically, and keep top 10
                return counts
                    .OrderByDescending(pair => pair.Value)
                    .ThenBy(pair => pair.Key)
                    .Take(10)
                    .Select(pair => pair.Key + " (" + pair.Value + ")")
                    .ToArray();
            }
            catch (Exception ex)
            {
                return new[] { "ERROR: " + ex.Message };
            }
        }
    }
}
