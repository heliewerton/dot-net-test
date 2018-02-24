using System;
using Xunit;
using Domain;
using Moq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;

namespace Domain.Tests
{
    public class AuthenticatorTests
    {
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;

        public AuthenticatorTests()
        {
            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
        }

        [Fact]
        public async Task Login_ShouldReturn_TheCorrectToken()
        {
            // Setup
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content =new StringContent("{\"access_token\": \"access.token.string\"}")
            });

            var a = new Authenticator(_httpClient);

            // Action
            var r = await a.Login();

            // Assert
            r.Should().NotBeNull();
            r.Should().BeOfType<Token>();
            r.Should()
                .Match<Token>((x) => 
                    x.AccessToken == "access.token.string"
                );
        }
    }
}
