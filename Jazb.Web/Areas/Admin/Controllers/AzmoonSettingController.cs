using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.AdminModel.ManageAzmoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Areas.Admin.Controllers
{

    [Authorize(Roles = "admin,moderator")]
    public partial class AzmoonSettingController : Controller
    {

        //
        // GET: /Admin/Degree/
        JazbDbContext db;
        public AzmoonSettingController()
        {
            db = new JazbDbContext();
        }

        

        public virtual ActionResult Index(int AzmoonId)
        {
            //ViewBag.AzmoonId = AzmoonId;
            //ViewBag.PossibleGenders = GetGender();
            //ViewBag.PossibleMarriageStatus = GetMarriageStatus();
            //ViewBag.PossibleConscriptStatuses = GetConscriptStatuses();
            //ViewBag.PossibleReligions = GetReligions();
            //ViewBag.PossibleFaithses = GetFaithses();
           
            return PartialView(MVC.Admin.AzmoonSetting.Views._Index, AzmoonId);
        }

        #region "AzmoonDegree"
        public virtual ActionResult AddAzmoonDegree(int AId)
        {
            ViewBag.PossibleDegrees = GetDegrees();

            AzmoonDegreeModel model = new AzmoonDegreeModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonDegree, model);
        }

        [HttpPost]
        public virtual ActionResult AddAzmoonDegree(AzmoonDegreeModel model)
        {
            ViewBag.PossibleDegrees = GetDegrees();

            if(!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا یک از مقاطع تحصیلی را انتخاب کنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonDegree, model);
            }
            var az = db.Azmoons.Find(model.AzmoonId);
            var d = db.degrees.Find(model.DegreeId);
            var azj = db.AzmoonDegrees.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.Code == d.degreeCode).ToList();
            if (azj.Any())
            {
                this.ErrorMessage("این مقطع تحصیلی قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = model.AzmoonId });
            }


            int MP = db.AzmoonDegrees.Where(x => x.Azmoon.AzmoonId == model.AzmoonId).Max(x => (int?)x.Periority) ?? 0;
            AzmoonDegree ad = new AzmoonDegree
            {
                  Azmoon=az,
                  Code=d.degreeCode,
                  Periority=MP+1,
                  Title=d.degreeTitle
            };

            db.AzmoonDegrees.Add(ad);
            db.SaveChanges();
            this.SuccessMessage("مقطع تحصیلی اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonDegrees, new { AId =model.AzmoonId});
        }

        public virtual ActionResult GetAllAzmoonDegrees(int AId)
        {
            var model = db.AzmoonDegrees.Include(x=>x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId)
                                        .OrderBy(x=>x.Periority).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllAzmoonDegrees, model);
        }


        [HttpDelete]
        public virtual ActionResult DeleteAzmoonDegree(int Id)
        {
            var models = db.AzmoonDegrees.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmoonDegrees.Remove(model);
            db.SaveChanges();
            this.WarningMessage("مقطع تحصیلی با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonDegrees, new { AId = AID });
        }

        [HttpPost]
        public virtual ActionResult UpAzmoonDegree(int Id,int AzmoonId)
        {
            var model1 = db.AzmoonDegrees.Find(Id);
            var models = db.AzmoonDegrees.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Periority < model1.Periority).Take(1).ToList();
            if(models.Count()>0)
            {
                var model2 = db.AzmoonDegrees.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;
              

                db.AzmoonDegrees.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonDegrees.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();             

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonDegrees, new { AId = AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult DownAzmoonDegree(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonDegrees.Find(Id);
            var models = db.AzmoonDegrees.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Periority > model1.Periority).Take(1).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonDegrees.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;


                db.AzmoonDegrees.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonDegrees.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonDegrees, new { AId = AzmoonId });
        }
        #endregion

        //---------------------------

        #region "AzmooneducatedSkill"
        public virtual ActionResult AddAzmoonEducationSkill(int AId)
        {
            ViewBag.PossibleEducationSkills = GetEducationSkills();

            AzmoonEducationViewModel model = new AzmoonEducationViewModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonEducationSkill, model);
        }
        [HttpPost]
        public virtual ActionResult AddAzmoonEducationSkill(AzmoonEducationViewModel model)
        {
            ViewBag.PossibleEducationSkills = GetEducationSkills();

            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا یکی از رشته های تحصیلی را انتخاب کنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonEducationSkill, model);
            }
            var az = db.Azmoons.Find(model.AzmoonId);
            var d = db.educatedSkills.Find(model.EducationId);

            var azj = db.AzmooneducatedSkills.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.AzmooneducatedSkillCode == d.educatedSkillCode).ToList();
            if (azj.Any())
            {
                this.ErrorMessage("این رشته تحصیلی قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = model.AzmoonId });
            }

            AzmooneducatedSkill ad = new AzmooneducatedSkill
            {
               Azmoon=az,
               AzmooneducatedSkillCode=d.educatedSkillCode,
               AzmooneducatedSkillTitle=d.educatedSkillTitle
            };

            db.AzmooneducatedSkills.Add(ad);
            db.SaveChanges();
            this.SuccessMessage("رشته تحصیلی اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonEducatedSkills, new { AId = model.AzmoonId });
        }
        public virtual ActionResult GetAllAzmoonEducatedSkills(int AId)
        {
            var model = db.AzmooneducatedSkills.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllAzmoonEducatedSkills, model);
        }
        [HttpDelete]
        public virtual ActionResult DeleteAzmooneducatedSkills(int Id)
        {
            var models = db.AzmooneducatedSkills.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmooneducatedSkills.Remove(model);
            db.SaveChanges();
            this.WarningMessage("رشته تحصیلی با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonEducatedSkills, new { AId = AID });
        }

        #endregion


        //---------------------------

        #region "AzmoonJob"
        public virtual ActionResult AddAzmoonJob(int AId)
        {
            ViewBag.PossibleJobs = GetJobs();

            AzmoonJobModel model = new AzmoonJobModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonJob, model);
        }

        [HttpPost]
        public virtual ActionResult AddAzmoonJob(AzmoonJobModel model)
        {
            ViewBag.PossibleJobs = GetJobs();

            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا یکی از رشته های شغلی را انتخاب کنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonJob, model);
            }
           
            var az = db.Azmoons.Find(model.AzmoonId);
            var d = db.Jobs.Find(model.JobId);

            var azj = db.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.AzmoonJobCode == d.JobCode).ToList();
            if(azj.Any())
            {
                this.ErrorMessage("این رشته شغلی قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = model.AzmoonId });
            }
            int MP = db.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == model.AzmoonId).Max(x => (int?)x.Periority) ?? 0;
            AzmoonJob ad = new AzmoonJob
            {
                Azmoon = az,
                AzmoonJobCode = d.JobCode,
                Periority = MP + 1,
                AzmoonJobTitle = d.JobTitle
            };

            db.AzmoonJobs.Add(ad);
            db.SaveChanges();
            this.SuccessMessage("رشته شغلی اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = model.AzmoonId });
        }

        public virtual ActionResult GetAllAzmoonJobs(int AId)
        {
            var model = db.AzmoonJobs.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId)
                                        .OrderBy(x => x.Periority).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllAzmoonJobs, model);
        }


        [HttpDelete]
        public virtual ActionResult DeleteAzmoonJob(int Id)
        {
            var models = db.AzmoonJobs.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmoonJobs.Remove(model);
            db.SaveChanges();
            this.WarningMessage("رشته شغلی با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = AID });
        }

        [HttpPost]
        public virtual ActionResult UpAzmoonJob(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonJobs.Find(Id);
            var models = db.AzmoonJobs.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId &&  x.Periority < model1.Periority).Take(1).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonJobs.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;


                db.AzmoonJobs.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonJobs.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult DownAzmoonJob(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonJobs.Find(Id);
            var models = db.AzmoonJobs.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Periority > model1.Periority).Take(1).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonJobs.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;


                db.AzmoonJobs.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonJobs.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonJobs, new { AId = AzmoonId });
        }
        #endregion

        //---------------------------

        #region "AzmoonPlace"
        public virtual ActionResult AddAzmoonPlace(int AId)
        {
            ViewBag.PossiblePlaces = GetPlaces();
            ViewBag.PossiblePlaceJobs = GetAzmoonJobs(AId);

            AzmoonPlaceModel model = new AzmoonPlaceModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonPlace, model);
        }

        [HttpPost]
        public virtual ActionResult AddAzmoonPlace(AzmoonPlaceModel model)
        {
            ViewBag.PossiblePlaces = GetPlaces();
            ViewBag.PossiblePlaceJobs = GetAzmoonJobs(model.AzmoonId);

            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا فرم را به صورت کامل پرکنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonPlace, model);
            }

            var az = db.Azmoons.Find(model.AzmoonId);
           // var j = db.Jobs.Find(model.PlaceJobId);
            var p = db.Places.Find(model.PlaceId);
           // int Icode = int.Parse(j.JobCode.ToString());
            var azjp = db.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.JobId == model.PlaceJobId && x.PlaceCode==p.PlaceCode).ToList();
            if (azjp.Any())
            {
                this.ErrorMessage("این اطلاعات قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonPlaces, new { AId = model.AzmoonId });
            }
            int MP = db.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == model.AzmoonId).Max(x => (int?)x.Periority) ?? 0;
            AzmoonPlace ad = new AzmoonPlace
            {
                Azmoon = az,
                Capacity = model.Capacity,
                Periority = MP + 1,
                JobId = model.PlaceJobId,
                PlaceCode = p.PlaceCode,
                PlaceTitle = p.PlaceTitle,
                WillMan=model.WillMan,
                WillWoman=model.WillWoman
            };

            db.AzmoonPlaces.Add(ad);
            db.SaveChanges();
            this.SuccessMessage("محل موردتقاضای جدید اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonPlaces, new { AId = model.AzmoonId });
        }

        public virtual ActionResult GetAllAzmoonPlaces(int AId)
        {
            var model = db.AzmoonPlaces.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId)
                                        .OrderBy(x => x.Periority).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllAzmoonPlaces, model);
        }


        [HttpDelete]
        public virtual ActionResult DeleteAzmoonPlace(int Id)
        {
            var models = db.AzmoonPlaces.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmoonPlaces.Remove(model);
            db.SaveChanges();
            this.WarningMessage("محل مورد تقاضا با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonPlaces, new { AId = AID });
        }

        [HttpPost]
        public virtual ActionResult UpAzmoonPlace(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonPlaces.Find(Id);
            var models = db.AzmoonPlaces.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Periority < model1.Periority).Take(1).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonPlaces.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;


                db.AzmoonPlaces.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonPlaces.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonPlaces, new { AId = AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult DownAzmoonPlace(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonPlaces.Find(Id);
            var models = db.AzmoonPlaces.OrderBy(x => x.Periority).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Periority > model1.Periority).Take(1).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonPlaces.Find(models.First().Id);
                var P1 = model2.Periority;
                var P2 = model1.Periority;


                db.AzmoonPlaces.Attach(model1);
                model1.Periority = P1;
                db.SaveChanges();

                db.AzmoonPlaces.Attach(model2);
                model2.Periority = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonPlaces, new { AId = AzmoonId });
        }
        #endregion

        //---------------------------

        #region "AzmoonDevotionType"
        public virtual ActionResult AddDevotionType(int AId)
        {
            ViewBag.PossibleDevotionTypes = GetDevotionTypes();

            DevotionTypeModel model = new DevotionTypeModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddDevotionType, model);
        }

        [HttpPost]
        public virtual ActionResult AddDevotionType(DevotionTypeModel model)
        {
            ViewBag.PossibleDevotionTypes = GetDevotionTypes();

            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا یکی از سهمیه های ایثارگری را انتخاب کنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddDevotionType, model);
            }

            var az = db.Azmoons.Find(model.AzmoonId);
            var d = db.DevotionTypes.Find(model.DevotionId);

            var azj = db.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.Grade == d.Grade).ToList();
            if (azj.Any())
            {
                this.ErrorMessage("این سهمیه قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllDevotionTypes, new { AId = model.AzmoonId });
            }
            int MP = db.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == model.AzmoonId).Max(x => (int?)x.Perority) ?? 0;
            AzmoonDevotionType ADT = new AzmoonDevotionType
            {
                Azmoon = az,
                Grade = d.Grade,
                Perority = MP + 1,
                DevotionTypeTitle = d.DevotionTypeTitle
            };

            db.AzmoonDevotionTypes.Add(ADT);
            db.SaveChanges();
            this.SuccessMessage("سهمیه ایثارگری اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllDevotionTypes, new { AId = model.AzmoonId });
        }

        public virtual ActionResult GetAllDevotionTypes(int AId)
        {
            var model = db.AzmoonDevotionTypes.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId)
                                        .OrderByDescending(x => x.Grade).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllDevotionTypes, model);
        }

        [HttpDelete]
        public virtual ActionResult DeleteDevotionType(int Id)
        {
            var models = db.AzmoonDevotionTypes.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmoonDevotionTypes.Remove(model);
            db.SaveChanges();
            this.WarningMessage("سهمیه ایثارگری با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllDevotionTypes, new { AId = AID });
        }

        [HttpPost]
        public virtual ActionResult UpDevotionType(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonDevotionTypes.Find(Id);
            var models = db.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Grade > model1.Grade).OrderByDescending(x => x.Grade).ToList();
           
            if (models.Any())
            {
                var model2 = db.AzmoonDevotionTypes.Find(models.Last().Id);
                var P1 = model2.Grade;
                var P2 = model1.Grade;


                db.AzmoonDevotionTypes.Attach(model1);
                model1.Grade = P1;
                db.SaveChanges();

                db.AzmoonDevotionTypes.Attach(model2);
                model2.Grade = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllDevotionTypes, new { AId = AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult DownDevotionType(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonDevotionTypes.Find(Id);
            var models = db.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Grade < model1.Grade).OrderByDescending(x => x.Grade).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonDevotionTypes.Find(models.First().Id);
                var P1 = model2.Grade;
                var P2 = model1.Grade;


                db.AzmoonDevotionTypes.Attach(model1);
                model1.Grade = P1;
                db.SaveChanges();

                db.AzmoonDevotionTypes.Attach(model2);
                model2.Grade = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllDevotionTypes, new { AId = AzmoonId });
        }
        #endregion

        //---------------------------

        #region "AzmoonQoutaType"

        public virtual ActionResult AddAzmoonQoutaType(int AId)
        {
            ViewBag.PossibleQoutaTypes = GetQoutaTypes();

            AzmoonQoutaTypeModel model = new AzmoonQoutaTypeModel
            {
                AzmoonId = AId
            };

            return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonQoutaType, model);
        }

        [HttpPost]
        public virtual ActionResult AddAzmoonQoutaType(AzmoonQoutaTypeModel model)
        {
            ViewBag.PossibleQoutaTypes = GetQoutaTypes();

            if (!ModelState.IsValid)
            {
                this.ErrorMessage("لطفا یکی از سهمیه ها را انتخاب کنید");
                return PartialView(MVC.Admin.AzmoonSetting.Views._AddAzmoonQoutaType, model);
            }

            var az = db.Azmoons.Find(model.AzmoonId);
            var d = db.QoutaTypes.Find(model.QoutaTypeId);

            var azj = db.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.Grade == d.Grade).ToList();
            if (azj.Any())
            {
                this.ErrorMessage("این سهمیه قبلا ثبت شده");
                return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonQoutaTypes, new { AId = model.AzmoonId });
            }
          
            AzmoonQoutaType ad = new AzmoonQoutaType
            {
                Azmoon = az,
                Grade = d.Grade,
                QoutaTypeTitle = d.QoutaTypeTitle
            };

            db.AzmoonQoutaTypes.Add(ad);
            db.SaveChanges();
            this.SuccessMessage("سهمیه اضافه شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonQoutaTypes, new { AId = model.AzmoonId });
        }

        public virtual ActionResult GetAllAzmoonQoutaTypes(int AId)
        {
            var model = db.AzmoonQoutaTypes.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == AId)
                                        .OrderByDescending(x=>x.Grade).ToList();
            return PartialView(MVC.Admin.AzmoonSetting.Views._GetAllAzmoonQoutaTypes, model);
        }
        
        [HttpDelete]
        public virtual ActionResult DeleteAzmoonQoutaType(int Id)
        {
            var models = db.AzmoonQoutaTypes.Include(x => x.Azmoon).Where(x => x.Id == Id).ToList();
            var model = models.First();
            var AID = model.Azmoon.AzmoonId;
            db.AzmoonQoutaTypes.Remove(model);
            db.SaveChanges();
            this.WarningMessage("سهمیه با موفقیت  حذف شد");
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonQoutaTypes, new { AId = AID });
        }
        
        [HttpPost]
        public virtual ActionResult UpAzmoonQoutaTypee(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonQoutaTypes.Find(Id);
            var models = db.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Grade > model1.Grade).OrderByDescending(x => x.Grade).ToList();

            if (models.Any())
            {
                var model2 = db.AzmoonQoutaTypes.Find(models.Last().Id);
                var P1 = model2.Grade;
                var P2 = model1.Grade;


                db.AzmoonQoutaTypes.Attach(model1);
                model1.Grade = P1;
                db.SaveChanges();

                db.AzmoonQoutaTypes.Attach(model2);
                model2.Grade = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonQoutaTypes, new { AId = AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult DownAzmoonQoutaType(int Id, int AzmoonId)
        {
            var model1 = db.AzmoonQoutaTypes.Find(Id);
            var models = db.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Grade < model1.Grade).OrderByDescending(x => x.Grade).ToList();
            if (models.Count() > 0)
            {
                var model2 = db.AzmoonQoutaTypes.Find(models.First().Id);
                var P1 = model2.Grade;
                var P2 = model1.Grade;


                db.AzmoonQoutaTypes.Attach(model1);
                model1.Grade = P1;
                db.SaveChanges();

                db.AzmoonQoutaTypes.Attach(model2);
                model2.Grade = P2;
                db.SaveChanges();

            }

            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.GetAllAzmoonQoutaTypes, new { AId = AzmoonId });
        }
        #endregion


        //----------------------------

        public virtual ActionResult ShowLinks(int AId)
        {
            var a = db.Azmoons.Find(AId);
            ButtonLinksViewModel BLVM = new ButtonLinksViewModel
            {
                AzmoonId = AId,
                Active = a.Active,
                ShowAzmoon = a.ShowAzmoon,
                ShowDownloadAgahi = a.ShowDownloadAgahi,
                ShowCanPrintResult = a.ShowCanPrintResult,
                ShowPrintCard = a.ShowPrintCard,
                ShowRegisters = a.ShowRegisters,
                ShowCanEditResult=a.ShowCanEditResult,
                ShowEveryOneSee=a.EveryOneSee
            };
            return PartialView(MVC.Admin.AzmoonSetting.Views._ShowLinks, BLVM);
        }

        public virtual ActionResult ActiveAzmoonLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.Active = true;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });        
        }

        public virtual ActionResult DontActiveAzmoonLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.Active = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }

        public virtual ActionResult ShowAgahiLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowDownloadAgahi = true;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult DontShowAgahiLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowDownloadAgahi = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }

        public virtual ActionResult ShowRegisterLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowRegisters = true;
            az.EndRegister = false;
            az.ShowCanPrintResult = false;
            az.ShowPrintCard = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult DontShowRegisterLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowRegisters = false;
            az.EndRegister = true;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }

        public virtual ActionResult ShowViewCardLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowRegisters = false;
            az.ShowCanPrintResult = false;
            az.ShowPrintCard = true;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult DontShowViewCardLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowPrintCard = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }

        public virtual ActionResult ShowViewResultLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowRegisters = false;
            az.ShowCanPrintResult = true;
            az.ShowPrintCard = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult DontShowViewResultLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowCanPrintResult = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }


        public virtual ActionResult ShowViewEditLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowCanEditResult = true;
            az.ShowPrintCard = false;
            az.ShowCanPrintResult = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult DontShowViewEditLink(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.ShowCanEditResult = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }



        public virtual ActionResult ShowAzmoonForUser(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.EveryOneSee = false;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }
        public virtual ActionResult ShowAzmoonForEveryOne(int AID)
        {
            var az = db.Azmoons.Find(AID);
            db.Azmoons.Attach(az);
            az.EveryOneSee = true;
            db.SaveChanges();
            return RedirectToAction(MVC.Admin.AzmoonSetting.ActionNames.ShowLinks, new { AId = AID });
        }


        #region "Private Method"
        private SelectList GetGender()
        {
            var qGender = (from p in db.Genders
                           select new SelectListItem
                           {
                               Text = p.GenderTitle,
                               Value = p.GenderCode.ToString()
                           }).ToList();

            return new SelectList(qGender, "Value", "Text");

        }
        private SelectList GetMarriageStatus()
        {
            var qMarriage = (from p in db.marriageStatuses
                             select new SelectListItem
                             {
                                 Text = p.marriageStatusTitle,
                                 Value = p.marriageStatusCode.ToString()
                             }).ToList();
            return new SelectList(qMarriage, "Value", "Text");
        }
        private SelectList GetConscriptStatuses()
        {
            var qConscriptStatus = (from p in db.ConscriptStatuses
                                    select new SelectListItem
                                    {
                                        Text = p.ConscriptStatusTitle,
                                        Value = p.ConscriptStatusCode.ToString()
                                    }).ToList();

            return new SelectList(qConscriptStatus, "Value", "Text");
        }
        private SelectList GetReligions()
        {
            var qReligion = (from p in db.Religions
                             select new SelectListItem
                             {
                                 Text = p.ReligionTitle,
                                 Value = p.ReligionCode.ToString()
                             }).ToList();

            return new SelectList(qReligion, "Value", "Text");
        }
        private SelectList GetFaithses()
        {
            var qFaithse = (from p in db.Faiths
                            select new SelectListItem
                            {
                                Text = p.FaithTitle,
                                Value = p.FaithCode.ToString()
                            }).ToList();

            return new SelectList(qFaithse, "Value", "Text");


        }
        private SelectList GetDegrees()
        {
            var qDegree = (from p in db.degrees
                           select new SelectListItem
                           {
                               Text = p.degreeTitle,
                               Value = p.Id.ToString()
                           }).ToList();

            return new SelectList(qDegree, "Value", "Text");

        }
        private SelectList GetEducationSkills()
        {
            var qeducatedSkills = (from p in db.educatedSkills
                                   select new SelectListItem
                                   {
                                       Text = p.educatedSkillTitle,
                                       Value = p.Id.ToString()
                                   }).ToList();

            return new SelectList(qeducatedSkills, "Value", "Text");

        }
        private SelectList GetQoutaTypes()
        {
            var qQoutaTypes = (from p in db.QoutaTypes.OrderByDescending(x=>x.Grade)
                               select new SelectListItem
                               {
                                   Text = p.QoutaTypeTitle,
                                   Value = p.Id.ToString()
                               }).ToList();

            return new SelectList(qQoutaTypes, "Value", "Text");

        }
        private SelectList GetDevotionTypes()
        {
            var qDevotionType = (from p in db.DevotionTypes.OrderByDescending(x => x.Grade)
                                 select new SelectListItem
                                 {
                                     Text = p.DevotionTypeTitle,
                                     Value = p.Id.ToString()
                                 }).ToList();

            return new SelectList(qDevotionType, "Value", "Text");

        }
        private SelectList GetPlaces()
        {
            var qPlaces = (from p in db.Places
                           select new SelectListItem
                           {
                               Text = p.PlaceTitle,
                               Value = p.Id.ToString()
                           }).ToList();

            return new SelectList(qPlaces, "Value", "Text");

        }
        private SelectList GetJobs()
        {
            var qJobs = (from p in db.Jobs
                         select new SelectListItem
                         {
                             Text = p.JobTitle,
                             Value = p.Id.ToString()
                         }).ToList();

            return new SelectList(qJobs, "Value", "Text");

        }
        private SelectList GetPlanStatus()
        {

            var qPlanStatus = (from p in db.PlaneStatuses
                               select new SelectListItem
                               {
                                   Text = p.PlaneStatusTitle,
                                   Value = p.Id.ToString()
                               }).ToList();
            return new SelectList(qPlanStatus, "Value", "Text");
        }

        //-------------------------------------------
        private SelectList GetAzmoonDegrees(int AzmoonId)
        {
            var qDegree = (from p in db.AzmoonDegrees
                           where p.Azmoon.AzmoonId == AzmoonId
                           orderby p.Periority ascending
                           select new SelectListItem
                           {
                               Text = p.Title,
                               Value = p.Code.ToString()
                           }).ToList();
            return new SelectList(qDegree, "Value", "Text");
        }
        private SelectList GetAzmoonEducatedSkills(int AzmoonId)
        {
            var qEducatedSkill = (from p in db.AzmooneducatedSkills
                                  where p.Azmoon.AzmoonId == AzmoonId
                                  select new SelectListItem
                                  {
                                      Text = p.AzmooneducatedSkillTitle,
                                      Value = p.AzmooneducatedSkillCode.ToString()
                                  }).ToList();

            return new SelectList(qEducatedSkill, "Value", "Text");
        }
        private SelectList GetAzmoonJobs(int AzmoonId)
        {
            var qJob = (from p in db.AzmoonJobs
                        where p.Azmoon.AzmoonId == AzmoonId
                        orderby p.Periority
                        select new SelectListItem
                        {
                            Text = p.AzmoonJobTitle,
                            Value = p.AzmoonJobCode.ToString()
                        }).ToList();
            return new SelectList(qJob, "Value", "Text");
        }
        private SelectList GetAzmoonPlaces()
        {

            var qPlace = (from p in db.AzmoonPlaces
                          select new SelectListItem
                          {
                              Text = p.PlaceTitle,
                              Value = p.PlaceCode.ToString()
                          }).ToList();

            return new SelectList(qPlace, "Value", "Text");
        }

        #endregion
      




    }
}