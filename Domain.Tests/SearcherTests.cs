using System;
using Xunit;
using Domain;
using Moq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Equivalency;
using System.Collections.Generic;

namespace Domain.Tests
{
    public class SearcherTests
    {
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;

        public SearcherTests()
        {
            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
        }

        [Fact]
        public async Task Search_ShouldReturn_TheCorrectResult()
        {
            // Setup
            var search = new Search() {};

            var searchResult = new SearchResult() 
            { 
                TicketInfo = new TicketInfo() { 
                    Code = "1", 
                    Name = "Test" 
                }
            };

            var searchResultList = new List<SearchResult>();
            searchResultList.Add(searchResult);

            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(
                new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"Result\": [{\"TicketInfo\": {\"Code\": \"1\", \"Name\": \"Test\"}}]}")
                }
            );

            var s = new Searcher(_httpClient);

            // Action
            var r = await s.Search(search);

            // Assert
            r.Should().NotBeNull();
            r.Should().ContainSingle();
            r.Should().ContainSingle(x => x.TicketInfo.Name == "Test");
        }
    }
}
