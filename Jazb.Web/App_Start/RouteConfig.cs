using System.Web.Mvc;
using System.Web.Routing;
using LowercaseRoutesMVC;

namespace Jazb.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");

            routes.MapRouteLowercase("Default", "{controller}/{action}/{id}", new
            {
                area = "",
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional,
            }, new[] { "Jazb.Web.Controllers" }
                );

       
        }
    }
}