using System.Web;
using System.Web.Optimization;

namespace maps.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/fineuploader")
                    .Include("~/Scripts/jquery.fineuploader-4.1.1.min.js"));

            bundles.Add(new StyleBundle("~/Content/css/fineuploader")
                             .Include("~/Content/css/fineuploader-4.1.1.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/style.css"));
            bundles.Add(new StyleBundle("~/Content/css/admin").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/PagedList.css",
                "~/Content/css/admin.css"));
        }
    }
}
