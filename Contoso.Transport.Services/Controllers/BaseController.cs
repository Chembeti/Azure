using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Contoso.Transport.Services.Controllers
{
    public abstract class BaseController : ApiController
    {
        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <returns></returns>
        [Route("info/company")]
        public virtual string GetCompany()
        {
            return "Contoso Packaging LLC";
        }
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <returns></returns>
        [Route("info/version")]
        public virtual string GetVersion()
        {
            return "1.0.0.0";
        }
        /// <summary>
        /// Gets the module.
        /// </summary>
        /// <returns></returns>
        [Route("info/module")]
        public abstract string GetModule();
    }
}