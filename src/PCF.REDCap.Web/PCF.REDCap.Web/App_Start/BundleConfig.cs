﻿using System.Web.Optimization;
using PCF.REDCap.Web.Helpers;

namespace PCF.REDCap.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/combined.js").Include(
                        "~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/nprogress.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/custom.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr.js").Include(
                        "~/Scripts/modernizr-*"));

            var cssTransform = new CssRewriteUrlTransformFixed();
            bundles.Add(new StyleBundle("~/bundles/combined.css")
                        .Include("~/Content/bootstrap.css", cssTransform)
                        .Include("~/Content/theme.css", cssTransform)
                        .Include("~/Content/fonts.css", cssTransform)
                        .Include("~/Content/nprogress.css", cssTransform)
                        .Include("~/Content/custom.css", cssTransform));
        }
    }
}
