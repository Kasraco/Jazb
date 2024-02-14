using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using CaptchaMvc.Infrastructure;
using Jazb.Datalayer.Context;
using Jazb.Datalayer.Migrations;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Binders;
using MvcSiteMapProvider.Web;
using StructureMap;
using Jazb.Web.DependencyResolution;

namespace Jazb.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());

            //Database.SetInitializer<JazbDbContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JazbDbContext, Configuration>());

            //       MiniProfilerEF.InitializeEF42();

            XmlSiteMapController.RegisterRoutes(RouteTable.Routes); // <-- register sitemap.xml, add this line of code

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Remove Extra ViewEngins
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            MvcHandler.DisableMvcResponseHeader = true;
            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
            Jazb.Web.Infrastructure.AutoMapperWebConfiguration.Configure();

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (Context.User == null)
                return;

            var userService = ObjectFactory.GetInstance<IUserService>();

            UserStatus userStatus = userService.GetStatus(Context.User.Identity.Name);

            if (userStatus.IsBaned)
                FormsAuthentication.SignOut();

            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
                return;

            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);


            // retrieve roles from UserData
            if (authTicket == null) return;
            string[] roles = authTicket.UserData.Split(',');

            if (userStatus.Role != roles[0])
                FormsAuthentication.SignOut();

            Context.User = new GenericPrincipal(Context.User.Identity, roles);
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            
            var app = sender as HttpApplication;
            if (app == null || app.Context == null)
                return;
            app.Context.Response.Headers.Remove("Server");
        }

        private void Application_EndRequest(object sender, EventArgs e)
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }


        void Application_Error(object sender, EventArgs e)
        {
            //var app = sender as HttpApplication;
            //app.Context.Items["_Error"] = true;
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax", true);
            }
        }


    }
}