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
    public partial class EducationSkillController : Controller
    {
        JazbDbContext db;
        public EducationSkillController()
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
            return PartialView(MVC.Admin.EducationSkill.Views._Index);
        }


        public virtual ActionResult Nav()
        {

            return PartialView(MVC.Admin.EducationSkill.Views._Nav);
        }

        public virtual ActionResult List(string term = "", int page = 0, int count = 10,
                                         Order order = Order.Asscending, EducationSkillOrderBy orderBy = EducationSkillOrderBy.Code,
                                         EducationSkillSearchBy searchBy = EducationSkillSearchBy.Title)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.ORDER = order;
            ViewBag.ORDERBY = orderBy;
            ViewBag.SEARCHBY = searchBy;


            IList<EducationdSkillViewModel> selectDatas = GetDataTable(term, page, count, order, orderBy, searchBy);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? db.educatedSkills.Count() : selectDatas.Count;


            return PartialView(MVC.Admin.EducationSkill.Views._List, selectDatas);
        }

        public virtual ActionResult Add()
        {
            //return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.success, Position.topLeft);
            return PartialView(MVC.Admin.EducationSkill.Views._Add);
        }


        [HttpPost]
        public virtual ActionResult Add(EducationdSkillViewModel model)
        {
            // return MessageBox.Show("شما تیک چک باکس را نزده اید", AlertType.warning, Position.topLeft);
            if (!ModelState.IsValid)
            {
                //TempData["Message"] = Notification.Show("لطفا اطلاعات را صحیح وارد کنید", "خطا", position: Position.BottomRight, type: ToastType.Error);

                return PartialView(MVC.Admin.EducationSkill.Views._Add, model);
            }

            educatedSkill m = new educatedSkill()
            {
                educatedSkillTitle = model.educatedSkillTitle,
                educatedSkillCode = model.educatedSkillCode
            };
            db.educatedSkills.Add(m);
            db.SaveChanges();
            //TempData["Message"] = Notification.Show("مقطع تحصیلی جدید ثبت شد", position: Position.BottomRight, type: ToastType.Success);
            this.SuccessMessage("نوع رشته تحصیلی جدید ثبت شد");
            //TempData["Message"] = ViewBag.Message;
            return RedirectToAction(MVC.Admin.EducationSkill.ActionNames.Index, MVC.Admin.EducationSkill.Name);
        }

        public virtual ActionResult Edit(int Id)
        {
            var d = db.educatedSkills.Find(Id);
            EducationdSkillViewModel model = new EducationdSkillViewModel
            {
                educatedSkillTitle = d.educatedSkillTitle,
                educatedSkillCode = d.educatedSkillCode,
                Id = d.Id
            };
            return PartialView(MVC.Admin.EducationSkill.Views._Edit, model);
        }

        [HttpPost]
        public virtual ActionResult Edit(EducationdSkillViewModel model)
        {
            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا اطلاعات را به صورت صحیح وارد کنید");
                return PartialView(MVC.Admin.EducationSkill.Views._Edit, model);
            }


            var d = db.educatedSkills.Find(model.Id);

            d.educatedSkillTitle = model.educatedSkillTitle;
            d.educatedSkillCode = model.educatedSkillCode;
            d.Id = model.Id;
            db.SaveChanges();
            this.InfoMessage(" رشته تحصیلی با موفقیت ویرایش شد");
            return RedirectToAction(MVC.Admin.EducationSkill.ActionNames.Index, MVC.Admin.EducationSkill.Name);
        }


        [HttpDelete]
        public virtual ActionResult Delete(int id)
        {
            var d = db.educatedSkills.Find(id);
            db.educatedSkills.Remove(d);
            db.SaveChanges();
            this.WarningMessage(" رشته تحصیلی با موفقیت حذف شد");
            return RedirectToAction(MVC.Admin.EducationSkill.ActionNames.Index, MVC.Admin.EducationSkill.Name);
        }




        private IList<EducationdSkillViewModel> GetDataTable(string term, int page, int count,
                                                Order order, EducationSkillOrderBy orderBy, EducationSkillSearchBy searchBy)
        {

            IQueryable<educatedSkill> SelectDatas = db.educatedSkills.AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case EducationSkillSearchBy.Title:
                        SelectDatas = SelectDatas.Where(x => x.educatedSkillTitle.Contains(term)).AsQueryable();
                        break;

                    case EducationSkillSearchBy.Code:
                        SelectDatas = SelectDatas.Where(x => x.educatedSkillCode.Equals(term)).AsQueryable();
                        break;

                }
            }

            if (order == Order.Asscending)
            {
                switch (orderBy)
                {

                    case EducationSkillOrderBy.Title:
                        SelectDatas = SelectDatas.OrderBy(x => x.educatedSkillTitle).AsQueryable();
                        break;


                    case EducationSkillOrderBy.Code:
                        SelectDatas = SelectDatas.OrderBy(x => x.educatedSkillCode).AsQueryable();
                        break;



                }

            }
            else
            {
                switch (orderBy)
                {
                    case EducationSkillOrderBy.Title:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.educatedSkillTitle).AsQueryable();
                        break;


                    case EducationSkillOrderBy.Code:
                        SelectDatas = SelectDatas.OrderByDescending(x => x.educatedSkillCode).AsQueryable();
                        break;
                }
            }


            return SelectDatas.Skip(page * count).Take(count).Select(x =>
              new EducationdSkillViewModel
              {
                  educatedSkillTitle = x.educatedSkillTitle,
                  educatedSkillCode = x.educatedSkillCode,
                  Id = x.Id
              }).ToList();
        }
    }
}