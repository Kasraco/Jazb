using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Jazb.Datalayer.Context;
using PagedList;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Servicelayer.Interfaces;
using Jazb.Model.AdminModel;
using Jazb.Web.Infrastructure;
using Jazb.Utilities.DateAndTime;
using Jazb.Web.Caching;
using Jazb.Model;
using System.ComponentModel;

namespace Jazb.Web.Areas.Admin.Controllers
{
   
    public partial class AzmoonsController : Controller
    {

          private readonly IUnitOfWork _uow;
          private readonly IAzmoon _azmoonService;
        private readonly IOptionService _optionService;
        public AzmoonsController(IUnitOfWork uow, IAzmoon azmoonService, IOptionService optionService)
        {
            _uow = uow;
              
        _optionService = optionService;

            _azmoonService = azmoonService;
        }

        private JazbDbContext context = new JazbDbContext();

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult Index()
        {
            return PartialView(MVC.Admin.Azmoons.Views._Index);
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult NavBar()
        {
            return PartialView(MVC.Admin.Azmoons.Views._Nav);
        }

        //
        // GET: /Azmoons/

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult DataTable(string term = "", int page = 0, int count = 10,
        Order order = Order.Descending, AzmoonOrderBy orderBy = AzmoonOrderBy.StartDate,
        AzmoonSearchBy searchBy = AzmoonSearchBy.Active)
        {
            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;

            IList<AzmoonDataTableModel> selectAzmoon = _azmoonService.GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _azmoonService.Count : selectAzmoon.Count;

            return PartialView(MVC.Admin.Azmoons.Views._DataTable, selectAzmoon);
        }

        //
        // GET: /Azmoons/Details/5
        [Authorize(Roles = "admin,moderator")]
        public virtual ViewResult Details(int id)
        {
            Azmoon azmoon = context.Azmoons.Single(x => x.AzmoonId == id);
            return View(azmoon);
        }

        //
        // GET: /Azmoons/Create
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult Create()
        {
          
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

        ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;

        

            return PartialView(MVC.Admin.Azmoons.Views._Add);
        }

        //
        // POST: /Azmoons/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult Create(AzmoonModel azmoon)
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;
            

            KRBUpload KU=new KRBUpload();
            string ms = "";
            var fileAghiFileData = Request.Files["AghiFileData2"];

            var filePictutreAzmoon = Request.Files["PicturAzmoon2"];

            if (fileAghiFileData == null)
            {
                ms = "شما فایل آگهی آزمون را ارسال نکرده اید";
                this.ErrorMessage(ms);
                return Content(ms);
            }

            AllowUploadStatuse AUS_AghiFileData = KU.AllowUploadSpecialFilesOnly(fileAghiFileData);
            if (AUS_AghiFileData == AllowUploadStatuse.NotDefaineExtension)
            { 
                ms = "فایل انتخابی شما قابل شناسایی نیست";
                this.ErrorMessage(ms);
                return Content(ms);
            }

            if (AUS_AghiFileData == AllowUploadStatuse.HaveNotAllowUpload)
            {
                ms = "شما اجازه بارگزاری این فایل را ندارید";
                this.ErrorMessage(ms);
                return Content(ms);
            }

            if (AUS_AghiFileData == AllowUploadStatuse.DoNotHaveContentFile)
            {
                ms = "فایل انتخابی شما قابل خواندن نیست";
                this.ErrorMessage(ms);
                return Content(ms);
            }

            if (AUS_AghiFileData == AllowUploadStatuse.UploadSuccessFully)
            {
                fileAghiFileData.SaveAs(Server.MapPath("~/App_Data/") + fileAghiFileData.FileName);
                azmoon.AghiFileData = KU.ConvertToArray(fileAghiFileData, 0, 0).ImageFile;
                azmoon.AghiFileDataContentType = fileAghiFileData.ContentType;
                azmoon.AghiFileDataFileName = fileAghiFileData.FileName;
            }

            if (filePictutreAzmoon != null)
            {
                AllowUploadStatuse AUS_filePictutreAzmoon = KU.AllowUploadSpecialFilesOnly(filePictutreAzmoon);
                if (AUS_filePictutreAzmoon == AllowUploadStatuse.NotDefaineExtension)
                {
                    ms = "فایل انتخابی شما قابل شناسایی نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.HaveNotAllowUpload)
                {
                    ms = "شما اجازه بارگزاری این فایل را ندارید";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.DoNotHaveContentFile)
                {
                    ms = "فایل انتخابی شما قابل خواندن نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.UploadSuccessFully)
                {

                   
                    azmoon.PicturAzmoon = KU.ConvertToArray(filePictutreAzmoon, 0, 0).ImageFile;
                    azmoon.PicturAzmoonContentType = filePictutreAzmoon.ContentType;
                    azmoon.PicturAzmoonFileName = filePictutreAzmoon.FileName;
                }
            }
            else
            {
                ms = "شما فایل بنر آزمون را ارسال نکرده اید";
                this.ErrorMessage(ms);
                return Content(ms);
            }

           
            if (ModelState.IsValid)
            {
                _azmoonService.AddAzmoon(azmoon);
                _uow.SaveChanges();
            }

            this.ErrorMessage("آزمون جدید ایجاد شد");
            return RedirectToAction(MVC.Admin.Azmoons.ActionNames.Index, MVC.Admin.Azmoons.Name);
        }

        //
        // GET: /Azmoons/Edit/5
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult Edit(int id)
        {
            
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;

            Azmoon azmoon = context.Azmoons.Single(x => x.AzmoonId == id);
            AzmoonEditModel am = new AzmoonEditModel
            {
                Active= azmoon.Active,
                EveryOneSee=azmoon.EveryOneSee,
                AghiFileData= azmoon.AghiFileData,
                AghiFileDataContentType= azmoon.AghiFileDataContentType,
                AghiFileDataFileName= azmoon.AghiFileDataFileName,
                AmountValue= azmoon.AmountValue,
                AzmoonId= azmoon.AzmoonId,
                PicturAzmoon= azmoon.PicturAzmoon,
                DateExecuteAzmoon= azmoon.DateExecuteAzmoon.ToPersianDate(),
                LocationExecuteAzmoon= azmoon.LocationExecuteAzmoon,
                PicturAzmoonContentType= azmoon.PicturAzmoonContentType,
                PicturAzmoonFileName= azmoon.PicturAzmoonFileName,
                TimeExecuteAzmoon= azmoon.TimeExecuteAzmoon,
                Discription= azmoon.Discription,
                EndDate= azmoon.EndDate.ToPersianDate(),
                InputCardTitle= azmoon.InputCardTitle,
                AzPaymentType= azmoon.AzmoonPaymentType,
                Name= azmoon.Name,
                ResultEndDate= azmoon.ResultEndDate,
                SignTextValentear= azmoon.SignTextValentear,
                ResultStartDate= azmoon.ResultStartDate,
                StartDate= azmoon.StartDate.ToPersianDate(),
                Title= azmoon.Title
            };
            return PartialView(MVC.Admin.Azmoons.Views._Edit, am);
        }

        //
        // POST: /Azmoons/Edit/5

        [HttpPost]
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult Edit(AzmoonEditModel azmoon)
        {
            
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;

            KRBUpload KU = new KRBUpload();
            string ms = "";
            bool blnAgahi = false;
            bool blnBanner = false;

            var fileAghiFileData = Request.Files["AghiFileData2"];

            var filePictutreAzmoon = Request.Files["PicturAzmoon2"];

            if (fileAghiFileData != null)
                if (fileAghiFileData.ContentLength > 0)
                    blnAgahi = true;

            if (filePictutreAzmoon != null)
                if (filePictutreAzmoon.ContentLength > 0)
                    blnBanner = true;

            if(blnAgahi)
            {
                AllowUploadStatuse AUS_AghiFileData = KU.AllowUploadSpecialFilesOnly(fileAghiFileData);
                if (AUS_AghiFileData == AllowUploadStatuse.NotDefaineExtension)
                {
                    ms = "فایل انتخابی شما قابل شناسایی نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_AghiFileData == AllowUploadStatuse.HaveNotAllowUpload)
                {
                    ms = "شما اجازه بارگزاری این فایل را ندارید";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_AghiFileData == AllowUploadStatuse.DoNotHaveContentFile)
                {
                    ms = "فایل انتخابی شما قابل خواندن نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_AghiFileData == AllowUploadStatuse.UploadSuccessFully)
                {
                    fileAghiFileData.SaveAs(Server.MapPath("~/App_Data/") + fileAghiFileData.FileName);
                    azmoon.AghiFileData = KU.ConvertToArray(fileAghiFileData, 0, 0).ImageFile;
                    azmoon.AghiFileDataContentType = fileAghiFileData.ContentType;
                    azmoon.AghiFileDataFileName = fileAghiFileData.FileName;
                }
            }

            if (blnBanner)
            {
                AllowUploadStatuse AUS_filePictutreAzmoon = KU.AllowUploadSpecialFilesOnly(filePictutreAzmoon);
                if (AUS_filePictutreAzmoon == AllowUploadStatuse.NotDefaineExtension)
                {
                    ms = "فایل انتخابی شما قابل شناسایی نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.HaveNotAllowUpload)
                {
                    ms = "شما اجازه بارگزاری این فایل را ندارید";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.DoNotHaveContentFile)
                {
                    ms = "فایل انتخابی شما قابل خواندن نیست";
                    this.ErrorMessage(ms);
                    return Content(ms);
                }

                if (AUS_filePictutreAzmoon == AllowUploadStatuse.UploadSuccessFully)
                {


                    azmoon.PicturAzmoon = KU.ConvertToArray(filePictutreAzmoon, 0, 0).ImageFile;
                    azmoon.PicturAzmoonContentType = filePictutreAzmoon.ContentType;
                    azmoon.PicturAzmoonFileName = filePictutreAzmoon.FileName;
                }
            }

            if (ModelState.IsValid)
            {
                _azmoonService.EditAzmoon(azmoon);
                _uow.SaveChanges();
                this.ErrorMessage("آزمون باموفقیت ویرایش شد");
                return RedirectToAction(MVC.Admin.Azmoons.ActionNames.Index, MVC.Admin.Azmoons.Name);
            }
            return View(azmoon);
        }

        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult AzmoonCardSetting(int id)
        {
            var az = context.Azmoons.Find(id);
            AzmoonCardModel ACM = new AzmoonCardModel
            {
                AzmoonId = az.AzmoonId,
                DateExecuteAzmoon = az.DateExecuteAzmoon.ToPersianDate(),
                InputCardTitle = az.InputCardTitle,
                LocationExecuteAzmoon = az.LocationExecuteAzmoon,
                TimeExecuteAzmoon = az.TimeExecuteAzmoon
            };
            return PartialView(MVC.Admin.Azmoons.Views._AzmoonCardSetting, ACM);
        }


        [HttpPost]
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult AzmoonCardSetting(AzmoonCardModel model)
        {
            if(!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات خواسته شده را به طور کامل وارد کنید");
                return RedirectToAction(MVC.Admin.Azmoons.ActionNames.Index, MVC.Admin.Azmoons.Name);
            }

            _azmoonService.SettingCardAzmoon(model);
            _uow.SaveChanges();

            this.SuccessMessage("تنظیمات کارت ورود به جلسه با موفقیت انجام شد");
            return RedirectToAction(MVC.Admin.Azmoons.ActionNames.Index, MVC.Admin.Azmoons.Name);
        }
        
        //
        // GET: /Azmoons/Delete/5
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult Delete(int id)
        {
            Azmoon azmoon = context.Azmoons.Single(x => x.AzmoonId == id);
            return View(azmoon);
        }

        //
        // POST: /Azmoons/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin,moderator")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            Azmoon azmoon = context.Azmoons.Single(x => x.AzmoonId == id);
            context.Azmoons.Remove(azmoon);
            context.SaveChanges();
            return RedirectToAction("Index");
        }



      


      



    }
}
