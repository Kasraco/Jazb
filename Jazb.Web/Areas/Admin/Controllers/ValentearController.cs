using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.ValentearModel;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Servicelayer.Interfaces;
using Jazb.Web.Filters;
using Jazb.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Areas.Admin.Controllers
{

    
    public partial class ValentearController : Controller
    {
        private JazbDbContext context = new JazbDbContext();
        private readonly IUnitOfWork _uow;
        private readonly IValentearService _valentearService;
        private readonly IJobCityService _JobCityService;

        public ValentearController(IUnitOfWork uow, IValentearService valentearService, IJobCityService JobCityService)
        {
            _uow = uow;
            _valentearService = valentearService;
            _JobCityService = JobCityService;

        }



       


        //
        // GET: /Admin/Valentear/


        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult Updateamount()
        {
            var Vs = context.Valentears.Where(x => x.IsPaymented == true).ToList();
              foreach(var v in Vs)
              {
                  var blnempty = true;
                    var Pays = context.Payments.Where(x => x.ValentearId == v.Id && x.AzmoonId == v.Azmoon.AzmoonId).ToList();
                    foreach (var t in Pays)
                    {
                         if (!string.IsNullOrEmpty(t.ReferenceNumber) && !string.IsNullOrWhiteSpace(t.ReferenceNumber))
                         {
                             blnempty = false;
                             if (v.AmountValue == "150000" || v.AmountValue == "100000")
                             {
                                 v.AmountValue = t.ReferenceNumber.ToString();
                                 v.IsPaymented = true;
                                 context.Valentears.Attach(v);
                                 context.Entry(v).Property(x => x.AmountValue).IsModified = true;
                                 context.Entry(v).Property(x => x.IsPaymented).IsModified = true;
                                 context.SaveChanges();
                             }
                         }
                    }
                  if(blnempty)
                  {
                      v.AmountValue = "پرداخت نشده";
                      v.IsPaymented = false;
                      context.Valentears.Attach(v);
                      context.Entry(v).Property(x => x.AmountValue).IsModified = true;
                      context.Entry(v).Property(x => x.IsPaymented).IsModified = true;
                      context.SaveChanges();
                  }
              }

              return View();
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult Index()
        {
            return PartialView(MVC.Admin.Valentear.Views._Index);
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult Search(int? AzmoonId)
        {
            if (AzmoonId != null)
                ViewBag.PossibleAzmoon = AzmoonId.Value;
            return PartialView(MVC.Admin.Valentear.Views._Search);
        }
        public void CreateNumber(int AzmoonId, Jazb.DomainClasses.Entities.AzmoonJob taj)
        {
            int StartNumberMan = 0;
            int StartNumberWoman = 0;
            int Index = 0;

            StartNumberMan = taj.ManFrom;
            StartNumberWoman = taj.WomanFrom;
            var VManlist = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.GenderId == 2 && x.JobId == taj.AzmoonJobCode && x.Accept == 1).ToList();
            var VWomanlist = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.GenderId == 1 && x.JobId == taj.AzmoonJobCode && x.Accept == 1).ToList();
            Index = StartNumberWoman;
            foreach (var v in VWomanlist)
            {
                v.ChairCode = Index.ToString();
                context.Valentears.Attach(v);
                context.Entry(v).Property(x => x.ChairCode).IsModified = true;

                context.SaveChanges();
                Index = Index + 1;

            }


            Index = StartNumberMan;
            foreach (var v in VManlist)
            {
                v.ChairCode = Index.ToString();
                context.Valentears.Attach(v);
                context.Entry(v).Property(x => x.ChairCode).IsModified = true;

                context.SaveChanges();
                Index = Index + 1;
            }

        }

        [Authorize(Roles = "mostafa")]
        public virtual ActionResult CreateChairNumber(int AzmoonId)
        {
            var qAJ = context.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();

            foreach (var taj in qAJ)
            {
                CreateNumber(AzmoonId, taj);
            }

            return RedirectToAction(MVC.Admin.Valentear.ActionNames.DataTable, MVC.Admin.Valentear.Name);
        }


        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult DataTable(string term = "", int page = 0, int count = 10, ValentearSearchBy searchBy = ValentearSearchBy.Melicode)
        {

            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.SEARCHBY = searchBy;

            IList<Valentear> selectedValentear = _valentearService.GetAllValentearDataTable(term, page, count, searchBy);
          

            var qJobItem = (from p in context.AzmoonJobs
                            select new SelectListItem
                            {
                                Text = p.AzmoonJobTitle,
                                Value = p.AzmoonJobCode.ToString()
                            }).ToList();
            ViewBag.JobItems = new SelectList(qJobItem, "Value", "Text", 40401);

            ViewBag.CountList = DropDownList.CountList(count);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _valentearService.Count : selectedValentear.Count();

            List<ValentearModel> VM = new List<ValentearModel>();
            AutoMapper.Mapper.Map(selectedValentear, VM);
            foreach (var t in VM)
            {
                t.JobTitle = GetJobTitle(t.JobId);
                t.PlaceTitle = GetPlaceTitle(t.PlaceId);
            }

            return PartialView(MVC.Admin.Valentear.Views._DataTable, VM);
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult GetDataTableByAzmoon(int? AzmoonId, string term = "", int page = 0, int count = 10, ValentearSearchBy searchBy = ValentearSearchBy.Melicode)
        {
            if (AzmoonId == null || AzmoonId == 0)
                return RedirectToAction(MVC.Admin.Valentear.ActionNames.DataTable, MVC.Admin.Valentear.Name, new { term = term, searchBy = searchBy });
            ViewBag.TERM = term;
            ViewBag.PAGE = page;
            ViewBag.COUNT = count;
            ViewBag.SEARCHBY = searchBy;
            ViewBag.PossibleAzmoon = AzmoonId;
            IList<Valentear> selectedValentear = _valentearService.GetAllValentearDataTable(AzmoonId.Value, term, page, count, searchBy);

            var inttotal = _valentearService.GetAllValentearDataTable(AzmoonId.Value, term, searchBy);

            var qJobItem = (from p in context.AzmoonJobs
                            where p.Azmoon.AzmoonId == AzmoonId
                            select new SelectListItem
                            {
                                Text = p.AzmoonJobTitle,
                                Value = p.AzmoonJobCode.ToString()
                            }).ToList();
            ViewBag.JobItems = new SelectList(qJobItem, "Value", "Text", 40401);

            ViewBag.CountList = DropDownList.CountList(count);

            ViewBag.TotalRecords = (string.IsNullOrEmpty(term)) ? _valentearService.CountValentear(AzmoonId.Value) : inttotal;

            List<ValentearModel> VM = new List<ValentearModel>();
            AutoMapper.Mapper.Map(selectedValentear, VM);
            foreach (var t in VM)
            {
                t.JobTitle = GetJobTitle(t.JobId);
                t.PlaceTitle = GetPlaceTitle(t.PlaceId);
            }

            return PartialView(MVC.Admin.Valentear.Views._AzmoonDataTable, VM);
        }

        [HttpGet]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult Edit(int AzmoonId, string NationalId)
        {
            var qvlst = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.NationalID.Equals(NationalId)).ToList();
            if (qvlst.Count() > 0)
            {
                var V = qvlst.First();

                IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
                IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();



                ViewBag.PossibleGenders = GetGender(V.GenderId);
                ViewBag.PossibleReligions = GetReligions(V.ReligionId);
                ViewBag.PossibleFaiths = GetFaithses(V.FaithId);
                ViewBag.PossibleConscriptStatus = GetConscriptStatuses(V.ConscriptStatusId);
                ViewBag.PossibleMarriageStatus = GetMarriageStatus(V.MarriageStatusId);
                ViewBag.PossibleDegrees = GetDegrees(V.DegreeId,AzmoonId);
                ViewBag.PossibleEducatedSkills = GetEducatedSkills(V.EducatedSkillId,AzmoonId);
                ViewBag.PossibleJobs = GetJobs(V.JobId,AzmoonId);
                ViewBag.PossiblePlaces = GetPlaces(V.PlaceId);
                ViewBag.PossiblePlanStatus = GetPlanStatus(V.PlanStatusId);
                ViewBag.PossibleCooperationHistories = GetCooperationHistories(V.CooperationHistoryId);


                Jazb.Model.ValentearModel.RegisterModel RM = new Model.ValentearModel.RegisterModel();
                ValentearModel VM = new ValentearModel();
                AutoMapper.Mapper.Map(V, VM);
                VM.PlaceTitle = GetPlaceTitle(V.PlaceId);
                VM.JobTitle = GetJobTitle(V.JobId);
                VM.AzmoonId = AzmoonId;
                V.Azmoon.AzmoonId = AzmoonId;


                foreach (var t in context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                    lstADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = t.DevotionTypeTitle, Grade = t.Grade, Id = t.Id });


                foreach (var t in context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                    lstQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = t.QoutaTypeTitle, Grade = t.Grade, Id = t.Id });


                var ListAQV = AutoMapper.Mapper.Map<List<AzmoonQoutasValentear>, IEnumerable<Jazb.Model.ValentearModel.QoutaTypeModel>>(V.AzmoonQoutasValentears.ToList());
                var ListADV = AutoMapper.Mapper.Map<List<AzmoonDevotionValentear>, IEnumerable<Jazb.Model.ValentearModel.DevotionTypeModel>>(V.AzmoonDevotionValentearas.ToList());

                RM.Valentear = VM;
                RM.DevotionTypes = lstADT.OrderBy(x => x.Grade) ;
                RM.SelectedDevotionTypes = ListADV.OrderBy(x => x.Grade).ToList();
                RM.QoutaTypes = lstQTM;
                RM.SelectedQoutaTypes = ListAQV.ToList();

                return PartialView(MVC.Admin.Valentear.Views._EditVlentear, RM);
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult Edit(RegisterModel RM)
        {

            #region "Initialize Data For Return View"
            IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

            IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstSelectedADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstSelectedQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();



            foreach (var t in context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId).ToList())
                lstADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = t.DevotionTypeTitle, Grade = t.Grade, Id = t.Id });


            foreach (var t in context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId).ToList())
                lstQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = t.QoutaTypeTitle, Grade = t.Grade, Id = t.Id });

            if (RM.PostedDevotionType == null)
            {

                var qPDT = context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade == 0).ToList();

                RM.PostedDevotionType = new Model.ValentearModel.PostedDevotionType();
                RM.PostedDevotionType.DevotionTypes = new string[] { qPDT.First().Id.ToString() };

            }
            foreach (var tid in RM.PostedDevotionType.DevotionTypes)
            {
                int idD = int.Parse(tid);
                var qPDT = context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade.Equals(idD)).ToList();
                if (qPDT.Count() > 0)
                    lstSelectedADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = qPDT.First().DevotionTypeTitle, Grade = qPDT.First().Grade, Id = qPDT.First().Id });

            }

            if (RM.PostedQoutaType == null)
            {

                var qQT = context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade == 0).ToList();
                RM.PostedQoutaType = new Model.ValentearModel.PostedQoutaType();
                RM.PostedQoutaType.QoutaTypes = new string[] { qQT.First().Id.ToString() };
            }
            foreach (var tid in RM.PostedQoutaType.QoutaTypes)
            {
                int idD = int.Parse(tid);
                var qPDT = context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade.Equals(idD)).ToList();
                if (qPDT.Count() > 0)
                    lstSelectedQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = qPDT.First().QoutaTypeTitle, Grade = qPDT.First().Grade, Id = qPDT.First().Id });

            }


            RM.DevotionTypes = lstADT.OrderBy(x => x.Grade).ToList();
            RM.QoutaTypes = lstQTM;
            RM.SelectedDevotionTypes = lstSelectedADT;
            RM.SelectedQoutaTypes = lstSelectedQTM;
            #endregion




            RM.Valentear.GenderTitle = GetGenderTitle(RM.Valentear.GenderId);
            RM.Valentear.ReligionTitle = GetReligionTitle(RM.Valentear.ReligionId);
            RM.Valentear.FaithTitle = GetFaithseTitle(RM.Valentear.FaithId);
            RM.Valentear.MarriageStatusTitle = GetMarriageStatuTitle(RM.Valentear.MarriageStatusId);
            RM.Valentear.DegreeTitle = GetDegreeTitle(RM.Valentear.DegreeId,RM.Valentear.AzmoonId);
            RM.Valentear.EducatedSkillTitle = GetEducatedSkillTitle(RM.Valentear.EducatedSkillId);
            RM.Valentear.JobTitle = GetJobTitle(RM.Valentear.JobId);
            RM.Valentear.PlaceTitle = GetPlaceTitle(RM.Valentear.PlaceId);
            RM.Valentear.PlanStatusTitle = GetPlanStatuTitle(RM.Valentear.PlanStatusId);
            RM.Valentear.ConscriptStatusTitle = GetConscriptStatuseTitle(RM.Valentear.ConscriptStatusId);
            RM.Valentear.CooperationHistoryTitle = GetCooperationHistorieTitle(RM.Valentear.CooperationHistoryId);

            var az = context.Azmoons.Find(RM.Valentear.AzmoonId);
            var CompanyName = context.Options.Where(x => x.Name == "SiteName" && x.Value != null).First();

            ViewBag.AzmoonName = az.Title;
            ViewBag.CompanyName = CompanyName.Value;

            var personel_picture = Request.Files["personel_picture"];
            var fish_variz = Request.Files["fish_variz"];
            var Pic_Tarh = Request.Files["Pic_Tarh"];
            var Pic_Conscript = Request.Files["Pic_Conscript"];
            // var fish_variz2 = Request.Files["fish_variz2"];

            Session["KRB_personel_picture"] = "0";
            Session["KRB_fish_variz"] = "0";
            Session["KRB_Pic_Tarh"] = "0";
            Session["KRB_Pic_Conscript"] = "0";

            if (personel_picture!=null)
                if ((personel_picture.ContentLength != 0) || (!string.IsNullOrEmpty(personel_picture.FileName)))
                {
                    RM.Valentear.PictureValenteer = getBytefile(personel_picture);
                    Session["KRB_personel_picture"] = "1";
                }

            if (fish_variz != null)
            if ((fish_variz.ContentLength != 0) || (!string.IsNullOrEmpty(fish_variz.FileName)))
            {
                RM.Valentear.FishVariszi = getBytefile(fish_variz);
                Session["KRB_fish_variz"] = "1";
            }

            if (Pic_Tarh != null)
            if ((Pic_Tarh.ContentLength != 0) || (!string.IsNullOrEmpty(Pic_Tarh.FileName)))
            {
                RM.Valentear.Pic_Tarth = getBytefile(Pic_Tarh);
                Session["KRB_Pic_Tarh"] = "1";
            }

            if (Pic_Conscript != null)
            if ((Pic_Conscript.ContentLength != 0) || (!string.IsNullOrEmpty(Pic_Conscript.FileName)))
            {
                RM.Valentear.Pic_Conscript = getBytefile(Pic_Conscript);
                Session["KRB_Pic_Conscript"] = "1";
            }


            //RM.Valentear.PictureValenteer = getBytefile(personel_picture);
            //RM.Valentear.FishVariszi = getBytefile(fish_variz);
            //RM.Valentear.Pic_Tarth = getBytefile(Pic_Tarh);



            if (!ModelState.IsValid)
            {
                foreach (var t in ModelState)
                {
                    if (t.Value.Errors.Count > 0)
                    {

                        ErrorCode ec = new ErrorCode();
                        ec.FieldName = t.Key;
                        ec.Melicode = RM.Valentear.NationalID;
                        ec.TrackingCode = RM.Valentear.TrackingCode;
                        ec.value = (t.Value.Value == null ? "" : t.Value.Value.AttemptedValue);
                        context.ErrorCodes.Add(ec);
                        context.SaveChanges();
                        ModelState.AddModelError(t.Key, "خطا را برطرف کنید");

                    }
                }
            }



            if (ModelState.IsValid)
            {
                Session["KRBRM"] = RM;
                return PartialView(MVC.Admin.Valentear.Views.ViewDataBeforSave, RM);
            }

            return View(RM);
        }

        [HttpPost]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult SaveDate(RegisterModel RM)
        {

            if (Session["KRBRM"] == null)
                return RedirectToAction(MVC.Admin.Valentear.ActionNames.Edit, MVC.Admin.Valentear.Name);



            if (Session["KRBRM"] != null)
            {
                RM = (Jazb.Model.ValentearModel.RegisterModel)Session["KRBRM"];

             //   RM.Valentear.DateRegister = DateTime.Now;
              
                var V = context.Valentears.Find(RM.Valentear.Id);
                AutoMapper.Mapper.Map(RM.Valentear, V);
                V.Edit = 1;
                V.EditUser = 11;// int.Parse(System.Web.Security.Membership.GetUser().ProviderUserKey.ToString());
                V.LastDateEdit = DateTime.Now;
                V.MaxQoutaType = int.Parse(RM.SelectedQoutaTypes.Max(x => x.Grade).ToString());
                V.SumQoutaType = int.Parse(RM.SelectedQoutaTypes.Sum(x => x.Grade).ToString());
                V.TextQoutaType = ConvertStringArrayToStringJoin("-", RM.SelectedQoutaTypes.Select(x => x.QoutaTypeTitle).ToArray());

                V.MaxDevotionType = int.Parse(RM.SelectedDevotionTypes.Max(x => x.Grade).ToString());
                V.SumDevotionType = int.Parse(RM.SelectedDevotionTypes.Sum(x => x.Grade).ToString());
                V.TextDevotionType = ConvertStringArrayToStringJoin("-", RM.SelectedDevotionTypes.Select(x => x.DevotionTypeTitle).ToArray());

                if ((V.FightInWarYear != null) || (V.FightInWarMonth != null) || (V.FightInWarDay != null))
                    if ((V.FightInWarYear != "0") || (V.FightInWarMonth != "0") || (V.FightInWarDay != "0"))
                    {
                        V.SumQoutaType = V.SumQoutaType + 8192;
                        V.MaxQoutaType = 8192;
                        V.TextQoutaType = string.Format("رزمنده  مدت حضور درجبهه {0} سال {1} ماه {2} روز", V.FightInWarYear, V.FightInWarMonth, V.FightInWarDay) + V.TextQoutaType;
                    }

                if ((V.CaptivationDay != null) || (V.CaptivationMonth != null) || (V.CaptivationYear != null))
                    if ((V.CaptivationDay != "0") || (V.CaptivationMonth != "0") || (V.CaptivationYear != "0"))
                    {
                        V.SumQoutaType = V.SumQoutaType + 16384;
                        V.MaxQoutaType = 16384;
                        V.TextQoutaType = string.Format("آزاده  مدت اسارت {0} سال {1} ماه {2} روز", V.CaptivationYear, V.CaptivationMonth, V.CaptivationDay) + V.TextQoutaType;
                    }


                //if ((!string.IsNullOrEmpty(V.WoundedPercent)))
                //{
                //    V.SumQoutaType = V.SumQoutaType + 32768;
                //    V.MaxQoutaType = 32768;
                //    V.TextQoutaType = "جانباز " + V.WoundedPercent + " درصد - " + V.TextQoutaType;
                //}


                var ListAQV = AutoMapper.Mapper.Map<IEnumerable<Jazb.Model.ValentearModel.QoutaTypeModel>, List<AzmoonQoutasValentear>>(RM.SelectedQoutaTypes);
                var ListADV = AutoMapper.Mapper.Map<IEnumerable<Jazb.Model.ValentearModel.DevotionTypeModel>, List<AzmoonDevotionValentear>>(RM.SelectedDevotionTypes);

                var qdvList = context.Valentears.Find(V.Id).AzmoonDevotionValentearas.ToList();
                var qqvList = context.Valentears.Find(V.Id).AzmoonQoutasValentears.ToList();

                foreach (var t in qdvList)
                    context.AzmoonDevotionValentears.Remove(t);

                foreach (var t in qqvList)
                    context.AzmoonQoutasValentears.Remove(t);
                context.SaveChanges();

                // context.Valentears.Attach(V);

                context.SaveChanges();

                Session["KRBKEYVID"] = V.Id;
                foreach (var t in ListADV)
                {
                    t.Valentear = V;
                    context.AzmoonDevotionValentears.Add(t);
                }
                foreach (var t in ListAQV)
                {
                    t.Valentear = V;
                    context.AzmoonQoutasValentears.Add(t);
                }
                context.Valentears.Attach(V);
               // context.Entry(V).State = EntityState.Modified;
                context.Entry(V).Property(x => x.ChairCode).IsModified = false ;
                if (Session["KRB_Pic_Tarh"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic_Tarth).IsModified = false;

                if (Session["KRB_personel_picture"].ToString() == "0")
                    context.Entry(V).Property(x => x.PictureValenteer).IsModified = false;

                if (Session["KRB_fish_variz"].ToString() == "0")
                    context.Entry(V).Property(x => x.FishVariszi).IsModified = false;

                if (Session["KRB_Pic_Conscript"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic_Conscript).IsModified = false;

                context.SaveChanges();
                Session["KRBRM"] = null;

            }

            return RedirectToAction(MVC.Admin.Valentear.ActionNames.PrintData, MVC.Admin.Valentear.Name);

        }

        [HttpGet]
        public virtual ActionResult EditNOID()
        {
            int i = 0;
            var q = context.Valentears.ToList();
            foreach (var t in q)
            {
                t.BirthCertificateNoID = Convert.ToDecimal(t.BirthCertificateNo.Trim());
                context.Valentears.Attach(t);
                context.Entry(t).State = EntityState.Unchanged;
                context.Entry(t).Property(x => x.BirthCertificateNoID).IsModified = true;
                i++;
            }
            context.SaveChanges();
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult EditSahmiyeh(int AzmoonId, string NationalId)
        {
                 var qvlst = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.NationalID.Equals(NationalId)).ToList();
                 if (qvlst.Count() > 0)
                 {
                     var V = qvlst.First();

                     IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
                     IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

                     Jazb.Model.ValentearModel.RegisterModel RM = new Model.ValentearModel.RegisterModel();
                     ValentearModel VM = new ValentearModel();
                     AutoMapper.Mapper.Map(V, VM);
                     VM.PlaceTitle = GetPlaceTitle(V.PlaceId);
                     VM.JobTitle = GetJobTitle(V.JobId);
                     VM.AzmoonId = AzmoonId;
                     V.Azmoon.AzmoonId = AzmoonId;

                     foreach (var t in context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                         lstADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = t.DevotionTypeTitle, Grade = t.Grade, Id = t.Id });


                     foreach (var t in context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                         lstQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = t.QoutaTypeTitle, Grade = t.Grade, Id = t.Id });


                     var ListAQV = AutoMapper.Mapper.Map<List<AzmoonQoutasValentear>, IEnumerable<Jazb.Model.ValentearModel.QoutaTypeModel>>(V.AzmoonQoutasValentears.ToList());
                     var ListADV = AutoMapper.Mapper.Map<List<AzmoonDevotionValentear>, IEnumerable<Jazb.Model.ValentearModel.DevotionTypeModel>>(V.AzmoonDevotionValentearas.ToList());

                     RM.DevotionTypes = lstADT;
                     RM.SelectedDevotionTypes = ListADV.ToList();
                     RM.QoutaTypes = lstQTM;
                     RM.SelectedQoutaTypes = ListAQV.ToList();

                     return PartialView(MVC.Admin.Valentear.Views._EditSahmiyeh, RM);
                 }

                 return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult EditSahmiyeh(RegisterModel RM)
        {





            //#region "Initialize Data For Return View"


            //IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstSelectedADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            //IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstSelectedQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

            //if (RM.PostedDevotionType == null)
            //{

            //    var qPDT = context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade == 0).ToList();

            //    RM.PostedDevotionType = new Model.ValentearModel.PostedDevotionType();
            //    RM.PostedDevotionType.DevotionTypes = new string[] { qPDT.First().Id.ToString() };

            //}
            //foreach (var tid in RM.PostedDevotionType.DevotionTypes)
            //{
            //    int idD = int.Parse(tid);
            //    var qPDT = context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade.Equals(idD)).ToList();
            //    if (qPDT.Count() > 0)
            //        lstSelectedADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = qPDT.First().DevotionTypeTitle, Grade = qPDT.First().Grade, Id = qPDT.First().Id });

            //}

            //if (RM.PostedQoutaType == null)
            //{

            //    var qQT = context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade == 0).ToList();
            //    RM.PostedQoutaType = new Model.ValentearModel.PostedQoutaType();
            //    RM.PostedQoutaType.QoutaTypes = new string[] { qQT.First().Id.ToString() };
            //}
            //foreach (var tid in RM.PostedQoutaType.QoutaTypes)
            //{
            //    int idD = int.Parse(tid);
            //    var qPDT = context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Grade.Equals(idD)).ToList();
            //    if (qPDT.Count() > 0)
            //        lstSelectedQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = qPDT.First().QoutaTypeTitle, Grade = qPDT.First().Grade, Id = qPDT.First().Id });

            //}


            //#endregion

            return PartialView();
        }

        [HttpGet]
        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult AutoCompleteSearch(string term, ValentearSearchBy searchBy = ValentearSearchBy.Melicode)
        {
            IList<string> data = new List<string>();

            switch (searchBy)
            {
                case ValentearSearchBy.Melicode:
                    data = _valentearService.SearchByNationalId(term);
                    break;
                case ValentearSearchBy.FirstName:
                    data = _valentearService.SearchByFirstName(term);
                    break;
                case ValentearSearchBy.LastName:
                    data = _valentearService.SearchByLastName(term);
                    break;
                case ValentearSearchBy.FatherName:
                    data = _valentearService.SearchByFatherName(term);
                    break;
                case ValentearSearchBy.BirthCertificateNo:
                    data = _valentearService.SearchByBirthCertificateNo(term);
                    break;
            }

            return Json(data.Select(x => new { label = x }).ToList()
                , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult BlockValentear(string vid, string term1 = "", int page1 = 0, int count1 = 10, ValentearSearchBy searchBy1 = ValentearSearchBy.Melicode)
        {

            ViewBag.TERM = term1;
            ViewBag.PAGE = page1;
            ViewBag.COUNT = count1;
            ViewBag.SEARCHBY = searchBy1;

            var qlst = context.Valentears.Where(x => x.TrackingCode.Equals(vid)).ToList();

            if (qlst.Count() > 0)
            {
                var q = qlst.First();
                if ((q.RecordBlocked == null) || (q.RecordBlocked == 0))
                    q.RecordBlocked = 1;
                else
                    q.RecordBlocked = 0;
                context.Valentears.Attach(q);
                context.Entry(q).Property(x => x.RecordBlocked).IsModified = true;
                context.SaveChanges();
                return RedirectToAction(MVC.Admin.Valentear.ActionNames.DataTable, MVC.Admin.Valentear.Name, new { term = term1, page = page1, count = count1, searchBy = searchBy1 });
            }

            return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "مشکلی به وجود آمده", Mode = AlertMode.Error });
        }

      [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult StatusAcceptValentear(int ValentearId,int AcceptStatus, string Message)
        {
            var V = context.Valentears.Find(ValentearId);
            if (V != null)
            {
                V.Accept = AcceptStatus;
                V.Description = Message;
                context.Valentears.Attach(V);
                context.Entry(V).Property(x => x.ChairCode).IsModified = false;
                context.Entry(V).Property(x => x.Pic_Tarth).IsModified = false;
                context.Entry(V).Property(x => x.PictureValenteer).IsModified = false;
                context.Entry(V).Property(x => x.FishVariszi).IsModified = false;
                context.Entry(V).Property(x => x.Accept).IsModified = true;
                context.Entry(V).Property(x => x.Description).IsModified = true;
                context.SaveChanges();

            }


            return RedirectToAction(MVC.Admin.Valentear.ActionNames.GetDataTableByAzmoon, MVC.Admin.Valentear.Name, new { AzmoonId = V.Azmoon.AzmoonId, term = V.NationalID.ToString(), searchBy = ValentearSearchBy.Melicode });
        }

        [HttpPost]
        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult BlockValentear(int AzmoonId, string vid, string term1 = "", int page1 = 0, int count1 = 10, ValentearSearchBy searchBy1 = ValentearSearchBy.Melicode)
        {

            ViewBag.TERM = term1;
            ViewBag.PAGE = page1;
            ViewBag.COUNT = count1;
            ViewBag.SEARCHBY = searchBy1;

            var qlst = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.TrackingCode.Equals(vid)).ToList();

            if (qlst.Count() > 0)
            {
                var q = qlst.First();
                if ((q.RecordBlocked == null) || (q.RecordBlocked == 0))
                    q.RecordBlocked = 1;
                else
                    q.RecordBlocked = 0;
                context.Valentears.Attach(q);
                context.Entry(q).Property(x => x.RecordBlocked).IsModified = true;
                context.SaveChanges();
                return RedirectToAction(MVC.Admin.Valentear.ActionNames.GetDataTableByAzmoon, MVC.Admin.Valentear.Name, new { AzmoonId = AzmoonId, term = term1, page = page1, count = count1, searchBy = searchBy1 });
            }

            return PartialView(MVC.Admin.Shared.Views._Alert, new Alert { Message = "مشکلی به وجود آمده", Mode = AlertMode.Error });
        }

        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult PrintData()
        {
            if (Session["KRBKEYVID"] == null)
            {
                return PartialView(MVC.Admin.Valentear.Views._errorPrintData);
            }

            if (Session["KRBKEYVID"] != null)
            {
                var VID = (int)Session["KRBKEYVID"];
                Jazb.Model.ValentearModel.RegisterModel RM = new Model.ValentearModel.RegisterModel();
                Jazb.Model.ValentearModel.ValentearModel VM = new Model.ValentearModel.ValentearModel();
                Valentear V = context.Valentears.Find(VID);
               

                var qADT = (from p in context.AzmoonDevotionValentears
                            where p.Valentear.Id.Equals(V.Id)
                            select new Jazb.Model.ValentearModel.DevotionTypeModel
                            {
                                DevotionTypeTitle = p.DevotionTitle,
                                Grade = p.DevotionCode,
                                Id = p.Id
                            }).ToList();


                var qAQT = (from p in context.AzmoonQoutasValentears
                            where p.Valentear.Id.Equals(V.Id)
                            select new Jazb.Model.ValentearModel.QoutaTypeModel
                            {
                                QoutaTypeTitle = p.QoutaTitle,
                                Grade = p.QoutaCode,
                                Id = p.Id
                            }).ToList();



                AutoMapper.Mapper.Map(V, VM);

                VM.GenderTitle = GetGenderTitle(VM.GenderId);
                VM.ReligionTitle = GetReligionTitle(VM.ReligionId);
                VM.FaithTitle = GetFaithseTitle(VM.FaithId);
                VM.MarriageStatusTitle = GetMarriageStatuTitle(VM.MarriageStatusId);
                VM.DegreeTitle = GetDegreeTitle(VM.DegreeId,V.Azmoon.AzmoonId);
                VM.EducatedSkillTitle = GetEducatedSkillTitle(VM.EducatedSkillId);
                VM.JobTitle = GetJobTitle(VM.JobId);
                VM.PlaceTitle = GetPlaceTitle(VM.PlaceId);
                VM.PlanStatusTitle = GetPlanStatuTitle(VM.PlanStatusId);
                VM.ConscriptStatusTitle = GetConscriptStatuseTitle(VM.ConscriptStatusId);
                VM.CooperationHistoryTitle = GetCooperationHistorieTitle(VM.CooperationHistoryId);

                VM.AzmoonId = V.Azmoon.AzmoonId;
               
                RM.Valentear = VM;
                RM.SelectedDevotionTypes = qADT;
                RM.SelectedQoutaTypes = qAQT;
                Session["KRBKEYVID"] = null;

                ViewBag.PossibleSignTextValenter = V.Azmoon.SignTextValentear;
                ViewBag.PossiblePicture = V.Azmoon.PicturAzmoon;

                return PartialView(MVC.Admin.Valentear.Views._PrintData, RM);

            }

            return View();
        }

        [Authorize(Roles = "admin,moderator,editor")]
        public virtual ActionResult PrintDataByMelicode(int AzmoonId, string natinalId)
        {
            System.Diagnostics.Debug.WriteLine(natinalId);
            System.Diagnostics.Debug.WriteLine(AzmoonId);
            var qV = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.NationalID.Equals(natinalId)).ToList();
            System.Diagnostics.Debug.WriteLine(qV.Count().ToString());
            if (qV.Count() > 0)
            {
                Session["KRBKEYVID"] = qV.First().Id;

                return RedirectToAction(MVC.Admin.Valentear.ActionNames.PrintData, MVC.Admin.Valentear.Name);
            }
            Session["KRBKEYVID"] = null;
            return RedirectToAction(MVC.Admin.Valentear.ActionNames.PrintData, MVC.Admin.Valentear.Name);
        }

        [Authorize]
        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportValentear(int AzmoonId)
        {
            var q = (from p in context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId  && x.RecordBlocked==0 ).AsQueryable()
                     group p by new { p.JobId }
                         into g
                         select new
                         {
                             qname = g.Key,
                             mard = g.Count(x => x.GenderId == 2),
                             zan = g.Count(x => x.GenderId == 1),
                             Total = g.Count()
                         }).Distinct().ToList();

            var results = (from p in q.AsQueryable()
                           join j in context.AzmoonJobs.Where(ap => ap.Azmoon.AzmoonId == AzmoonId).ToList() on p.qname.JobId equals j.AzmoonJobCode
                           select new Jazb.Model.AdminModel.ValentearGroups
                           {
                               JobName = j.AzmoonJobTitle,
                               CountMan = p.mard,
                               CountWoman = p.zan,
                               TotlaCount = p.Total
                           }).Distinct().ToList();

            return PartialView(MVC.Admin.Valentear.Views._ReportValentear,results);
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportSepratJobCity(int AzmoonId)
        {
            var q = (from p in context.Valentears.Where(ap => ap.Azmoon.AzmoonId == AzmoonId && ap.RecordBlocked == 0).ToList()
                     group p by new { p.JobId, p.PlaceId }
                         into g
                         select new
                         {
                             qname = g.Key.JobId,
                             cityid = g.Key.PlaceId,
                             mard = g.Count(x => x.GenderId == 2),
                             zan = g.Count(x => x.GenderId == 1),
                             Total = g.Count()
                         }).Distinct().ToList();

            var qJobs = context.AzmoonJobs.Where(ap => ap.Azmoon.AzmoonId == AzmoonId).ToList();
            var qCity = context.AzmoonPlaces.Where(ap => ap.Azmoon.AzmoonId == AzmoonId).Select(x => new { x.PlaceCode, x.PlaceTitle }).Distinct().ToList();

            var results = (from p in q.ToList()
                           join j in qJobs on p.qname equals j.AzmoonJobCode

                           join c in qCity on p.cityid equals c.PlaceCode
                           select new Jazb.Model.AdminModel.ValentearJobCity
                           {
                               JobName = j.AzmoonJobTitle,
                               CityName = c.PlaceTitle,
                               CountMan = p.mard,
                               CountWoman = p.zan,
                               TotlaCount = p.Total
                           }).Distinct().ToList();

            return PartialView(MVC.Admin.Valentear.Views._ReportSepratJobCity ,results.OrderBy(x => x.CityName));
        }
        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportSepratJobCitySortByAghi(int AzmoonId)
        {
            var RVJC = new Jazb.Model.AdminModel.ReportValentearJobCity();
            List<Jazb.Model.AdminModel.ReportValentearJobCity> lstRVJC = new List<Model.AdminModel.ReportValentearJobCity>();

            var qAgahi = _JobCityService.GetJobCityByAgahi(AzmoonId); 
           
            foreach (var t in qAgahi)
            {
                var qCounts = _valentearService.CountGenderByJobPlace(AzmoonId, int.Parse(t.JobCode.ToString()), t.PlaceCode, 0, 0);
                RVJC = new Jazb.Model.AdminModel.ReportValentearJobCity
                {
                    JobCode = t.JobCode,
                    JobName = t.JobName,
                    JobPriority = t.JobPeriority,
                    PlaceCode = t.PlaceCode,
                    PlaceTitle = t.PlaceTitle,
                    PlacePriority = t.PlacePeriority,
                    CountMan = qCounts.CountMan,
                    CountWoman = qCounts.CountWoman,
                    TotlaCount = qCounts.TotlaCount
                };

                lstRVJC.Add(RVJC);
            }
            var qResult = lstRVJC.OrderBy(x => x.JobPriority).ThenBy(p => p.PlacePriority);

            return PartialView(MVC.Admin.Valentear.Views._RSJCSA, qResult.OrderBy(x => x.JobPriority).ThenBy(p => p.PlacePriority));
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportValentearByAgahi(int AzmoonId)
        {
            var qJobs = context.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            var qPlace = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();

            List<Jazb.Model.AdminModel.ReportValentearByAgahiModel> lstRVJC = new List<Jazb.Model.AdminModel.ReportValentearByAgahiModel>();
            Jazb.Model.AdminModel.ReportValentearByAgahiModel RVJC;
            var qAgahi = _JobCityService.GetJobCityByAgahi(AzmoonId); 

            foreach (var t in qAgahi)
            {
                var qCounts = _valentearService.CountGenderByJobPlace(AzmoonId, int.Parse(t.JobCode.ToString()), int.Parse(t.PlaceCode.ToString()), 0, 0);
                RVJC = new Jazb.Model.AdminModel.ReportValentearByAgahiModel
                {
                    JobCode = t.JobCode,
                    JobName = t.JobName,
                    JobPriority = t.JobPeriority,
                    PlaceCode = t.PlaceCode,
                    PlaceTitle = t.PlaceTitle,
                    PlacePriority = t.PlacePeriority,
                    CountMan = qCounts.CountMan,
                    CountWoman = qCounts.CountWoman,
                    TotlaCount = qCounts.TotlaCount,
                    Valentears= GetListValentears(AzmoonId, t.JobCode, t.PlaceCode)
                };

                lstRVJC.Add(RVJC);
            }

          var   qResult = lstRVJC.OrderBy(x => x.JobPriority).ThenBy(p => p.PlacePriority);

            return PartialView(MVC.Admin.Valentear.Views._ReportValentearByAgahi, qResult.OrderBy(x => x.JobPriority).ThenBy(p => p.PlacePriority));
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportValentearFillterGendederByAgahi(int AzmoonId)
        {
            var qGenderGroups = GetValentearGenderGroup(AzmoonId);
            return PartialView(MVC.Admin.Valentear.Views._ReportValentearFillterGendederByAgahi, qGenderGroups);
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult ReportValentearChairCard(int AzmoonId)
        {
            var qGenderGroups = GetValentearGenderGroup(AzmoonId);
            return PartialView(MVC.Admin.Valentear.Views._ReportValentearChairCard, qGenderGroups);

        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult GetFishPicture(int AzmoonId,string natinalId)
        {
            var vlist = _valentearService.GetValentears(AzmoonId);
            var flists = vlist.Where(x => x.NationalID == natinalId).ToList();
            if (flists.Count() > 0)
            {
                return PartialView(flists.First());
            }
            return null;
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult GetTargPicture(int AzmoonId,string natinalId)
        
        {
            var vlist = _valentearService.GetValentears(AzmoonId);
            var flists = vlist.Where(x => x.NationalID == natinalId).ToList();
            if (flists.Count() > 0)
            {
                return PartialView(flists.First());
            }
            return null;
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult GetPicPicture(int AzmoonId,string natinalId)
        {
            var vlist = _valentearService.GetValentears(AzmoonId);
            var flists = vlist.Where(x => x.NationalID == natinalId).ToList();
            if (flists.Count() > 0)
            {
                return PartialView(flists.First());
            }
            return null;
        }


    

      
      
        private List<ValentearModel> GetListValentears(int AzmoonId, long JobId, long PlaceId)
        {
            var q = _valentearService.GetValentears(AzmoonId, int.Parse(JobId.ToString()), int.Parse(PlaceId.ToString()), 0, 0);
            List<ValentearModel> VM = new List<ValentearModel>();
            AutoMapper.Mapper.Map(q, VM);
            foreach (var t in VM)
            {
                t.JobTitle = GetJobTitle(t.JobId);
                t.PlaceTitle = GetPlaceTitle(t.PlaceId);
            }

            return VM;
        }

        private List<ValentearModel> GetListValentearsFilterGender(int AzmoonId,int GenderId, long JobId, long PlaceId)
        {
            var q = context.Valentears.Where(ap => ap.Azmoon.AzmoonId == AzmoonId && ap.JobId == JobId && ap.PlaceId == PlaceId && ap.GenderId == GenderId && ap.RecordBlocked == 0).ToList();
            List<ValentearModel> VM = new List<ValentearModel>();
            AutoMapper.Mapper.Map(q, VM);
            foreach (var t in VM)
            {
                t.JobTitle = GetJobTitle(t.JobId);
                t.PlaceTitle = GetPlaceTitle(t.PlaceId);
                t.ChairCodeint = t.ChairCode == null ? 0 : int.Parse(t.ChairCode);
            }

            return VM.OrderBy(x => x.ChairCodeint).ToList();
        }

        private List<Model.AdminModel.ReportValentearByGender> GetValentearGenderGroup(int AzmoonId)
        {
            var qJobs = context.AzmoonJobs.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            var qPlace = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();


            var qGenderGroups = new List<Model.AdminModel.ReportValentearByGender>
            {
                new Model.AdminModel.ReportValentearByGender{ GenderId=1, GenderTitle="زن"},
                new Model.AdminModel.ReportValentearByGender{ GenderId=2, GenderTitle="مرد"}
            };


            List<Jazb.Model.AdminModel.ReportValentearByAgahiModel> lstRVJC;
            Jazb.Model.AdminModel.ReportValentearByAgahiModel RVJC;

            var qAgahi = _JobCityService.GetJobCityByAgahi(AzmoonId);

            foreach (var g in qGenderGroups)
            {
                lstRVJC = new List<Jazb.Model.AdminModel.ReportValentearByAgahiModel>();
                foreach (var t in qAgahi)
                {
                    var qCounts = _valentearService.CountGenderByJobPlace(AzmoonId, int.Parse(t.JobCode.ToString()), int.Parse(t.PlaceCode.ToString()), 0, 0);
                    RVJC = new Jazb.Model.AdminModel.ReportValentearByAgahiModel
                    {
                        JobCode = t.JobCode,
                        JobName = t.JobName,
                        JobPriority = t.JobPeriority,
                        PlaceCode = t.PlaceCode,
                        PlaceTitle = t.PlaceTitle,
                        PlacePriority = t.PlacePeriority,
                        CountMan = qCounts.CountMan,
                        CountWoman = qCounts.CountWoman,
                        TotlaCount = qCounts.TotlaCount,
                        Valentears = GetListValentearsFilterGender(AzmoonId, g.GenderId, t.JobCode, t.PlaceCode).OrderBy(x => x.ChairCodeint).ToList()
                    };

                    lstRVJC.Add(RVJC);
                }

                g.ValentearsGroups = lstRVJC.OrderBy(x => x.JobPriority).ThenBy(p => p.PlacePriority).ThenBy(x => x.Valentears.OrderBy(y => y.ChairCodeint)).ToList();
            }
            return qGenderGroups;

        }
        #region "Private Methds"

        protected string ConvertStringArrayToStringJoin(string seperator, string[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(seperator, array);
            return result;
        }

        [Authorize(Roles = "admin,moderator,writer,editor")]
        public virtual ActionResult GetLocationRequest(int AzmoonId, int? Jobid, int? GenderId)
        {
            List<AzmoonPlace> q;
            if (GenderId == 1)
                q = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.JobId == Jobid && x.WillWoman == true).ToList();
            else
                q = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.JobId == Jobid && x.WillMan == true).ToList();


            string strOption = "<option value='{0}'>{1}</option>";
            string strOptions = "";
            strOptions += string.Format(strOption, 0, "--- انتخاب ---");
            foreach (var item in q)
            {
                strOptions += string.Format(strOption, item.PlaceCode, item.PlaceTitle);
            }

            return Content(strOptions);
        }

        private string GetJobTitle(int Id)
        {
            var q = context.AzmoonJobs.Where(x => x.AzmoonJobCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().AzmoonJobTitle;


            return "مشخص نشده";
        }
        private string GetPlaceTitle(long Id)
        {

            var q = context.AzmoonPlaces.Where(x => x.PlaceCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().PlaceTitle;


            return "مشخص نشده";
        }
        protected byte[] getBytefile(HttpPostedFileBase file)
        {
            string fileName;
            string fileContentType;
            byte[] fileBytes;

            fileName = file.FileName;
            fileContentType = file.ContentType;
            fileBytes = new byte[file.ContentLength];
            file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

            return fileBytes;
        }
        private SelectList GetGender(int Id)
        {
            var qGender = (from p in context.Genders
                           select new SelectListItem
                           {
                               Text = p.GenderTitle,
                               Value = p.GenderCode.ToString(),

                           }).ToList();

            return new SelectList(qGender, "Value", "Text", Id);

        }
        private SelectList GetConscriptStatuses(int Id)
        {
            var qConscriptStatus = (from p in context.ConscriptStatuses
                                    select new SelectListItem
                                    {
                                        Text = p.ConscriptStatusTitle,
                                        Value = p.ConscriptStatusCode.ToString()
                                    }).ToList();

            return new SelectList(qConscriptStatus, "Value", "Text", Id);
        }
        private SelectList GetReligions(int Id)
        {
            var qReligion = (from p in context.Religions
                             select new SelectListItem
                             {
                                 Text = p.ReligionTitle,
                                 Value = p.ReligionCode.ToString()
                             }).ToList();

            return new SelectList(qReligion, "Value", "Text", Id);
        }
        private SelectList GetFaithses(int Id)
        {
            var qFaithse = (from p in context.Faiths
                            select new SelectListItem
                            {
                                Text = p.FaithTitle,
                                Value = p.FaithCode.ToString()
                            }).ToList();

            return new SelectList(qFaithse, "Value", "Text", Id);


        }
        private SelectList GetEducatedSkills(int Id,int AzmoonId)
        {
            var qEducatedSkill = (from p in context.AzmooneducatedSkills.Include(x=>x.Azmoon)
                                                                        .Where(x=>x.Azmoon.AzmoonId==AzmoonId)
                                  select new SelectListItem
                                  {
                                      Text = p.AzmooneducatedSkillTitle,
                                      Value = p.AzmooneducatedSkillCode.ToString()
                                  }).ToList();

            return new SelectList(qEducatedSkill, "Value", "Text", Id);
        }
        private SelectList GetMarriageStatus(int Id)
        {
            var qMarriage = (from p in context.marriageStatuses
                             select new SelectListItem
                             {
                                 Text = p.marriageStatusTitle,
                                 Value = p.marriageStatusCode.ToString()
                             }).ToList();
            return new SelectList(qMarriage, "Value", "Text", Id);
        }
        private SelectList GetDegrees(int Id,int AzmoonId)
        {
            var qDegree = (from p in context.AzmoonDegrees.Include(x => x.Azmoon)
                                                           .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                           orderby p.Periority ascending
                           select new SelectListItem
                           {
                               Text = p.Title,
                               Value = p.Code.ToString()
                           }).ToList();
            return new SelectList(qDegree, "Value", "Text", Id);
        }
        private SelectList GetJobs(int Id, int AzmoonId)
        {
            var qJob = (from p in context.AzmoonJobs.Include(x => x.Azmoon)
                                                     .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                        select new SelectListItem
                        {
                            Text = p.AzmoonJobTitle,
                            Value = p.AzmoonJobCode.ToString()
                        }).ToList();
            return new SelectList(qJob, "Value", "Text", Id);
        }
        private SelectList GetPlaces(long Id)
        {

            //var qPlace = (from p in context.AzmoonPlaces
            //              select new SelectListItem
            //              {
            //                  Text = p.PlaceTitle,
            //                  Value = p.PlaceCode.ToString()
            //              }).ToList();

            var qPlace = new List<SelectListItem>();

            return new SelectList(qPlace, "Value", "Text", Id);
        }
        private SelectList GetPlanStatus(int Id)
        {

            var qPlanStatus = (from p in context.PlaneStatuses
                               select new SelectListItem
                               {
                                   Text = p.PlaneStatusTitle,
                                   Value = p.PlaneStatusCode.ToString()
                               }).ToList();
            return new SelectList(qPlanStatus, "Value", "Text", Id);
        }
        private SelectList GetCooperationHistories(int Id)
        {

            var qcooperationHistories = (from p in context.cooperationHistories
                                         select new SelectListItem
                                         {
                                             Text = p.cooperationHistoryTitle,
                                             Value = p.cooperationHistoryCode.ToString()
                                         }).ToList();
            return new SelectList(qcooperationHistories, "Value", "Text", Id);
        }


        private string GetGenderTitle(int Id)
        {
            var q = context.Genders.Where(x => x.GenderCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().GenderTitle;


            return "مشخص نشده";


        }
        private string GetConscriptStatuseTitle(int Id)
        {
            var q = context.ConscriptStatuses.Where(x => x.ConscriptStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().ConscriptStatusTitle;


            return "مشخص نشده";
        }
        private string GetReligionTitle(int Id)
        {
            var q = context.Religions.Where(x => x.ReligionCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().ReligionTitle;


            return "مشخص نشده";
        }
        private string GetFaithseTitle(int Id)
        {
            var q = context.Faiths.Where(x => x.FaithCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().FaithTitle;


            return "مشخص نشده";


        }
        private string GetEducatedSkillTitle(int Id)
        {
            var q = context.AzmooneducatedSkills.Where(x => x.AzmooneducatedSkillCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().AzmooneducatedSkillTitle;


            return "مشخص نشده";
        }
        private string GetMarriageStatuTitle(int Id)
        {
            var q = context.marriageStatuses.Where(x => x.marriageStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().marriageStatusTitle;


            return "مشخص نشده";
        }
        private string GetDegreeTitle(int Id ,int azmoonid)
        {
            var q = context.AzmoonDegrees.Where(x => x.Code.Equals(Id) && x.Azmoon.AzmoonId.Equals(azmoonid)).ToList();
            if (q.Count() > 0)
                return q.First().Title;


            return "مشخص نشده";
        }
        private string GetPlanStatuTitle(int Id)
        {
            var q = context.PlaneStatuses.Where(x => x.PlaneStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().PlaneStatusTitle;


            return "مشخص نشده";
        }
        private string GetCooperationHistorieTitle(int Id)
        {
            var q = context.cooperationHistories.Where(x => x.cooperationHistoryCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().cooperationHistoryTitle;


            return "مشخص نشده";
        }

        #endregion

    }
}
