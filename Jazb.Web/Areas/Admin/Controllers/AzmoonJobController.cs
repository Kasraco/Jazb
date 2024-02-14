using Jazb.Datalayer.Context;
using Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Areas.Admin.Controllers
{
    public partial class AzmoonJobController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IAzmoonJobService _azmoonJobService;
        private readonly IValentearService _valentearService;
        public AzmoonJobController(IUnitOfWork uow, IAzmoonJobService AzmoonJobService, IValentearService ValentearService)
        {
            _uow = uow;
            _azmoonJobService = AzmoonJobService;
            _valentearService = ValentearService;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public virtual ActionResult SetChairNumber(int AzmoonId)
        {
            var lstModel = _azmoonJobService.GetAzmoonJobsModel(AzmoonId);
            return PartialView(MVC.Admin.AzmoonJob.Views._ChairNumber, lstModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public virtual ActionResult SetChairNumber(ChaireNumberViewModel model)
        {
            if(!ModelState.IsValid)
            {
               
            }
            _azmoonJobService.EditAzmoonJobs(model);
            _valentearService.SetChairNumber(model.AzmoonId);


            return RedirectToAction(MVC.Admin.AzmoonJob.ActionNames.SetChairNumber, MVC.Admin.AzmoonJob.Name, new { AzmoonId = model.AzmoonId });
        }
    }
}