using Jazb.Web.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); 
            filters.Add(new HandleExceptionAttribute());
        }
    }
}