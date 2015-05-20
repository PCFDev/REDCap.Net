using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Owin.Security.ActiveDirectoryLDAP;

[assembly: OwinStartupAttribute(typeof(PCF.REDCap.Web.App_Start.Startup))]

namespace PCF.REDCap.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = "Session",
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //LoginPath = new PathString("/login"),
                Provider = new CookieAuthenticationProvider
                {
                    //TODO: Can we somehow access the domains from options or generate this from options instead of needing the list in both places?
                    OnValidateIdentity = LDAPAuthenticationProvider.OnValidateIdentity(MvcApplication.DomainCredentials, TimeSpan.FromMinutes(15))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseLDAPAuthentication(new LDAPAuthenticationOptions
            {
                Domains = MvcApplication.DomainCredentials,
                LoginPath = new PathString("/login"),
                RedirectPath = new PathString("/login-callback"),
            });
        }
    }
}