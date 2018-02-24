using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;

namespace Domain
{
    // The class searcher to make a search on the target api.
    public class Searcher
    {
        // Holds the http client instance.
        private HttpClient httpClient { get; set; }

        // Constructor to defined required values.
        public Searcher(HttpClient client) 
        {
            httpClient = client;
        }

        // Makes the search on the target api.
        public async Task<List<SearchResult>> Search(Search search)
        {
            // Mounts the search url on the api.
            var url = Config.Instance.TargetURL + Constants.SearchPath;

            // Makes a POST request to get the search results.
            var response = await httpClient.PostAsync(url, search.ToJSONStringContent());

            // Ensures that the reponse code was success.
            response.EnsureSuccessStatusCode();

            // Gets the response content as string.
            var json = await response.Content.ReadAsStringAsync();

            // Extract the right result json.
            var resultJson = ExtractSearchResultJSON(json);

            return new List<SearchResult>().FromJSON(resultJson);
        }

        // Extracts the correct json from the search result.
        private string ExtractSearchResultJSON(string json) {
            JObject data = JObject.Parse(json);
            return data["Result"].ToString();
        }

    }
}
