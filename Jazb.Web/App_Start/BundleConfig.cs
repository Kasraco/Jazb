using System.Web;
using System.Web.Optimization;

namespace Jazb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/toastr.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryAdmin").Include(
               "~/Scripts/jquery-{version}.js",
               "~/Scripts/jquery-migrate-{version}.js",
               "~/Scripts/jquery.unobtrusive-ajax.js",
               "~/Scripts/load.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuiAdmin").Include(
             "~/Scripts/jquery-ui-{version}.js",
             "~/Scripts/PersianCalender/calendar.js",
             "~/Scripts/PersianCalender/jquery.ui.datepicker-cc-fa.js",
             "~/Scripts/PersianCalender/jquery.ui.datepicker-cc.js"
             ));


            bundles.Add(new ScriptBundle("~/bundles/bootstrapAdmin").Include(
           "~/Scripts/bootstrap/bootstrap-rtl.js",
           "~/Scripts/toastr.js"
           //"~/Scripts/noty/jquery.noty.js",
           //"~/Scripts/noty/layouts/top.js",
           //"~/Scripts/noty/layouts/topCenter.js",
           //"~/Scripts/noty/themes/default.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuitoolsAdmin").Include(
               "~/Scripts/PersianCalender/calendar.js",
               "~/Scripts/PersianCalender/jquery.ui.datepicker-cc-fa.js",
               "~/Scripts/PersianCalender/jquery.ui.datepicker-cc.js",
               "~/Scripts/jquery-ui-1.10.2.autocomplete.js",
               "~/Scripts/jquery-validator-combined.js"
               ));

            bundles.Add(new StyleBundle("~/Content/themes/start/autocompleteandanimations").Include(
            "~/Content/themes/start/jquery-ui-1.10.2.autocomplete.css",
            "~/Content/animate.css",
            "~/Content/themes/start/jquery.ui.datepicker.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/redactor").Include(
             "~/Scripts/redactor/redactor.js"
             ));
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                "~/Scripts/chosen/chosen.jquery.js",
                "~/Scripts/adminjs.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/Select2js").Include(
                   "~/Scripts/Select2/select2.min.js"));

            bundles.Add(new ScriptBundle("~/Content/form_wizardjs").Include(
                       "~/Scripts/uniform/jquery.uniform.min.js",
                       "~/Scripts/bootstrap-wizard/jquery.bootstrap.wizard.min.js",
                       "~/Scripts/bootstrap-wizard/form-wizard.min.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                                                                  "~/Content/bootstrap-rtl.css",
                                                                  "~/Content/bootstrap-theme.css",
                                                                  "~/Content/themes/default.css",
                                                                  "~/Content/carousel.css",
                                                                  "~/Content/KRBCSS.css",
                                                                  "~/Content/toastr.css"
                                                                 ));
            bundles.Add(new StyleBundle("~/Scripts/Canendarjs").Include(
                                   "~/Scripts/PersianDatePicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/themes/base/Canendarcss").Include(
                       "~/Content/persianDatepicker-dark.css"));

            bundles.Add(new StyleBundle("~/Content/Select2Css").Include(
                      "~/Content/Select2/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/form_wizardCss").Include(
                      "~/Content/uniform/css/uniform.default.min.css",
                      "~/Content/bootstrap-wizard/wizard.css"));


            bundles.Add(new StyleBundle("~/Content/themes/start/css").Include(
                "~/Content/themes/start/jquery.ui.core.css",
                "~/Content/themes/start/jquery.ui.resizable.css",
                "~/Content/themes/start/jquery.ui.selectable.css",
                "~/Content/themes/start/jquery.ui.accordion.css",
                "~/Content/themes/start/jquery.ui.autocomplete.css",
                "~/Content/themes/start/jquery.ui.button.css",
                "~/Content/themes/start/jquery.ui.dialog.css",
                "~/Content/themes/start/jquery.ui.menu.css",
                "~/Content/themes/start/jquery.ui.slider.css",
                "~/Content/themes/start/jquery.ui.tabs.css",
                "~/Content/themes/start/jquery.ui.datepicker.css",
                "~/Content/themes/start/jquery.ui.progressbar.css",
                "~/Content/themes/start/jquery.ui.tooltip.css",
                "~/Content/themes/start/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}