using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Contoso.Transport.Services.Models;
using System.Net;
using System.Net.Security;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System.Net.Http.Headers;

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

        [TestMethod]
        public async Task FindPackageByIdSecureNotNullTest()
        {
            const int Packageid = 1;
            // Enable this  for fiddler
            var handler = new HttpClientHandler
            {
                CookieContainer = new CookieContainer(),
                UseCookies = true,
                UseDefaultCredentials = false,
                Proxy = new WebProxy("http://localhost:17337", true),
                UseProxy = true,
            };

            // Disable Https endpoint validation for local environment
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            try
            {
                // Get the client token
                var authtoken = await this.GetAuthTokenAsync();

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // add the authorization header
                    client.DefaultRequestHeaders.Add("Authorization", authtoken);

                    // get the response
                    var response = await client.GetStringAsync(string.Format("{0}/api/package/{1}", ConfigurationManager.AppSettings["baseurl"], Packageid));
                    //var response = await client.GetStringAsync(string.Format("{0}/api/package/{1}/status", ConfigurationManager.AppSettings["baseurl"], Packageid));
                    Assert.IsNotNull(response);
                    Console.WriteLine(response);
                    //var package = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Package>(response));
                    var package1 = JsonConvert.DeserializeObject<Package>(response);
                    Assert.IsNotNull(package1);
                    Assert.AreEqual(Packageid, package1.Id);
                }
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }
        private async Task<string> GetAuthTokenAsync()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            // Create the Azure AD auth context based on the directory tenant
            var authContext = new AuthenticationContext(
                                    string.Format(
                                        "https://login.windows.net/{0}",
                                        ConfigurationManager.AppSettings["ida.tenant"]));
            // Get token by prompting the user for credentials
            // Alternatively use AcquireTokenSilent to pass in user credentials without prompting
            var result = authContext.AcquireToken(
                ConfigurationManager.AppSettings["ida.webAPIAppIdURI"],
                ConfigurationManager.AppSettings["ida.nativeclientId"],
                new Uri(ConfigurationManager.AppSettings["ida.nativeAppRedirectUri"]),
                PromptBehavior.Auto);
            // get the authorization header
            return result.CreateAuthorizationHeader();
        }


    }
}
