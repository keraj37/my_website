﻿using System.Web;
using System.Web.Optimization;

namespace my_website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/grid.css"));

            bundles.Add(new ScriptBundle("~/bundles/myscripts").Include(
                        "~/Scripts/Custom/my_css_and_anims.js",
                        "~/Scripts/Custom/utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/masonry").Include(
                       "~/Scripts/Masonry/masonry.pkgd.min.js",
                       "~/Scripts/Masonry/imagesloaded.pkgd.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/console_ajax").Include(
                        "~/Scripts/Custom/console_ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/cookies").Include(
                        "~/Scripts/Custom/cookies.js"));

            bundles.Add(new ScriptBundle("~/bundles/mandelbrot_ajax").Include(
                        "~/Scripts/Custom/mandelbrot_ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalr").Include(
                        "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/hubs").Include(
                        "~/Scripts/Custom/hubs.js"));
        }
    }
}
