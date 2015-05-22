using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PCF.REDCap.Web.Attributes
{
    /// <summary>
    /// Specifies that a controller or controller method requires a valid anti-forgery cookie/header pair.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateAntiForgeryHeaderAttribute : ActionFilterAttribute
    {
        public const string DefaultCookieNotPresentMessage = "The required anti-forgery cookie \"{0}\" is not present.";
        public const string DefaultHeaderName = "X-CSRF-Token";
        public const string DefaultHeaderNotPresentMessage = "The required anti-forgery header \"{0}\" is not present.";
        public const string DefaultNotValidMessage = "The required anti-forgery header \"{0}\" is invalid.";

        public ValidateAntiForgeryHeaderAttribute()
        {
            this.CookieName = AntiForgeryConfig.CookieName;
            this.HeaderName = DefaultHeaderName;
            this.CookieNotPresentMessage = DefaultCookieNotPresentMessage;
            this.HeaderNotPresentMessage = DefaultHeaderNotPresentMessage;
            this.NotValidMessage = DefaultNotValidMessage;
        }

        public string CookieName { get; set; }
        public string CookieNotPresentMessage { get; set; }
        public string HeaderName { get; set; }
        public string HeaderNotPresentMessage { get; set; }
        public string NotValidMessage { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            var cookie = headers.GetCookies().Select(_ => _[CookieName]).FirstOrDefault();
            var cookieToken = cookie == null ? default(string) : cookie.Value;

            if (String.IsNullOrEmpty(cookieToken))
                actionContext.ModelState.AddModelError(String.Empty, String.Format(CookieNotPresentMessage, CookieName));
            else
            {
                var headerToken = headers.GetValues(HeaderName).FirstOrDefault();
                if (String.IsNullOrWhiteSpace(headerToken))
                    actionContext.ModelState.AddModelError(String.Empty, String.Format(HeaderNotPresentMessage, HeaderName));
                else
                {
                    try
                    {
                        AntiForgery.Validate(cookieToken, headerToken);//How many times can a token be re-used? Is there any sort of mechanism to prevent re-use?
                    }
                    catch (System.Web.Mvc.HttpAntiForgeryException)
                    {
                        actionContext.ModelState.AddModelError(String.Empty, String.Format(NotValidMessage, HeaderName));
                    }
                }
            }

            if (!actionContext.ModelState.IsValid)
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
        }
    }
}