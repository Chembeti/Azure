using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Contoso.Transport.Services.Models;

using System.Collections.ObjectModel;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http.Routing.Constraints;
using System.Threading;

namespace Contoso.Transport.Services.Controllers
{
    internal class PackageResult : IHttpActionResult
    {
        IEnumerable<Event> package;
        HttpRequestMessage _request;
        public PackageResult(IEnumerable<Event> value, HttpRequestMessage request)
        {
            package = value;
            _request = request;
        }

        
        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new ObjectContent<IEnumerable<Event>>(package, new JsonMediaTypeFormatter()),
                RequestMessage = _request,
                StatusCode = HttpStatusCode.OK,
            };
            return response;
        }
    }
}