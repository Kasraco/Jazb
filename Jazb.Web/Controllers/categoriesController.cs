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

    }
}
