using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;

namespace Contoso.Transport.Services
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
            };

            app.UseWindowsAzureActiveDirectoryBearerAuthentication(new WindowsAzureActiveDirectoryBearerAuthenticationOptions
            {
                TokenValidationParameters = validationParameters,
                Tenant = ConfigurationManager.AppSettings["ida:Tenant"]
            });
        }
    }
}