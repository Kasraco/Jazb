using System.Web.Mvc;
using Jazb.Datalayer.Context;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.Mail;

namespace Jazb.Web.Controllers
{
    public partial class MailController : Controller
    {
        private readonly IForgottenPasswordService _forgttenPasswordService;
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;

        public MailController(IUnitOfWork uow, IUserService userService,
            IForgottenPasswordService forgottenPasswordService)
        {
            _uow = uow;
            _userService = userService;
            _forgttenPasswordService = forgottenPasswordService;
        }

        public virtual ActionResult Index()
        {
            string m = "";

            return Content(m);
        }
    }
}