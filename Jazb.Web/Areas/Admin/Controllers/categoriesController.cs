using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Jazb.Datalayer.Context;
using System.Data.Entity;
using PagedList;
using Jazb.DomainClasses.Entities;

namespace Jazb.Web.Areas.Admin.Controllers
{
        [Authorize(Roles = "admin,moderator,writer,editor")]
    public partial class categoriesController : Controller
    {
        private JazbDbContext context = new JazbDbContext();

        //
        // GET: /categories/

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

            var lst = context.categories.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                lst = lst.Where(x => x.CategoryId.ToString().Contains(searchString)).ToList();
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
                    lst = lst.OrderBy(x => x.CategoryId).ToList();
                    break;
            }

            int pageNumber = (page ?? 1);
            int pageSize2 = (pageSize ?? 5);
            return View(lst.ToPagedList(pageNumber, pageSize2));
        }

        //
        // GET: /categories/Details/5

        public virtual ViewResult Details(int id)
        {
            category category = context.categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // GET: /categories/Create

        public virtual ActionResult Create()
        {
            return View();
        }

        //
        // POST: /categories/Create

        [HttpPost]
        public virtual ActionResult Create(category category)
        {
            if (ModelState.IsValid)
            {
                context.categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        //
        // GET: /categories/Edit/5

        public virtual ActionResult Edit(int id)
        {
            category category = context.categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // POST: /categories/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(category category)
        {
            if (ModelState.IsValid)
            {
                context.Entry(category).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //
        // GET: /categories/Delete/5

        public virtual ActionResult Delete(int id)
        {
            category category = context.categories.Single(x => x.CategoryId == id);
            return View(category);
        }

        //
        // POST: /categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            category category = context.categories.Single(x => x.CategoryId == id);
            context.categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
