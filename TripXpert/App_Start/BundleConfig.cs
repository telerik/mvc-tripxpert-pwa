using System.Web;
using System.Web.Optimization;

namespace TripXpert
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/kendo/2018.1.221/kendo.custom.min.js",
                "~/Scripts/tripxpert.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/kendo-theme.css",
                "~/Content/tripxpert.css"
            ));
        }
    }
}
