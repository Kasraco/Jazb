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
    public partial class FaithController : Controller
    {

        JazbDbContext db;
        public FaithController()
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
            return PartialView(MVC.Admin.Faith.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Faith.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, FaithOrderBy orderBy = FaithOrderBy.Code,
                                         FaithSearchBy searchBy = FaithSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<FaithViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.Faiths.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Faith.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Faith.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(FaithViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.Faith.Views._Add, model);
            }

            Faith m = new Faith()
            {
                FaithTitle = model.FaithTitle,
                FaithCode = model.FaithCode
            };
            db.Faiths.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("نوع مذهب جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Faith.ActionNames.Index, MVC.Admin.Faith.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.Faiths.Find(Id);
            FaithViewModel model = new FaithViewModel
            {
                FaithTitle = d.FaithTitle,
                FaithCode = d.FaithCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Faith.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(FaithViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Faith.Views._Edit, model);
            }


            var d = db.Faiths.Find(model.Id);

            d.FaithTitle = model.FaithTitle;
            d.FaithCode = model.FaithCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" نوع مذهب با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Faith.ActionNames.Index, MVC.Admin.Faith.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.Faiths.Find(id);
            db.Faiths.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" نوع مذهب با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Faith.ActionNames.Index, MVC.Admin.Faith.Name);
        }




        private IList<FaithViewModel> GetDataTable(string term, int page, int count,
                                                Order order, FaithOrderBy orderBy, FaithSearchBy searchBy)
        {

            IQueryable<Faith> SelectDatas = db.Faiths.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case FaithSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.FaithTitle.Contains(term)).AsQueryable();
                        break;

                    case FaithSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.FaithCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case FaithOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.FaithTitle).AsQueryable();
                        break;


                    case FaithOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.FaithCode).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case FaithOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.FaithTitle).AsQueryable();
                        break;


                    case FaithOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.FaithCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new FaithViewModel
              {
                  FaithTitle = x.FaithTitle,
                  FaithCode = x.FaithCode,
                  Id = x.Id
              }).ToList();
        }
    }
}