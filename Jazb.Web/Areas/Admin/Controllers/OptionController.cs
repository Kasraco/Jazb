using System.Web.Mvc;
using Jazb.Datalayer.Context;
using Jazb.Model;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Caching;
using Jazb.Web.Filters;

namespace Jazb.Web.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin")]
    public partial class OptionController : Controller
    {
        private readonly IOptionService _optionService;
        private readonly IUnitOfWork _uow;

        public OptionController(IUnitOfWork uow, IOptionService optionService)
        {
            _uow = uow;
            _optionService = optionService;
        }

        public virtual ActionResult Index()
        {
            return PartialView(MVC.Admin.Option.Views._Index, _optionService.GetAll());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public virtual ActionResult Index(SiteConfig model)
        {
            _optionService.Update(model);
            _uow.SaveChanges();
            JazbCache.RemoveSiteConfig(HttpContext);
            return PartialView(MVC.Admin.Shared.Views._Alert, new Alert
            {
                Message = "تنظیمات سایت با موفقیت به روز رسانی شد",
                Mode = AlertMode.Success
            });
        }
    }
}