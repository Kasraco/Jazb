using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel.ManageAmCard;
using Jazb.Servicelayer.EFServices;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Areas.Admin.Controllers
{
    public partial class ManageAmountCardController : Controller
    {
        // GET: Admin/ManageAmountCard

        private readonly IAmountCardService _amountCardService; 
        private readonly IUnitOfWork _uow; 
  

        private static IDictionary<string, int> tasks = new Dictionary<string, int>();
        private string taskId;
        public ManageAmountCardController(IUnitOfWork uow,IAmountCardService AmountCardService)
        {
            _uow = uow;
            _amountCardService = AmountCardService;

            taskId = "strValv";
            if (tasks.Count() > 0)
                tasks.Remove(taskId);

           tasks.Add(taskId, 0);
        }

        [HttpGet]
        public virtual ActionResult AddAmondCard(int AzmoonId)
        {
            AddAmountCardViewModel model = new AddAmountCardViewModel
            {
                AzmoonId = AzmoonId
            };

            return PartialView(MVC.Admin.ManageAmountCard.Views._AddAmondCardView, model);
        }
        
        [HttpPost]
        public virtual ActionResult AddAmondCard(AddAmountCardViewModel model)
        {
            GenerateUniqCode GUC = new GenerateUniqCode();
            List<AmountCard> lst = new List<AmountCard>();
            for (var i = 0; i <= model.AmountCardCount; i++)
            {
                tasks[taskId] = i; // update task progress
                AmountCard amcm = new AmountCard
                {
                    Amount = model.Amount,
                    AmountSerial = GUC.GetPaymentUniqCode(12),
                    AzmoonId = model. AzmoonId
                };

                lst.Add(amcm);               
            }

            _amountCardService.Add(lst);

          this.SuccessMessage(string.Format("{0} کارت خرید صادر شد. ", model.AmountCardCount));
            return RedirectToAction(MVC.Admin.ManageAmountCard.ActionNames.AddAmondCard, new { AzmoonId = model.AzmoonId });
        }


     

    }
}