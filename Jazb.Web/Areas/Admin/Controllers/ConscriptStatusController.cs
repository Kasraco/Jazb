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
    public partial class ConscriptStatusController : Controller
    {
        // GET: Admin/ConscriptStatus
        JazbDbContext db;
        public ConscriptStatusController()
        {
            db = new JazbDbContext();
        }
        public virtual ActionResult Index()
        {

            return PartialView(MVC.Admin.ConscriptStatus.Views._Index);
        }

        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.ConscriptStatus.Views._Nav);
        }


        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                        Order order = Order.Asscending, ConscriptStatusOrderBy orderBy = ConscriptStatusOrderBy.Code,
                                       ConscriptStatusSearchBy searchBy = ConscriptStatusSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<ConscriptStatusViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.ConscriptStatuses.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.ConscriptStatus.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            return PartialView(MVC.Admin.ConscriptStatus.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(ConscriptStatusViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را صحیح وارد کنید", "خطا");

                return PartialView(MVC.Admin.ConscriptStatus.Views._Add, model);
            }

            ConscriptStatus m = new ConscriptStatus
            {
                ConscriptStatusCode = model.ConscriptStatusCode,
                ConscriptStatusTitle = model.ConscriptStatusTitle
            };
            db.ConscriptStatuses.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("وضعیت نظام وظیفه جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.ConscriptStatus.ActionNames.Index, MVC.Admin.ConscriptStatus.Name);
        }


        public virtual ActionResult Edit(int Id)
        {
            var d = db.ConscriptStatuses.Find(Id);
            ConscriptStatusViewModel model = new ConscriptStatusViewModel
            {
                ConscriptStatusCode = d.ConscriptStatusCode,
                ConscriptStatusTitle = d.ConscriptStatusTitle,
                Id = d.Id
            };
            return PartialView(MVC.Admin.ConscriptStatus.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(ConscriptStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.ConscriptStatus.Views._Edit, model);
            }


            var d = db.ConscriptStatuses.Find(model.Id);

            d.ConscriptStatusCode = model.ConscriptStatusCode;
            d.ConscriptStatusTitle = model.ConscriptStatusTitle;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage("وضعیت نظام وظیفه با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.ConscriptStatus.ActionNames.Index, MVC.Admin.ConscriptStatus.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.ConscriptStatuses.Find(id);
            db.ConscriptStatuses.Remove(d);
            db.SaveChanges();
            this.WarningMessage("وضعیت نظام وظیفه با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.ConscriptStatus.ActionNames.Index, MVC.Admin.ConscriptStatus.Name);
        }


        private IList<ConscriptStatusViewModel> GetDataTable(string term, int page, int count,
                                          Order order, ConscriptStatusOrderBy orderBy, ConscriptStatusSearchBy searchBy)
        {

            IQueryable<ConscriptStatus> SelectDatas = db.ConscriptStatuses.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ConscriptStatusSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.ConscriptStatusTitle.Contains(term)).AsQueryable();
                        break;

                    case ConscriptStatusSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.ConscriptStatusCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case ConscriptStatusOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.ConscriptStatusTitle).AsQueryable();
                        break;


                    case ConscriptStatusOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.ConscriptStatusCode).AsQueryable();
                        break;

                }

            }
            else
            {
                switch (orderBy)
                {
                    case ConscriptStatusOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.ConscriptStatusTitle).AsQueryable();
                        break;


                    case ConscriptStatusOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.ConscriptStatusCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new ConscriptStatusViewModel
              {
                  ConscriptStatusCode = x.ConscriptStatusCode,
                  ConscriptStatusTitle = x.ConscriptStatusTitle,
                  Id = x.Id
              }).ToList();
        }

    }
}