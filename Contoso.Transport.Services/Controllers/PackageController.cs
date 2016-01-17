using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Contoso.Transport.Services.Models;

namespace Contoso.Transport.Services.Controllers
{
    [Authorize]
    [RoutePrefix("api/package")]
    public class PackageController : BaseController
    {
        private static IEnumerable<Package> packages;

        public Package Get(int id)
        {
            return packages.SingleOrDefault(p => p.Id == id);
        }

        [Route("{id}/status")]
        public IHttpActionResult GetStatus(int id)
        {
            // find the package
            var package = packages.SingleOrDefault(p => p.Id == id);
            if (package == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return new PackageResult(package.Status, Request);
        }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            //for testing
            GenerateStubs();
        }
        
        private static void GenerateStubs()
        {
            packages = new List<Package>
            {
                new Package
                {
                    Id = 1,
                    AccountNumber = Guid.NewGuid(),
                    Origin = "CA",
                    Destination = "TX",
                    StatusCode = 1,
                    Units = 111,
                    Weight = 2.5,
                    Created = DateTime.UtcNow,
                },
                new Package
                {
                    Id = 2,
                    AccountNumber = Guid.NewGuid(),
                    Origin = "AZ",
                    Destination = "AL",
                    StatusCode = 1,
                    Units = 2,
                    Weight = 1,
                    Created = DateTime.UtcNow.AddDays(-2),
                },
                new Package
                {
                    Id = 3,
                    AccountNumber = Guid.NewGuid(),
                    Origin = "FL",
                    Destination = "GA",
                    StatusCode = 3,
                    Units = 1,
                    Weight = 2.5,
                    Created = DateTime.UtcNow,
                }
            };
        }

        public override string GetModule()
        {
            throw new NotImplementedException();
        }
    }
}
