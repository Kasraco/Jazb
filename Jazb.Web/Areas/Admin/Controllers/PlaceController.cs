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
    public partial class PlaceController : Controller
    {
        JazbDbContext db;
        public PlaceController()
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
            return PartialView(MVC.Admin.Place.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Place.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, PlaceOrderBy orderBy = PlaceOrderBy.Code,
                                         PlaceSearchBy searchBy = PlaceSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<PlaceViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.Places.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Place.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Place.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(PlaceViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.Place.Views._Add, model);
            }

            Place m = new Place()
            {
                PlaceTitle = model.PlaceTitle,
                PlaceCode = model.PlaceCode
            };
            db.Places.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("محل مورد تقاضا جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Place.ActionNames.Index, MVC.Admin.Place.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.Places.Find(Id);
            PlaceViewModel model = new PlaceViewModel
            {
                PlaceTitle = d.PlaceTitle,
                PlaceCode = d.PlaceCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Place.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(PlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Place.Views._Edit, model);
            }


            var d = db.Places.Find(model.Id);

            d.PlaceTitle = model.PlaceTitle;
            d.PlaceCode = model.PlaceCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" محل مورد تقاضا با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Place.ActionNames.Index, MVC.Admin.Place.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.Places.Find(id);
            db.Places.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" محل مورد تقاضا با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Place.ActionNames.Index, MVC.Admin.Place.Name);
        }




        private IList<PlaceViewModel> GetDataTable(string term, int page, int count,
                                                Order order, PlaceOrderBy orderBy, PlaceSearchBy searchBy)
        {

            IQueryable<Place> SelectDatas = db.Places.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case PlaceSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.PlaceTitle.Contains(term)).AsQueryable();
                        break;

                    case PlaceSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.PlaceCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case PlaceOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.PlaceTitle).AsQueryable();
                        break;


                    case PlaceOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.PlaceCode).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case PlaceOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.PlaceTitle).AsQueryable();
                        break;


                    case PlaceOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.PlaceCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new PlaceViewModel
              {
                  PlaceTitle = x.PlaceTitle,
                  PlaceCode = x.PlaceCode,
                  Id = x.Id
              }).ToList();
        }
    }
}