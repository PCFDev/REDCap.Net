using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace PCF.REDCap.Web.Helpers
{
    public static partial class AntiForgeryHelpers
    {
        //Return something better than tuple?
        public static Tuple<HttpCookie, string> GetVerificationTokenContent(HttpCookieCollection cookies = null, string valueId = "VerificationToken", string cookieName = null)
        {
            cookieName = cookieName ?? AntiForgeryConfig.CookieName;//Use the default cookie name, do we want to set it against the root path or keep using our own path? (two cookie)
            var oldCookieToken = default(string);
            if (cookies != null)
            {
                if (cookies.AllKeys.Contains(cookieName))
                {
                    var oldCookie = cookies.Get(cookieName);//why does this create a cookie if it doesn't exist...
                    if (oldCookie != null)
                        oldCookieToken = oldCookie.Value;
                }
            }

            var tokens = GetTokens(oldCookieToken);
            var cookie = tokens.Item1 != null && tokens.Item1 != oldCookieToken ? new HttpCookie(cookieName, tokens.Item1) { HttpOnly = true, Secure = false } : default(HttpCookie);//TODO: Set cookie secure if we are always on secure, need to get that from a setting or something.
            var content = String.Format("<!DOCTYPE html><html><head><meta charset=\"utf-8\"><title>&#3232;_&#3232;</title></head><input type=\"hidden\" id=\"{0}\" value=\"{1}\"></html>", valueId, tokens.Item2);
            return Tuple.Create(cookie, content);
        }

        //Return something better than tuple?
        private static Tuple<string, string> GetTokens(string oldCookieToken = null)
        {
            string cookieToken, formToken;
            AntiForgery.GetTokens(oldCookieToken, out cookieToken, out formToken);
            return Tuple.Create(cookieToken, formToken);
        }
    }
}