using System;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using http = System.Web.Http;
using mvc = System.Web.Mvc;

//TODO: Get from Owin.Security.ActiveDirectoryLDAP solution.
namespace PCF.REDCap.Web.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ClaimAuthorizeAttribute : mvc.AuthorizeAttribute
    {
        private string ClaimType { get; set; }
        private string ClaimValue { get; set; }

        public ClaimAuthorizeAttribute(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var user = httpContext.User as ClaimsPrincipal;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (!user.HasClaim(ClaimType, ClaimValue))
            {
                return false;
            }

            return true;
        }
    }
}

namespace PCF.REDCap.Web.Http
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class ClaimAuthorizeAttribute : http.AuthorizeAttribute
    {
        private string ClaimType { get; set; }
        private string ClaimValue { get; set; }

        public ClaimAuthorizeAttribute(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            //var user = actionContext.ControllerContext.RequestContext.Principal as ClaimsPrincipal;
            var user = HttpContext.Current.User as ClaimsPrincipal;
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (!user.HasClaim(ClaimType, ClaimValue))
            {
                return false;
            }

            return true;
        }
    }
}
