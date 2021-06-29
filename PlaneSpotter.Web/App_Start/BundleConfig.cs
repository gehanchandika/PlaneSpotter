using System.Web;
using System.Web.Optimization;

namespace PlaneSpotter.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/lib/bootstrap.js",
                      "~/Scripts/lib/bootstrap-datepicker.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/knockoutjs").Include(
                "~/Scripts/lib/knockout-3.5.1.js",
                "~/Scripts/lib/knockout-3.5.1.debug.js"));

            bundles.Add(new ScriptBundle("~/bundles/momentjs").Include(
                "~/Scripts/lib/moment.js.js",
                "~/Scripts/lib/moment-with-locales.js"));


            var jsBundle = new ScriptBundle("~/bundles/customscripts");
            jsBundle.Include(
                "~/Scripts/app/common/Common.js",
                "~/Scripts/app/common/CustomBindings.js");
            bundles.Add(jsBundle);
        }
    }
}
