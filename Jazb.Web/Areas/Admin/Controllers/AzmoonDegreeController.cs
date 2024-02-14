using Jazb.Datalayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Jazb.DomainClasses.Entities;

namespace Jazb.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin,moderator")]
    public partial class AzmoonDegreeController : Controller
    {
        //
        // GET: /Admin/AzmoonDegree/


        private JazbDbContext db = new JazbDbContext();


        public virtual ActionResult Index(int AzmoonId)
        {
            return PartialView();
        }
        public virtual ActionResult ListData(int AzmoonId)
        {
            var lstAD = db.AzmoonDegrees.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId);
            return PartialView(lstAD);
        }

        public virtual ActionResult AddDegree(int AzmoonId)
        {
            var az = db.Azmoons.Find(AzmoonId);

            AzmoonDegree model = new AzmoonDegree
            {
                Azmoon = az
            };

            return PartialView(model);
        }

        [HttpPost]
        public virtual ActionResult AddDegree(AzmoonDegree model)
        {
            if (ModelState.IsValid)
            {
                db.AzmoonDegrees.Add(model);
                db.SaveChanges();
                return PartialView();
            }

            return PartialView();

        }


        public virtual ActionResult EditDegree(int AZId)
        {
            var model = db.AzmoonDegrees.Find(AZId);


            return PartialView(model);
        }

        [HttpPost]
        public virtual ActionResult EditDegree(AzmoonDegree model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView();
            }

            return PartialView();

        }

        [HttpDelete]
        public virtual ActionResult DeleteADegree(int ADId)
        {
            var AZD = db.AzmoonDegrees.Find(ADId);
            db.AzmoonDegrees.Remove(AZD);
            db.SaveChanges();
            return PartialView();
        }
    }
}
