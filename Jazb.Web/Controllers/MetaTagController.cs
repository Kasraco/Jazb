using System.Web.Mvc;
using Jazb.Model;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Caching;

namespace Jazb.Web.Controllers
{
    public partial class MetaTagController : Controller
    {
        private readonly IOptionService _optionService;

        public MetaTagController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        public virtual ActionResult Index(string title, string keywords, string description)
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            ViewBag.Title = !string.IsNullOrEmpty(title)
                ? string.Format("{0} - {1}", title, siteConfig.BlogName)
                : siteConfig.BlogName;

            ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;

            return PartialView();
        }


        public virtual ActionResult GetCompanyAddress()
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            return Content(siteConfig.CompanyAddress);
        }

        public virtual ActionResult GetCompanyName()
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            return Content(siteConfig.BlogName);
        }

        public virtual ActionResult GetCompanyCompanyLogo()
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            return Content(siteConfig.CompanyLogo);
        }

        public virtual ActionResult GetCompanyURL()
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            return Content(siteConfig.SiteUrl);
        }
    }
}