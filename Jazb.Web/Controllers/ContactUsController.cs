using System.Web.Mvc;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.DateAndTime;
using Jazb.Web.HtmlCleaner;

namespace Jazb.Web.Controllers
{
    public partial class ContactUsController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;

        public ContactUsController(IUnitOfWork uow, IMessageService messageService, IUserService userService)
        {
            _uow = uow;
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public virtual ActionResult Submit(FAQ model)
        {
            if (!ModelState.IsValid)
                return PartialView(MVC.Shared.Views._ValidationSummery, model);

            _messageService.Add(new FAQ
            {
                QuestionDate = DateAndTime.GetDateTime(),
                Body = model.Body.ToSafeHtml(),
                Subject = model.Subject,
                FirstName =model.FirstName,
                LastName =model.LastName,
                MeliCode=model.MeliCode              

            });
            _uow.SaveChanges();

            return PartialView(MVC.Shared.Views._Alert,
                new Alert { Mode = AlertMode.Success, Message = "پیغام شما با موفقیت برای ما ارسال شد." });
        }
    }
}