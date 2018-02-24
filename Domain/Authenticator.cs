using System;
using System.IO;

using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    // The authenticator class for authentication logic.
    public class Authenticator
    {
        // Holds a http client instance.
        private HttpClient httpClient;

        // Constructor to define required values.
        public Authenticator(HttpClient client) {
            httpClient = client;
        }

        // Does the login on the target api.
        public async Task<Domain.Token> Login()
        {
            // Mounts the url to get the token.
            var url = Config.Instance.TargetURL + Constants.TokenPath;

            // Makes a POST request to get the token.
            var response = await httpClient.PostAsync(url, 
                new StringContent(Config.Instance.TokenCredentials));

            // Ensures that the reponse code was success.
            response.EnsureSuccessStatusCode();

            // Gets the response content as string.
            var json = await response.Content.ReadAsStringAsync();
 
            return new Token().FromJSON(json);
        }

    }
}
