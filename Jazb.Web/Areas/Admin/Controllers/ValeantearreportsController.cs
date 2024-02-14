using Jazb.Datalayer.Context;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.DateAndTime;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jazb.DomainClasses.Entities;
using Jazb.Model.Report;

namespace Jazb.Web.Areas.Admin.Controllers
{
    public partial class ValeantearreportsController : Controller
    {

        private JazbDbContext context = new JazbDbContext();
        private readonly IUnitOfWork _uow;
        private readonly IValentearService _valentearService;
        private readonly IJobCityService _JobCityService;
        private readonly IAzmoon _azmoon;
        private readonly IOptionService _OptionService;
        private readonly IReportService _ReportService;
        private readonly IUserService _userService;

        public ValeantearreportsController(IUnitOfWork uow, IValentearService valentearService, IJobCityService JobCityService, IReportService ReportService,
                                           IAzmoon Azmoon, IOptionService OptionService , IUserService UserService)
        {
            _uow = uow;
            _valentearService = valentearService;
            _JobCityService = JobCityService;
            _azmoon = Azmoon;
            _OptionService = OptionService;
            _userService = UserService;
            _ReportService = ReportService;

           
        }


        //-------------------------------------------------
        public virtual ActionResult GetAllDevotion(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }

        public virtual ActionResult LoadReportSnapshot()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var devotionList = _valentearService.GetDevotions(Azmoonid);

          
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/rptValentears.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("CompanyName", opt.BlogName);
            report.Dictionary.Variables.Add("vrReportHeader", "لیست ایثارگران شرکت کننده");           
            report.Dictionary.Variables.Add("ReportDate", DateTime.Now.ToPersianDate());
            report.Dictionary.Variables.Add("UserName", user.UserMetaData.FirstName + " " + user.UserMetaData.LastName);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        //-------------------------------------------------


        //-------------------------------------------------
        public virtual ActionResult GetAllDevotionByGender(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }

        public virtual ActionResult LoadReportSnapshotByGender()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var devotionList = _valentearService.GetDevotions(Azmoonid);
            devotionList= devotionList.OrderBy(x => x.GenderName).ThenBy(x => x.JobName).ThenBy(x => x.PlaceName).ToList();
            context.ErrorCodes.Add(new DomainClasses.Entities.ErrorCode
            {
                FieldName = "KRBAzmmoonId",
                Melicode = Azmoonid.ToString(),
                TrackingCode = devotionList.Count().ToString(),
                value = User.Identity.Name
            });
            context.SaveChanges();
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/rptValentearsGroupGender.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("CompanyName", opt.BlogName);
            report.Dictionary.Variables.Add("vrReportHeader", "لیست ایثارگران شرکت کننده به تقکیک جنسیت");
            report.Dictionary.Variables.Add("ReportDate", DateTime.Now.ToPersianDate());
            report.Dictionary.Variables.Add("UserName", user.UserMetaData.FirstName + " " + user.UserMetaData.LastName);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        //-------------------------------------------------


        public virtual ActionResult GetAllValentearGroupBy(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }


        public virtual ActionResult LoadReportValentearGroupBy()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var devotionList = _valentearService.GetAllValentear(Azmoonid);
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/rptValentearsGroup.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("CompanyName", opt.BlogName);
            report.Dictionary.Variables.Add("vrReportHeader", "لیست کلیه داوطلبان به تفکیک رشته شغلی");
            report.Dictionary.Variables.Add("ReportDate", DateTime.Now.ToPersianDate());
            report.Dictionary.Variables.Add("UserName", user.UserMetaData.FirstName + " " + user.UserMetaData.LastName);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        //-------------------------------------------------

        //-------------------------------------------------


        public virtual ActionResult GetAllPasokhnamehValentearView(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }


        public virtual ActionResult LoadReportPasokhnamehValentear()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();

            var img = new System.Drawing.Bitmap(Server.MapPath(opt.CompanyLogo));
            byte[] array1 = imageToByteArray(img);

            MemoryStream ms = new MemoryStream(array1);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            var devotionList = _valentearService.GetAllCardValentear(Azmoonid);
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/Pasokhnameh.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("imgLogo", image);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }


        //-------------------------------------------------


        public virtual ActionResult GetAllCardValentearView(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }


        public virtual ActionResult LoadReportCardValentear()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();

            var img = new System.Drawing.Bitmap(Server.MapPath(opt.CompanyLogo));
            byte[] array1 = imageToByteArray(img);

            MemoryStream ms = new MemoryStream(array1);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            var devotionList = _valentearService.GetAllCardValentear(Azmoonid);
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/AzmoonCard.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("imgLogo", image);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        //-------------------------------------------------
        public virtual ActionResult GetAllChairNumberValentear(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }


