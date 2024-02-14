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
    public partial class GenderController : Controller
    {
        JazbDbContext db;
        public GenderController()
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
            return PartialView(MVC.Admin.Gender.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Gender.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, GenderOrderBy orderBy = GenderOrderBy.Code,
                                         GenderSearchBy searchBy = GenderSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<GenderViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.Genders.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Gender.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Gender.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(GenderViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.Gender.Views._Add, model);
            }

            Gender m = new Gender
            {
                GenderTitle = model.GenderTitle,
                GenderCode = model.GenderCode
            };
            db.Genders.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("نوع جنسیت جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Gender.ActionNames.Index, MVC.Admin.Gender.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.Genders.Find(Id);
            GenderViewModel model = new GenderViewModel
            {
                GenderTitle = d.GenderTitle,
                GenderCode = d.GenderCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Gender.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(GenderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Gender.Views._Edit, model);
            }


            var d = db.Genders.Find(model.Id);

            d.GenderTitle = model.GenderTitle;
            d.GenderCode = model.GenderCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" نوع جنسیت با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Gender.ActionNames.Index, MVC.Admin.Gender.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.Genders.Find(id);
            db.Genders.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" نوع جنسیت با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Gender.ActionNames.Index, MVC.Admin.Gender.Name);
        }




        private IList<GenderViewModel> GetDataTable(string term, int page, int count,
                                                Order order, GenderOrderBy orderBy, GenderSearchBy searchBy)
        {

            IQueryable<Gender> SelectDatas = db.Genders.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case GenderSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.GenderTitle.Contains(term)).AsQueryable();
                        break;

                    case GenderSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.GenderCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case GenderOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.GenderTitle).AsQueryable();
                        break;


                    case GenderOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.GenderCode).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case GenderOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.GenderTitle).AsQueryable();
                        break;


                    case GenderOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.GenderCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new GenderViewModel
              {
                  GenderTitle = x.GenderTitle,
                  GenderCode = x.GenderCode,
                  Id = x.Id
              }).ToList();
        }
    }
}