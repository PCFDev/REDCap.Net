using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new ClaimsIdentity(loginInfo.ExternalIdentity.Claims, DefaultAuthenticationTypes.ApplicationCookie));

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
    }
}