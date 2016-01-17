using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIMAADDemo.Controllers
{
    [Authorize]
    public class CalcController : ApiController
    {
        [Route("api/add")]
        [HttpPost]
        public HttpResponseMessage Add(int a, int b)
        {
            return Request.CreateResponse<int>(a + b);
        }
        [Route("api/subtract")]
        [HttpPost]
        public HttpResponseMessage Subtract(int a, int b)
        {
            return Request.CreateResponse<int>(a - b);
        }
    }
}
