using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace Domain
{
    public static class Facade {
        // Gets the required token to next calls.
        public static void Login(HttpClient httpClient) {
            // Creates a authenticator instance.
            var authenticator = new Domain.Authenticator(httpClient);

            // Gets a token to next calls.
            var token = authenticator.Login().Result as Token;

            // Adds the Authorization header to the http client.
            var authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            httpClient.DefaultRequestHeaders.Authorization = authorization;
        }

        // Makes the search using a search object.
        public static List<SearchResult> Search(HttpClient httpClient, Search search) {
            // Makes the search and returns a Generic Collection.
            var searcher = new Domain.Searcher(httpClient);
            return searcher.Search(search).Result as List<SearchResult>;
        }
    }
}