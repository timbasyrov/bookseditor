using System.Collections.Generic;
using System.Web.Optimization;

namespace BooksEditor
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/sb-admin-2.css",
                "~/Content/timeline.css",
                "~/Content/font-awesome.css",
                "~/Content/metisMenu.css",
                "~/Content/style.css")
            );

            bundles.Add(new ScriptBundle("~/bundles/main")
                .Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive.dynamic.js",
                "~/Scripts/modernizr-*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/underscore.js",
                "~/Scripts/sb-admin-2.js",
                "~/Scripts/ractive.js",
                "~/Scripts/metisMenu.js",
                "~/Scripts/utils/Url.js"
                ).ForceOrdered());

            bundles.Add(new ScriptBundle("~/bundles/routing").Include(
                "~/Scripts/routes/page.js",
                "~/Scripts/routes/route-configuration.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/app/extensions", "*.js", true)
                .IncludeDirectory("~/Scripts/app/decorators", "*.js", true)
                .IncludeDirectory("~/Scripts/app/components", "*.js", true)
                .Include("~/Scripts/app/App.js").ForceOrdered());
        }
    }

    internal class AsIsBundleOrder : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

    internal static class BundleExtensions
    {
        public static Bundle ForceOrdered(this Bundle sb)
        {
            sb.Orderer = new AsIsBundleOrder();
            return sb;
        }
    }
}