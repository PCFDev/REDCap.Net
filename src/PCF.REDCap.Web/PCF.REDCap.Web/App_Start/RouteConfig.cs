using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace PCF.REDCap.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            RegisterAccountRoutes(routes);
            RegisterConfigRoutes(routes);
        }

        private static void RegisterAccountRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Account/Login",
                url: "login",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Account/Logout",
                url: "logout",
                defaults: new { controller = "Account", action = "Logout" }
            );

            routes.MapRoute(
                name: "Account/ExternalLoginCallback",
                url: "login-callback",
                defaults: new { controller = "Account", action = "ExternalLoginCallback" }
            );
        }

        private static void RegisterConfigRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "Config/Index",
                url: "",
                defaults: new { controller = "Config", action = "Index" }
            );
        }
    }
}
