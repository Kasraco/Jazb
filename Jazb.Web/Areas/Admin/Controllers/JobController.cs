using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin,moderator,writer,editor")]
    public partial class JobController : Controller
    {
        JazbDbContext db;
        public JobController()
        {
            db = new JazbDbContext();
        }

        public virtual ActionResult Index()
        {

            //if(TempData["Message"] !=null)
            //{
            //    var temp = TempData["Message"] as string;
            //    ViewBag.Message = temp;
            //}
            return PartialView(MVC.Admin.Job.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Job.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, JobOrderBy orderBy = JobOrderBy.Code,
                                         JobSearchBy searchBy = JobSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<JobViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.Jobs.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Job.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Job.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(JobViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.Job.Views._Add, model);
            }

            Job m = new Job()
            {
                JobTitle = model.JobTitle,
                JobCode = model.JobCode
            };
            db.Jobs.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("رشته شغلی جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Job.ActionNames.Index, MVC.Admin.Job.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.Jobs.Find(Id);
            JobViewModel model = new JobViewModel
            {
                JobTitle = d.JobTitle,
                JobCode = d.JobCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Job.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(JobViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Job.Views._Edit, model);
            }


            var d = db.Jobs.Find(model.Id);

            d.JobTitle = model.JobTitle;
            d.JobCode = model.JobCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" رشته شغلی با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Job.ActionNames.Index, MVC.Admin.Job.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.Jobs.Find(id);
            db.Jobs.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" رشته شغلی با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Job.ActionNames.Index, MVC.Admin.Job.Name);
        }




        private IList<JobViewModel> GetDataTable(string term, int page, int count,
                                                Order order, JobOrderBy orderBy, JobSearchBy searchBy)
        {

            IQueryable<Job> SelectDatas = db.Jobs.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case JobSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.JobTitle.Contains(term)).AsQueryable();
                        break;

                    case JobSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.JobCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case JobOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.JobTitle).AsQueryable();
                        break;


                    case JobOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.JobCode).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case JobOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.JobTitle).AsQueryable();
                        break;


                    case JobOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.JobCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new JobViewModel
              {
                  JobTitle = x.JobTitle,
                  JobCode = x.JobCode,
                  Id = x.Id
              }).ToList();
        }
    }
}