        public virtual ActionResult LoadReportChairNumberValentear()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());

            var devotionList = _valentearService.GetAllChairNumberValentear(Azmoonid);
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/ChairNumber.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
           
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }

        //-------------------------------------------------

        /// <summary>
        /// به تفکیک جنسیت
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult GetAllValentearGroupByGender(int id)
        {
            Session["rptAzmoonId"] = id;
            return View();
        }


        public virtual ActionResult LoadReportValentearGroupByGender()
        {
            int Azmoonid = int.Parse(Session["rptAzmoonId"].ToString());
            var opt = _OptionService.GetAll();
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var devotionList = _valentearService.GetAllValentear(Azmoonid);
            devotionList = devotionList.OrderBy(x => x.GenderName).ThenBy(x => x.JobName).ThenBy(x => x.PlaceName).ToList();
            var report = new StiReport();
            report.Load(Server.MapPath("~/ReportsMRT/rptValentearsGroupGender.mrt"));
            report.RegBusinessObject("ValentearReport", devotionList);
            report.Dictionary.Variables.Add("CompanyName", opt.BlogName);
            report.Dictionary.Variables.Add("vrReportHeader", "لیست کلیه داوطلبان به تفکیک جنسیت");
            report.Dictionary.Variables.Add("ReportDate", DateTime.Now.ToPersianDate());
            report.Dictionary.Variables.Add("UserName", user.UserMetaData.FirstName + " " + user.UserMetaData.LastName);
            Session["rptAzmoonId"] = null;
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }
        //-------------------------------------------------
        

        //-----------------------------------------------------------------------------------------------------------------------------
        //------------------------- ______                       _     _______      ______              _ -----------------------------
        //-------------------------|  ____|                     | |    |__ __|     |  ____|            | |-----------------------------
        //-------------------------| |__ __  ___ __   ___ _ _  _| |_     | | ___   | |__ __  _____ ___ | |-----------------------------
        //-------------------------|  __| \ \/ / '_ \ / _ \| '__| __|    | |/ _ \  |  __| \ \/ / __/ _ \ |-----------------------------
        //-------------------------| |____ >  <| |_) | (_) | |  | |_     | | (_) | | |____ >  < (_|  __/ |-----------------------------
        //-------------------------|______/_/\_\ .__/ \___/|_|   \__|    |_|\___/  |______/_/\_\___\___|_|-----------------------------
        //-------------------------            | |                                                        -----------------------------
        //-------------------------            |_|                                                        -----------------------------
        //-----------------------------------------------------------------------------------------------------------------------------

        public virtual ActionResult ValentearFullExport(int AzmoonId)
        {
            string strName = string.Format("Export{0}.xlsx", AzmoonId);
            string strPath = string.Format("~/App_Data/{0}", strName);

            var lst = _ReportService.GetAllValentear(AzmoonId);
            ExternalData exD = new ExternalData(_uow);
            string serverAppDataPath = Server.MapPath(strPath);
            exD.ExportToExcel(lst, serverAppDataPath);
            this.SuccessMessage("اطلاعات به فرمت اکسل  ذخیره شد.");

          
            byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath(strPath));
            var mime = MimeMapping.GetMimeMapping(Server.MapPath(strPath));
            return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, strName);
        }

        public virtual ActionResult ValentearFullWithNumberExport(int AzmoonId)
        {

            string strName = string.Format("ExportWithNumber{0}.xlsx", AzmoonId);
            string strPath = string.Format("~/App_Data/{0}", strName);

            var lst = _ReportService.GetAllValentearWithNumber(AzmoonId);
            ExternalData exD = new ExternalData(_uow);
            string serverAppDataPath = Server.MapPath(strPath);
            exD.ExportToExcel(lst, serverAppDataPath);
            this.SuccessMessage("اطلاعات به فرمت اکسل  ذخیره شد.");

            
            byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath(strPath));
            var mime = MimeMapping.GetMimeMapping(Server.MapPath(strPath));
            return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, strName);
        }

        public virtual ActionResult SavePicturesValentera(int AzmoonId)
        {
            var lst = _ReportService.GetAllAcceptPictureValentear(AzmoonId);
            string strPath = "";
            foreach (var t in lst)
            {
                strPath = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data\\Images\\" + t.PictureValen);
                MemoryStream ms = new MemoryStream(t.PictureValentear);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                image.Save(strPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            }

            return RedirectToAction(MVC.Admin.Valeantearreports.ActionNames.ValentearAcceptExport, new { AzmoonId = AzmoonId });
        }

        public virtual ActionResult ValentearAcceptExport(int AzmoonId)
        {

            string strName = string.Format("ExportAccept{0}.xlsx", AzmoonId);
            string strPath = string.Format("~/App_Data/{0}", strName);

            var lst = _ReportService.GetAllAcceptValentear(AzmoonId);
            ExternalData exD = new ExternalData(_uow);
            string serverAppDataPath = Server.MapPath(strPath);
            exD.ExportToExcel(lst, serverAppDataPath);
            this.SuccessMessage("اطلاعات به فرمت اکسل  ذخیره شد.");

            byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath(strPath));
            var mime = MimeMapping.GetMimeMapping(Server.MapPath(strPath));
            return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, strName);
        }

        public virtual ActionResult ValentearAnswerFinalExport(int AzmoonId,int Grade)
        {

            string strName = string.Format("ExportResult{0}.xlsx", AzmoonId);
            string strPath = string.Format("~/App_Data/{0}", strName);

            var lst = _ReportService.GetAllValentearAnswer(AzmoonId, Grade);

            ExternalData exD = new ExternalData(_uow);
            string serverAppDataPath = Server.MapPath(strPath);
            exD.ExportToExcel(lst, serverAppDataPath);
            this.SuccessMessage("اطلاعات به فرمت اکسل  ذخیره شد.");

            byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath(strPath));
            var mime = MimeMapping.GetMimeMapping(Server.MapPath(strPath));
            return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, strName);
        }




        public virtual ActionResult ViewerEvent()
        {
            return StiMvcViewer.ViewerEventResult(this.HttpContext);
        }

        public virtual ActionResult PrintReport()
        {
            return StiMvcViewer.PrintReportResult(this.HttpContext);
        }

        public virtual ActionResult ExportReport()
        {
            return StiMvcViewer.ExportReportResult(this.HttpContext);
        }


        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {


            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imageIn.RawFormat);
            return ms.ToArray();
        }

    }

}