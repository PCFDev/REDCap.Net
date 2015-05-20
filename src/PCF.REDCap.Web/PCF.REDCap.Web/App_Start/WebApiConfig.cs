using System;
using System.Web.Http;

namespace PCF.REDCap.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            RegisterAccountRoutes(config.Routes);
            RegisterConfigRoutes(config.Routes);

            config.Routes.MapHttpRoute(
                name: "Test",
                routeTemplate: "api/v1/tests/{id}",
                defaults: new { controller = "TestApi", id = RouteParameter.Optional }
            );
        }

        private static void RegisterAccountRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "API/Account/Logout",
                routeTemplate: "api/v1/account/logout",
                defaults: new { controller = "AccountApi", action = "Logout" }
            );
        }

        private static void RegisterConfigRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "API/Configs",
                routeTemplate: "api/v1/configs/{id}",
                defaults: new { controller = "ConfigApi", id = RouteParameter.Optional }
            );

            //routes.MapHttpRoute(
            //    name: "API/Configs/Get",
            //    routeTemplate: "api/v1/configs",
            //    defaults: new { controller = "ConfigApi", action = "Get" }
            //);

            //routes.MapHttpRoute(
            //    name: "API/Configs/Enable",
            //    routeTemplate: "api/v1/configs/{id}/enable",
            //    defaults: new { controller = "ConfigApi", action = "Enable" }
            //);

            //routes.MapHttpRoute(
            //    name: "API/Configs/Disable",
            //    routeTemplate: "api/v1/configs/{id}/disable",
            //    defaults: new { controller = "ConfigApi", action = "Disable" }
            //);

            //routes.MapHttpRoute(
            //    name: "API/Configs/Delete",
            //    routeTemplate: "api/v1/configs/{id}/delete",
            //    defaults: new { controller = "ConfigApi", action = "Delete" }
            //);
        }
    }
}
