using System.Web.Mvc;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Filters;
using Jazb.DomainClasses.Entities;
using Jazb.Datalayer.Context;
using System.Data.Entity;
using System.Linq;

namespace Jazb.Web.Areas.Admin.Controllers
{
    [SiteAuthorize(Roles = "admin,moderator")]
    public partial class ContactUsController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUnitOfWork _uow;
        public ContactUsController(IUnitOfWork uow, IMessageService messageService)
        {
            _uow = uow;
            _messageService = messageService;
        }

        public virtual ActionResult Index()
        {
            return PartialView(MVC.Admin.ContactUs.Views._Index, _messageService.GetAll());
        }

        public virtual ActionResult Detail(int id)
        {
            var qfaq=_messageService.Find(id);
            ViewBag.PossibleBody = qfaq.Body;
            return PartialView(MVC.Admin.ContactUs.Views._Detail, qfaq);
        }


        public virtual ActionResult GetByAzmoonId(int AzmoonId)
        {
            return PartialView(MVC.Admin.ContactUs.Views._Index, _messageService.GetAll(AzmoonId));
        }


        public virtual ActionResult GetNoneAnswerByAzmoonId(int AzmoonId)
        {
            return PartialView(MVC.Admin.ContactUs.Views._noneAnswer, _messageService.GetAllNonAnswer(AzmoonId));
        }

        public virtual ActionResult GetAnswerByAzmoonId(int AzmoonId)
        {
            return PartialView(MVC.Admin.ContactUs.Views._AllAnswerd, _messageService.GetAllAnswer(AzmoonId));
        }

        public virtual ActionResult NextFaqs(int AzmoonId,int id)
        {
            var qresult = new FAQ();
            var q = _messageService.GetAllNonAnswer(AzmoonId);
            var q1 = q.Where(x => x.Id > id).OrderByDescending(x=>x.QuestionDate).Take(1).ToList();
            if (q1.Count()== 0)
            {
                qresult.AzmoonId = q.First().AzmoonId;
                qresult.Body = q.First().Body;
                qresult.BodyAnswer = q.First().BodyAnswer;
                qresult.FirstName = q.First().FirstName;
                qresult.Id = q.First().Id;
                qresult.LastName = q.First().LastName;
                qresult.MeliCode = q.First().MeliCode;
                qresult.QuestionDate = q.First().QuestionDate;
                qresult.Subject = q.First().Subject;
            }
            else
            {
                qresult.AzmoonId = q1.First().AzmoonId;
                qresult.Body = q1.First().Body;
                qresult.BodyAnswer = q1.First().BodyAnswer;
                qresult.FirstName = q1.First().FirstName;
                qresult.Id = q1.First().Id;
                qresult.LastName = q1.First().LastName;
                qresult.MeliCode = q1.First().MeliCode;
                qresult.QuestionDate = q1.First().QuestionDate;
                qresult.Subject = q1.First().Subject;
            }
            ViewBag.PossibleBody = qresult.Body;
            return PartialView(MVC.Admin.ContactUs.Views._Detail, qresult);
        }

        public virtual ActionResult PreviuseFaqs(int AzmoonId, int id)
        {
            var qresult = new FAQ();
            var q = _messageService.GetAllNonAnswer(AzmoonId);
            var q1 = q.Where(x => x.Id < id).OrderByDescending(x => x.QuestionDate).Take(1).ToList();
            if (q1.Count() == 0)
            {
                qresult.AzmoonId = q.First().AzmoonId;
                qresult.Body = q.First().Body;
                qresult.BodyAnswer = q.First().BodyAnswer;
                qresult.FirstName = q.First().FirstName;
                qresult.Id = q.First().Id;
                qresult.LastName = q.First().LastName;
                qresult.MeliCode = q.First().MeliCode;
                qresult.QuestionDate = q.First().QuestionDate;
                qresult.Subject = q.First().Subject;
            }
            else
            {
                qresult.AzmoonId = q1.First().AzmoonId;
                qresult.Body = q1.First().Body;
                qresult.BodyAnswer = q1.First().BodyAnswer;
                qresult.FirstName = q1.First().FirstName;
                qresult.Id = q1.First().Id;
                qresult.LastName = q1.First().LastName;
                qresult.MeliCode = q1.First().MeliCode;
                qresult.QuestionDate = q1.First().QuestionDate;
                qresult.Subject = q1.First().Subject;
            }
            ViewBag.PossibleBody = qresult.Body;
            return PartialView(MVC.Admin.ContactUs.Views._Detail, qresult);
        }


        [HttpPost]
        public virtual ActionResult SaveDate(FAQ model)
        {
            _messageService.Edit(model);
            _uow.SaveChanges();
            return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "پاسخ ارسال شد", Mode = AlertMode.Success });
        }
    }
}