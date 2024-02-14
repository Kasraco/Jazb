using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using System.Data.Entity;
using PagedList;
namespace Jazb.Web.Controllers
{
    public partial class articlesController : Controller
    {
        private JazbDbContext context;

        public articlesController()
        {
            context = new JazbDbContext();
        }
        //
        // GET: /articles/
        [Authorize]
        public virtual ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            //ViewBag.UniversityTitleSortParm = sortOrder == "UniversityTitle" ? "UniversityTitle_desc" : "UniversityTitle";
            ViewBag.pageSize = pageSize;

            if (Request.HttpMethod == "GET")
            {
                if (!string.IsNullOrEmpty(currentFilter))
                { searchString = currentFilter; }
            }
            else { page = 1; }
            ViewBag.CurrentFilter = searchString;

            var lst = context.articles.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                lst = lst.Where(x => x.Id.ToString().Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                //case "UniversityTitle":
                //lst = lst.OrderBy(x => x.UniversityTitle).ToList();
                //break;
                //case "UniversityTitle_desc":
                //lst = lst.OrderByDescending(x => x.UniversityTitle).ToList();
                //break;
                default:
                    lst = lst.OrderBy(x => x.Id).ToList();
                    break;
            }

            int pageNumber = (page ?? 1);
            int pageSize2 = (pageSize ?? 5);
            return View(lst.ToPagedList(pageNumber, pageSize2));
        }

        //
        // GET: /articles/Details/5



        public virtual ViewResult Details(int id)
        {
            article article = context.articles.Single(x => x.Id == id);
            return View(article);
        }
        
        public virtual ActionResult AzmoonlistArticle(int Aid)
        {
            List<article> articles = context.articles.Where(x => x.AzmoonId == Aid && 
                                                                 x.StartDate<=DateTime.UtcNow || 
                                                                 x.ExpireDate>=DateTime.UtcNow)
                                                      .OrderByDescending(x=>x.StartDate)
                                                      .ToList();

            return PartialView(MVC.articles.Views._azmoonlistArticle, articles);
        }


        public virtual ActionResult DownloadAgahi(int AID)
        {
            var q = context.articles.Find(AID);
            byte[] filebyte = q.AttachmentFile;
             return File(filebyte, q.AttachmentFiletype, q.AttachmentFileName);
            

        }


    }
}
