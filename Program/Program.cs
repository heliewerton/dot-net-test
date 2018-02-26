using System;
using Domain;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

// The main class to run the project.  
public class Program
{
    // Configures the search object. 
    public static Search ConfigureSearch() {
        return new Search()
        {
            Language = "ENG",
            Currency = "USD",
            Destination = "MCO",
            
            DateFrom = DateTime.Now.ToString("MM/dd/yyyy"),
            DateTo = DateTime.Now.ToString("MM/dd/yyyy"),

            SearchOccupancy = new SearchOccupancy()
            {
                AdultCount = "1",
                ChildCount = "1",
                ChildAges = new string[] { "10" },
            }
        };
    } 

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

    // Parses search results to display values.
    public static void ParseSearchResults(List<SearchResult> results) {
        foreach (SearchResult result in results)
        {
            var ticketInfo = result.TicketInfo;
            
            Console.WriteLine("-----" + ticketInfo.Code + "-----");
            Console.WriteLine(ticketInfo.Name);
            Console.WriteLine("");
        }
    }

    public static void Run() {
        var httpClient = new HttpClient();

        // Does login and defines authorization.
        Domain.Facade.Login(httpClient);

        // Gets a correct search object and makes the search.
        var searchResults = Domain.Facade.Search(httpClient, ConfigureSearch());

        // Parses results.
        ParseSearchResults(searchResults);
    }

    // The main method to start.  
    public static void Main(string[] args)
    {
        // Great :)
        Program.Run();
    }
}