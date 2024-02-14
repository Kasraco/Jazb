using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using Jazb.Datalayer.Context;
using Jazb.Model;
using Jazb.DomainClasses.Entities;
using PagedList;
using Jazb.Web.Caching;
using Jazb.Servicelayer.Interfaces;
using System.IO;

namespace Jazb.Web.Controllers
{
  
    public partial class HomeController : Controller
    {
        private readonly IOptionService _optionService;
        private JazbDbContext context = new JazbDbContext();
        public HomeController(IOptionService optionService)
        {
            _optionService = optionService;
            
           

        }


       

        public virtual ActionResult Eslahiyeh()
        {
           
            return View();
        }

        //public virtual ActionResult SendMassages()
        //{
        //    var MList = context.MassageLists.ToList();
        //    foreach(var m in MList)
        //    {
        //        var valens = context.Valentears.Where(x => x.NationalID == m.NationalID && x.Azmoon.AzmoonId == m.AzmoonId).ToList();
        //        foreach(var v in valens)
        //        {
        //            v.Description = m.MessageBody;
        //            context.Valentears.Attach(v);
        //            context.Entry(v).Property(x => x.Description).IsModified = true;

        //            context.SaveChanges();
        //        }

        //    }
        //    return View();
        //}

        public virtual ActionResult Index(int? page, int? pageSize, int? page1)
        {
        

            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

                ViewBag.Keywords = siteConfig.BlogKeywords;
            ViewBag.Description = siteConfig.BlogDescription;
            ViewBag.CompanyName = siteConfig.BlogName;
            ViewBag.CompanyLogo = siteConfig.CompanyLogo;

            Jazb.DomainClasses.Entities.User Userkrb=new User();
            ViewBag.pageSize = pageSize;
            if (Request.HttpMethod != "GET")
            {
                page = 1;
                page1 = 1;
            }



            HomeModel HM = new HomeModel();
            //  HM.ArticlesList = new PagedList<article>(null, 0, 0);
            IList<article> la;
           // ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            if (User.Identity.IsAuthenticated)
            {

                var quser = context.Users.Where(x => x.UserName.Equals(User.Identity.Name));
                if(quser.Count()>0)
                    Userkrb=quser.First();
                int userID = Userkrb.Id;
            }

            la = context.articles.ToList();
            var ListAzmoons = context.Azmoons.Where(x => x.Active.Equals(true)).ToList();
            var ListAzmoonsDropdown = context.Azmoons.Where(x => x.Active.Equals(true)).ToList();


            IEnumerable<SelectListItem> lst = ListAzmoonsDropdown.Select(x =>
               new SelectListItem { Text = x.Name, Value = x.AzmoonId.ToString(CultureInfo.InvariantCulture) });
            ViewBag.AzmoonList = lst;

            int pageNumber = (page ?? 1);
            int pageNumber2 = (page1 ?? 1);
            int pageSize2 = (pageSize ?? 5);

           // ViewBag.countProfile = context.profiles.Count() == 0 ? false : true;
            HM.Azmoons = ListAzmoons.OrderByDescending(x => x.AzmoonId).ToList();
            //HM.ArticlesList = la.Where(x => x.CategoryId.Equals(1)).ToList().ToPagedList(pageNumber, pageSize2);
            //HM.AnnouncementList = la.Where(x => x.CategoryId.Equals(2)).ToList().ToPagedList(pageNumber2, pageSize2);
            return View(HM);


        }

        [Authorize]
        public virtual ActionResult FirstPage()
        {
            return View();
        }


        public virtual ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {

            var ListAzmoons = context.Azmoons.Where(x => x.Active.Equals(true)).ToList();

            IEnumerable<SelectListItem> lst = ListAzmoons.Select(x =>
               new SelectListItem { Text = x.Name, Value = x.AzmoonId.ToString(CultureInfo.InvariantCulture) });
            ViewBag.AzmoonList = lst;

            ViewBag.Message = "Your contact page.";

            return View();
        }

     


            public virtual ActionResult DownloadAgahi(int AID)
            {
                var q = context.Azmoons.Find(AID);
                byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath("~//App_Data//") + q.AghiFileDataFileName);
                return File(filebyte, q.AghiFileDataContentType, q.AghiFileDataFileName);


            }

            public virtual ActionResult Barrasimadarek_sanjesh_99()
            {
                byte[] filebyte = System.IO.File.ReadAllBytes(Server.MapPath("~//App_Data//") + "Barrasimadarek_sanjesh_99.pdf");
                return File(filebyte, "application/pdf", "Barrasimadarek_sanjesh_99.pdf");


            }

   


    }
}
