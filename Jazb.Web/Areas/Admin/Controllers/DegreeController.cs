using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Jazb.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin,moderator,writer,editor")]
    public partial class DegreeController : Controller
    {
        //
        // GET: /Admin/Degree/
        JazbDbContext db;
        public DegreeController()
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
            return PartialView(MVC.Admin.Degree.Views.Index);
        }

       
        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.Degree.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, DegreeOrderBy orderBy = DegreeOrderBy.Code,
                                         DegreeSearchBy searchBy = DegreeSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<DegreeModel> selectDatas = GetDataTable( term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.degrees.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.Degree.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.Degree.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(DegreeModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if(!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);
           
                return PartialView(MVC.Admin.Degree.Views._Add, model);
            }

            degree m = new degree
            {
                degreeCode = model.degreeCode,
                degreeTitle = model.degreeTitle
            };
            db.degrees.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("مقطع تحصیلی جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.Degree.ActionNames.Index,MVC.Admin.Degree.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.degrees.Find(Id);
            DegreeModel model = new DegreeModel
            {
                degreeCode = d.degreeCode,
                degreeTitle = d.degreeTitle,
                Id = d.Id
            };
            return PartialView(MVC.Admin.Degree.Views._Edit,model);
        }

        [HttpPost]
        public virtual ActionResult Edit(DegreeModel model)
        {
            if(!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.Degree.Views._Edit,model);
            }

         
            var d = db.degrees.Find(model.Id);

            d.degreeCode = model.degreeCode;
            d.degreeTitle = model.degreeTitle;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage("مقطع تحصیلی با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.Degree.ActionNames.Index, MVC.Admin.Degree.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.degrees.Find(id);
            db.degrees.Remove(d);
            db.SaveChanges();
            this.WarningMessage("مقطع تحصیلی با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.Degree.ActionNames.Index, MVC.Admin.Degree.Name);
        }




        private IList<DegreeModel> GetDataTable(string term, int page, int count,
                                                Order order, DegreeOrderBy orderBy, DegreeSearchBy searchBy)
        {

            IQueryable<degree> SelectDatas = db.degrees.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case DegreeSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.degreeTitle.Contains(term)).AsQueryable();
                        break;

                    case DegreeSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.degreeCode.Equals(term)).AsQueryable();
                        break;
                 
                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case DegreeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.degreeTitle).AsQueryable();
                        break;


                    case DegreeOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.degreeCode).AsQueryable();
                        break;
                   


                }

            }
            else
            {
                switch (orderBy)
                {
                    case DegreeOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.degreeTitle).AsQueryable();
                        break;


                    case DegreeOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.degreeCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new DegreeModel
              {
                   degreeCode=x.degreeCode,
                   degreeTitle=x.degreeTitle,
                   Id=x.Id
              }).ToList();
        }
        public int AddDegree(degree model)
        {
            return 0;
        }





    }
}
