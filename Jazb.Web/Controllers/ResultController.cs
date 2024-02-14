using Jazb.Datalayer.Context;
using Jazb.Model.ResultModel;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Controllers
{
    public partial class ResultController : Controller
    {

        //GET: /Result/
        private JazbDbContext db = new JazbDbContext();
        private readonly IReportService _reportService;
        private readonly IUnitOfWork _uow;

        public ResultController(IUnitOfWork uow, IReportService ReportService)
        {
            _reportService = ReportService;
        }

        public virtual ActionResult Index(int AID)
        {
            var qa = db.Azmoons.Find(AID);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanPrintResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);


            var ListAzmoonsDropdown = db.Azmoons.Where(x => x.Active.Equals(true)).ToList();


            ResultSearchNational RSN = new ResultSearchNational
            {
                AzmoonId = AID
            };


            return View(RSN);
        }
        public virtual ActionResult SearchResult(ResultSearchNational model)
        {
            var qa = db.Azmoons.Find(model.AzmoonId);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanPrintResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);


            FinalResultModel FRM = new FinalResultModel();
            if (model.AzmoonId > 0)
            {
                if (!string.IsNullOrEmpty(model.NationalId) && !string.IsNullOrWhiteSpace(model.NationalId))
                {
                    FRM = SearchByNationalId(model);

                    if (FRM == null)
                        return PartialView(MVC.Valentear.Views._errorPrintData);
                }
                else
                {
                    return RedirectToAction("Index");
                }


            }
            return View("searchresult", FRM);
        }

        public FinalResultModel SearchByNationalId(ResultSearchNational model)
        {
            FinalResultModel FRM = new FinalResultModel();
            var ARS = _reportService.GetAllValentearAnswer(model.AzmoonId, 5).Where(x => x.NationalID == model.NationalId).ToList();

            if (ARS.Count() > 0)
            {
                var ar = ARS.First();
                var Vs = db.Valentears.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.NationalID == model.NationalId).ToList();
                if (Vs.Count() > 0)
                {
                    var V = Vs.First();
                    string strresult = "قبول نشده";
                    if (string.IsNullOrEmpty(ar.StateResult))
                        strresult = "قبول نشده";

                    else if (string.IsNullOrWhiteSpace(ar.StateResult))
                        strresult = "قبول نشده";
                    else
                        strresult = ar.StateResult;

                    FRM = new FinalResultModel
                    {
                        FatherName = V.FatherName,
                        FirstName = V.FirstName,
                        LastName = V.LastName,
                        JobTitle = ar.JobName,
                        NationalId = V.NationalID,
                        StatuseTitle = strresult,
                        AzmoonId = model.AzmoonId,
                        ShomareShenasnameh = V.BirthCertificateNo
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var Vs = db.Valentears.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.NationalID == model.NationalId).ToList();
                if (Vs.Count() > 0)
                {
                    var V = Vs.First();
                    var jbs = db.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.AzmoonJobCode == V.JobId).ToList();
                    var jb = jbs.First();
                    FRM = new FinalResultModel
                    {
                        FatherName = V.FatherName,
                        FirstName = V.FirstName,
                        LastName = V.LastName,
                        JobTitle = jb.AzmoonJobTitle,
                        NationalId = V.NationalID,
                        StatuseTitle = "قبول نشده",
                        AzmoonId = model.AzmoonId,
                        ShomareShenasnameh = V.BirthCertificateNo
                    };
                }
                else
                {
                    FRM = new FinalResultModel
                    {
                        FatherName = "نامشخص",
                        FirstName = "نامشخص",
                        LastName = "نامشخص",
                        JobTitle = "نامشخص",
                        NationalId =model.NationalId,
                        StatuseTitle = "یافت نشد",
                        AzmoonId = model.AzmoonId,
                        ShomareShenasnameh = "نامشخص"
                    };
                }

            }
            return FRM;
        }




    }
}
