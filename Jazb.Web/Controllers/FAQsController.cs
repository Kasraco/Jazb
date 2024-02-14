using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;

namespace Jazb.Web.Controllers
{
    public partial class FAQsController : Controller
    {
        private JazbDbContext context = new JazbDbContext();

        //
        // GET: /FAQs/
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

            var lst = context.FAQs.ToList();
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


        public virtual ViewResult AnswerIndex(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
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

            var lst = new List<FAQ>();
            if (!String.IsNullOrEmpty(searchString))
            {
                lst = context.FAQs.ToList();
                lst = lst.Where(x => x.MeliCode.ToString().Contains(searchString)).ToList();
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

        //public ActionResult CreateQuery()
        //{
        //    var q = (from p in context.FAQs.Where(x => x.Body != null).ToList()
        //             where p.Body.Contains("شهر") ||
        //                   p.Body.Contains("شهرستان") ||
        //                   p.Body.Contains("سکونت") ||
        //                   p.Body.Contains("تولد") ||
        //                   p.Body.Contains("بومی") ||
        //                   p.Body.Contains("سهمیه") ||
        //                   p.Body.Contains("ایثارگری") ||
        //                   p.Body.Contains("جانباز") ||
        //                   p.Body.Contains("رزمنده") ||
        //                   p.Body.Contains("آزاده") ||
        //                   p.Body.Contains("مورد تقاضا") ||
        //                   p.Body.Contains("جغرافیا")
        //             group p by new { p.MeliCode } into g
        //             select g.Key.MeliCode);

        //    var qFAQ = (from p in context.FAQs.ToList()

        //                where q.Contains(p.MeliCode)
        //                group p by new { p.FirstName, p.LastName, p.MeliCode, p.Body } into g
        //                select new Jazb.Model.Report.FAQModel
        //                {
        //                    FirstName = g.Key.FirstName,
        //                    LastName = g.Key.LastName,
        //                    MeliCode = g.Key.MeliCode,
        //                    Mobile = "",
        //                    RequestType = g.Key.MeliCode
        //                }).Distinct().ToList();

        //    Reports.FAQPdfReport A = new Reports.FAQPdfReport();
        //    A.CreatePdfReport(qFAQ);
        //    return View(qFAQ.Distinct());
        //}
        //
        // GET: /FAQs/Details/5

        public virtual ViewResult Details(int id)
        {
            FAQ faq = context.FAQs.Single(x => x.Id == id);
            return View(faq);
        }

        //
        // GET: /FAQs/Create
        public virtual ActionResult Create()
        {
            var ListAzmoons = context.Azmoons.Where(x => x.Active.Equals(true)).ToList();



            IEnumerable<SelectListItem> lst = ListAzmoons.Select(x =>
               new SelectListItem { Text = x.Name, Value = x.AzmoonId.ToString(CultureInfo.InvariantCulture) });
            ViewBag.AzmoonList = lst;

            return PartialView(MVC.FAQs.Views._frmSendQuestion);
        }

        //
        // POST: /FAQs/Create

        [HttpPost]
        public virtual ActionResult Create(FAQ faq)
        {


            var ListAzmoons = context.Azmoons.Where(x => x.Active.Equals(true)).ToList();

            IEnumerable<SelectListItem> lst = ListAzmoons.Select(x =>
               new SelectListItem { Text = x.Name, Value = x.AzmoonId.ToString(CultureInfo.InvariantCulture) });
            ViewBag.AzmoonList = lst;

            if (ModelState.IsValid)
            {
                faq.QuestionDate = DateTime.Now;
                context.FAQs.Add(faq);
                context.SaveChanges();

                ModelState.AddModelError("", "سئوال شما با موقیت ثبت شد");
                return View(MVC.Home.Views.Contact, faq);

            }
            else
                ModelState.AddModelError("", "خطا رخ داده");

            return View(MVC.Home.Views.Contact, faq);
        }

        //
        // GET: /FAQs/Edit/5
        [Authorize]
        public virtual ActionResult Edit(int id)
        {
            FAQ faq = context.FAQs.Single(x => x.Id == id);
            return View(faq);
        }

        //
        // POST: /FAQs/Edit/5
        [Authorize]
        [HttpPost]
        public virtual ActionResult Edit(FAQ faq)
        {
            if (ModelState.IsValid)
            {
                context.Entry(faq).State = EntityState.Modified;
                context.Entry(faq).Property("Body").IsModified = false;
                context.Entry(faq).Property("FirstName").IsModified = false;
                context.Entry(faq).Property("LastName").IsModified = false;
                context.Entry(faq).Property("MeliCode").IsModified = false;
                context.Entry(faq).Property("QuestionDate").IsModified = false;
                context.Entry(faq).Property("Subject").IsModified = false;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }

        //
        // GET: /FAQs/Delete/5
        [Authorize]
        public virtual ActionResult Delete(int id)
        {
            FAQ faq = context.FAQs.Single(x => x.Id == id);
            return View(faq);
        }

        //
        // POST: /FAQs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            FAQ faq = context.FAQs.Single(x => x.Id == id);
            context.FAQs.Remove(faq);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
