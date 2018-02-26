using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    public class IndexController : Controller
    {
        private HttpClient httpClient;

        public IndexController() {
            httpClient = new HttpClient();
        }

        [HttpPost("search")]
        public object Search([FromBody] Domain.Search search)
        {
            // Does login and defines authorization.
            Domain.Facade.Login(httpClient);

            // Gets a correct search object and makes the search.
            return Domain.Facade.Search(httpClient, search);
        }
    }
}
