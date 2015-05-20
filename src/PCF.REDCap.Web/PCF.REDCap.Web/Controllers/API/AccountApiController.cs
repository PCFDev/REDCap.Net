using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PCF.REDCap.Web.Controllers.API
{
    [Authorize]
    public class AccountApiController : ApiController
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        [HttpPost]//, ValidateAntiForgeryHeader]
        public HttpResponseMessage Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Request.CreateResponse(HttpStatusCode.OK, new { Success = true });
        }
    }
}