using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Contoso.Transport.Services.Models;

namespace Contoso.Transport.Services.Tests
{
    [TestClass]
    public class PackageControllerTest
    {
        [TestMethod]
        public async Task FindPackageByIdNotNullTest()
        {
            var packageid = 1;
            var packageUrl = string.Format("http://localhost:17337/api/package/{0}", packageid);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(packageUrl);
                Assert.IsNotNull(response);
                var package = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Package>(response));
                Assert.IsNotNull(package);
                Assert.AreEqual(packageid, package.Id);
            }
        }
    }
}
