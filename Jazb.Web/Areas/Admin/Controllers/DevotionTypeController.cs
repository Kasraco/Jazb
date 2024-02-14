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
    public partial class DevotionTypeController : Controller
    {
        JazbDbContext db;
        public DevotionTypeController()
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
            return PartialView(MVC.Admin.DevotionType.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.DevotionType.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, DevotionTypeOrderBy orderBy = DevotionTypeOrderBy.Grade,
                                         DevotionTypeSearchBy searchBy = DevotionTypeSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<DevotionTypeViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.DevotionTypes.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.DevotionType.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.DevotionType.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(DevotionTypeViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.DevotionType.Views._Add, model);
            }

            DevotionType m = new DevotionType()
            {
                DevotionTypeTitle = model.DevotionTypeTitle,
                Grade = model.Grade
            };
            db.DevotionTypes.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("نوع سهمیه ایثارگری جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.DevotionType.ActionNames.Index, MVC.Admin.DevotionType.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.DevotionTypes.Find(Id);
            DevotionTypeViewModel model = new DevotionTypeViewModel
            {
                DevotionTypeTitle = d.DevotionTypeTitle,
                Grade = d.Grade,
                Id = d.Id
            };
            return PartialView(MVC.Admin.DevotionType.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(DevotionTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.DevotionType.Views._Edit, model);
            }


            var d = db.DevotionTypes.Find(model.Id);

            d.DevotionTypeTitle = model.DevotionTypeTitle;
            d.Grade = model.Grade;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" سهمیه ایثارگری با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.DevotionType.ActionNames.Index, MVC.Admin.DevotionType.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.DevotionTypes.Find(id);
            db.DevotionTypes.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" سهمیه ایثارگری با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.DevotionType.ActionNames.Index, MVC.Admin.DevotionType.Name);
        }




        private IList<DevotionTypeViewModel> GetDataTable(string term, int page, int count,
                                                Order order, DevotionTypeOrderBy orderBy, DevotionTypeSearchBy searchBy)
        {

            IQueryable<DevotionType> SelectDatas = db.DevotionTypes.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case DevotionTypeSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.DevotionTypeTitle.Contains(term)).AsQueryable();
                        break;

                    case DevotionTypeSearchBy.Grade:
                        SelectDatas = SelectDatas.Where(x => x.Grade.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case DevotionTypeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.DevotionTypeTitle).AsQueryable();
                        break;


                    case DevotionTypeOrderBy.Grade:
                        SelectDatas = SelectDatas.OrderBy(x => x.Grade).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case DevotionTypeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.DevotionTypeTitle).AsQueryable();
                        break;


                    case DevotionTypeOrderBy.Grade:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.Grade).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new DevotionTypeViewModel
              {
                  DevotionTypeTitle = x.DevotionTypeTitle,
                  Grade = x.Grade,
                  Id = x.Id
              }).ToList();
        }
    }
}