using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Helpers
{
    public static class UrlGenerator
    {
        public static MvcHtmlString ReturnUrl(this HtmlHelper htmlHelper, HttpContextBase contextBase,
            UrlHelper urlHelper)
        {
            string currentUrl = contextBase.Request.RawUrl;
            if (currentUrl == "/")
            {
                currentUrl = urlHelper.Action(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            return MvcHtmlString.Create(currentUrl);
        }
    }
}