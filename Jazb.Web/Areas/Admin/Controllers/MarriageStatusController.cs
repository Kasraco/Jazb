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
    public partial class MarriageStatusController : Controller
    {


        //
        // GET: /Admin/MarriageStatus/
        JazbDbContext db;
        public MarriageStatusController()
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
            return PartialView(MVC.Admin.MarriageStatus.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.MarriageStatus.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, MarriageStatusOrderBy orderBy = MarriageStatusOrderBy.Code,
                                         MarriageStatusSearchBy searchBy = MarriageStatusSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<MarriageStatusViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.marriageStatuses.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.MarriageStatus.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.MarriageStatus.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(MarriageStatusViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.MarriageStatus.Views._Add, model);
            }

            marriageStatus m = new marriageStatus
            {
                marriageStatusCode = model.marriageStatusCode,
                marriageStatusTitle = model.marriageStatusTitle
            };
            db.marriageStatuses.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("مقطع تحصیلی جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.MarriageStatus.ActionNames.Index, MVC.Admin.MarriageStatus.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.marriageStatuses.Find(Id);
            MarriageStatusViewModel model = new MarriageStatusViewModel
            {
                marriageStatusCode = d.marriageStatusCode,
                marriageStatusTitle = d.marriageStatusTitle,
                Id = d.Id
            };
            return PartialView(MVC.Admin.MarriageStatus.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(MarriageStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.MarriageStatus.Views._Edit, model);
            }


            var d = db.marriageStatuses.Find(model.Id);

            d.marriageStatusCode = model.marriageStatusCode;
            d.marriageStatusTitle = model.marriageStatusTitle;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage("مقطع تحصیلی با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.MarriageStatus.ActionNames.Index, MVC.Admin.MarriageStatus.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.marriageStatuses.Find(id);
            db.marriageStatuses.Remove(d);
            db.SaveChanges();
            this.WarningMessage("مقطع تحصیلی با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.MarriageStatus.ActionNames.Index, MVC.Admin.MarriageStatus.Name);
        }




        private IList<MarriageStatusViewModel> GetDataTable(string term, int page, int count,
                                                Order order, MarriageStatusOrderBy orderBy, MarriageStatusSearchBy searchBy)
        {

            IQueryable<marriageStatus> SelectDatas = db.marriageStatuses.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case MarriageStatusSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.marriageStatusTitle.Contains(term)).AsQueryable();
                        break;

                    case MarriageStatusSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.marriageStatusCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case MarriageStatusOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.marriageStatusTitle).AsQueryable();
                        break;

                    case MarriageStatusOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.marriageStatusCode).AsQueryable();
                        break;

                }

            }
            else
            {
                switch (orderBy)
                {
                    case MarriageStatusOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.marriageStatusTitle).AsQueryable();
                        break;


                    case MarriageStatusOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.marriageStatusCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new MarriageStatusViewModel
              {
                  marriageStatusCode = x.marriageStatusCode,
                  marriageStatusTitle = x.marriageStatusTitle,
                  Id = x.Id
              }).ToList();
        }
        public int AddMarriageStatus(marriageStatus model)
        {
            return 0;
        }




    }
}