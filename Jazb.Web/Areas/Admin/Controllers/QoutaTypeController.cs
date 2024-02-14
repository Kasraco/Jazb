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
    public partial class QoutaTypeController : Controller
    {
        JazbDbContext db;
        public QoutaTypeController()
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
            return PartialView(MVC.Admin.QoutaType.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.QoutaType.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, QoutaTypeOrderBy orderBy = QoutaTypeOrderBy.Grade,
                                         QoutaTypeSearchBy searchBy = QoutaTypeSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<QoutaTypeViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.QoutaTypes.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.QoutaType.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.QoutaType.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(QoutaTypeViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.QoutaType.Views._Add, model);
            }

            QoutaType m = new QoutaType()
            {
                QoutaTypeTitle = model.QoutaTypeTitle,
                Grade = model.Grade
            };
            db.QoutaTypes.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("نوع سهمیه جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.QoutaType.ActionNames.Index, MVC.Admin.QoutaType.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.QoutaTypes.Find(Id);
            QoutaTypeViewModel model = new QoutaTypeViewModel
            {
                QoutaTypeTitle = d.QoutaTypeTitle,
                Grade = d.Grade,
                Id = d.Id
            };
            return PartialView(MVC.Admin.QoutaType.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(QoutaTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.QoutaType.Views._Edit, model);
            }


            var d = db.QoutaTypes.Find(model.Id);

            d.QoutaTypeTitle = model.QoutaTypeTitle;
            d.Grade = model.Grade;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" نوع سهمیه با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.QoutaType.ActionNames.Index, MVC.Admin.QoutaType.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.QoutaTypes.Find(id);
            db.QoutaTypes.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" نوع سهمیه با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.QoutaType.ActionNames.Index, MVC.Admin.QoutaType.Name);
        }




        private IList<QoutaTypeViewModel> GetDataTable(string term, int page, int count,
                                                Order order, QoutaTypeOrderBy orderBy, QoutaTypeSearchBy searchBy)
        {

            IQueryable<QoutaType> SelectDatas = db.QoutaTypes.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case QoutaTypeSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.QoutaTypeTitle.Contains(term)).AsQueryable();
                        break;

                    case QoutaTypeSearchBy.Grade:
                        SelectDatas = SelectDatas.Where(x => x.Grade.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case QoutaTypeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.QoutaTypeTitle).AsQueryable();
                        break;


                    case QoutaTypeOrderBy.Grade:
                        SelectDatas = SelectDatas.OrderBy(x => x.Grade).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case QoutaTypeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.QoutaTypeTitle).AsQueryable();
                        break;


                    case QoutaTypeOrderBy.Grade:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.Grade).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new QoutaTypeViewModel
              {
                  QoutaTypeTitle = x.QoutaTypeTitle,
                  Grade = x.Grade,
                  Id = x.Id
              }).ToList();
        }
    }
}