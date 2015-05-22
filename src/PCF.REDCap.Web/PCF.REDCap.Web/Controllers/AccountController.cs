using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PCF.REDCap.Web.Helpers;

namespace PCF.REDCap.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null || loginInfo.ExternalIdentity == null || !loginInfo.ExternalIdentity.IsAuthenticated)
                return RedirectToAction("Login");

            var claims = loginInfo.ExternalIdentity.Claims.Where(_ => _.Type != ClaimTypes.GroupSid || _.Value == "S-1-5-21-620581126-1294811165-624655392-31026");

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie));

            var redirectUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";
            return Redirect(redirectUrl);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
            //{
            //    var redirectUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : "/";
            //    return Redirect(redirectUrl);
            //}
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        [AllowAnonymous, HttpGet]
        public ContentResult Token()
        {
            //We never want this cached, this may be overkill.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Expires", "-1");
            Response.AppendHeader("Pragma", "no-cache");

            var frameOptions = Response.Headers.Get("X-Frame-Options");
            if (frameOptions == null || !frameOptions.Equals("SameOrigin", StringComparison.OrdinalIgnoreCase))
                Response.Headers["X-Frame-Options"] = "SameOrigin";//Make sure it is set to SameOrigin.

            var tokens = AntiForgeryHelpers.GetVerificationTokenContent(Request.Cookies);
            if (tokens.Item1 != default(HttpCookie))
                Response.SetCookie(tokens.Item1);
            return Content(tokens.Item2, "text/html", Encoding.UTF8);
        }
    }
}