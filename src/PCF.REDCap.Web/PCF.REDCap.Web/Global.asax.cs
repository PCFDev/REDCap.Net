using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Owin.Security.ActiveDirectoryLDAP;

namespace PCF.REDCap.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;//Should we just set the identityprovider claim?
        }

        private static IList<DomainCredential> s_DomainCredentials { get; set; }
        internal static IList<DomainCredential> DomainCredentials
        {
            get
            {
                if (s_DomainCredentials == null)
                {
                    s_DomainCredentials = new List<DomainCredential>();
                    foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
                    {
                        if (connection.ProviderName == "ActiveDirectoryLDAP")
                            s_DomainCredentials.Add(new DomainCredential(connection.Name, connection.ConnectionString));
                    }
                }
                return s_DomainCredentials;
            }
        }
    }
}
