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
    public partial class ReligionController : Controller
    {
        JazbDbContext db;
        public ReligionController()
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
            return PartialView(MVC.Admin.Religion.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Religion.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, ReligionOrderBy orderBy = ReligionOrderBy.Code,
                                         ReligionSearchBy searchBy = ReligionSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<ReligionViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.Religions.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Religion.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Religion.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(ReligionViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.Religion.Views._Add, model);
            }

            Religion m = new Religion()
            {
                ReligionTitle = model.ReligionTitle,
                ReligionCode = model.ReligionCode
            };
            db.Religions.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("عنوان دین جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Religion.ActionNames.Index, MVC.Admin.Religion.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.Religions.Find(Id);
            ReligionViewModel model = new ReligionViewModel
            {
                ReligionTitle = d.ReligionTitle,
                ReligionCode = d.ReligionCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Religion.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(ReligionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Religion.Views._Edit, model);
            }


            var d = db.Religions.Find(model.Id);

            d.ReligionTitle = model.ReligionTitle;
            d.ReligionCode = model.ReligionCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" عنوان دین با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Religion.ActionNames.Index, MVC.Admin.Religion.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.Religions.Find(id);
            db.Religions.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" عنوان دین با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Religion.ActionNames.Index, MVC.Admin.Religion.Name);
        }




        private IList<ReligionViewModel> GetDataTable(string term, int page, int count,
                                                Order order, ReligionOrderBy orderBy, ReligionSearchBy searchBy)
        {

            IQueryable<Religion> SelectDatas = db.Religions.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ReligionSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.ReligionTitle.Contains(term)).AsQueryable();
                        break;

                    case ReligionSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.ReligionCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case ReligionOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.ReligionTitle).AsQueryable();
                        break;


                    case ReligionOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.ReligionCode).AsQueryable();
                        break;

                }

            }
            else
            {
                switch (orderBy)
                {
                    case ReligionOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.ReligionTitle).AsQueryable();
                        break;


                    case ReligionOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.ReligionCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new ReligionViewModel
              {
                  ReligionTitle = x.ReligionTitle,
                  ReligionCode = x.ReligionCode,
                  Id = x.Id
              }).ToList();
        }

    }
}