using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model;
using System.Web.UI;
using System.Globalization;
using Jazb.Model.ValentearModel;
using Jazb.Model.AdminModel.EditValentear;
using Jazb.Utilities.DateAndTime;
using Jazb.Model.Payment;

namespace Jazb.Web.Controllers
{


    public partial class ValentearController : Controller
    {

        private readonly IUnitOfWork _uow;

        public ValentearController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        //
        // GET: /Valentear/
        protected JazbDbContext context = new JazbDbContext();
        [HttpGet]

        private void UpdatePayment(long saleReferenceId, int valentearId)
        {
            var ps = context.Payments.Where(x => x.SaleReferenceId == saleReferenceId && x.StatusPayment == "OK" && x.IsUses == false).ToList();
            if (ps.Count() > 0)
            {
                // var payment = db.Payments.Find(paymentId);
                var payment = ps.First();

                if (payment != null)
                {
                    payment.IsUses = true;

                    context.Entry(payment).State = EntityState.Modified;
                    context.SaveChanges();

                    Valentear V = context.Valentears.Find(valentearId);
                    V.AmountValue = payment.PaymentIdstring.ToString();
                    V.IsPaymented = true;
                    context.Valentears.Attach(V);
                    context.Entry(V).Property(x => x.AmountValue).IsModified = true;
                    context.Entry(V).Property(x => x.IsPaymented).IsModified = true;
                    context.SaveChanges();
                }
                else
                {
                    // اطلاعاتی از دیتابیس پیدا نشد
                }

            }

        }
        private bool checkRefIsUse(string saleReferenceId, int AzmoonId)
        {
            long sal = long.Parse(saleReferenceId);
            var gpay = context.Payments.Where(x => x.SaleReferenceId == sal
                                                && x.StatusPayment == "OK" && x.AzmoonId == AzmoonId
                                                && x.IsUses == true).ToList();
            bool blnflag = false;
            if (gpay.Count() > 0)
                blnflag = true;

            return blnflag;


        }

        [HttpGet]
        public virtual ActionResult BeforeRegister(int AID)
        {
            BeforeRegisterModel BRM = new BeforeRegisterModel
            {
                AzmoonId = AID,
                PaymentId = "",
                SaleRefrenceId = 0
            };

            return View(BRM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult BeforeRegister(BeforeRegisterModel model)
        {

            var Pays = context.Payments.Where(x => x.AzmoonId == model.AzmoonId && x.SaleReferenceId == model.SaleRefrenceId
                                              && x.PaymentIdstring == model.PaymentId && x.StatusPayment == "OK" && x.IsUses == false).ToList();
            if (Pays.Count() > 0)
            {
                var Pay = Pays.First();
                Session["SRefKey"] = Pay.SaleReferenceId.ToString();
                return RedirectToAction(MVC.Valentear.ActionNames.Register, new { AID = Pay.AzmoonId });
            }
            return View(model);
        }





        public virtual ActionResult Register(int AID)
        {
            ViewBag.PossibleAzmoon = AID;
            var qa = context.Azmoons.Find(AID);

            //MsgIsManualPayment
            string MsgIsOnlinePayment = "";
            if (qa.AzmoonPaymentType == PaymentType.OnlinePayment)
                MsgIsOnlinePayment = "Yes";
            else
                MsgIsOnlinePayment = "No";

            ViewBag.MsgIsOnlinePayment = MsgIsOnlinePayment;
            var vsesSRefKey = "";
            if (MsgIsOnlinePayment == "Yes")
            {
                if (Session["SRefKey"] == null)
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = AID });
            }
            if (Session["SRefKey"] != null)
                vsesSRefKey = Session["SRefKey"].ToString();
            Session["SRefKey"] = null;
            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowRegisters != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            //if (Request.IsAuthenticated)
            //    if (User.Identity.Name != "ImportsUser")
            //        if (qa.EndRegister || !qa.Active)
            //            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

            ViewBag.PossibleGenders = GetGender();
            ViewBag.PossibleReligions = GetReligions();
            ViewBag.PossibleFaiths = GetFaithses();
            ViewBag.PossibleConscriptStatus = GetConscriptStatuses();
            ViewBag.PossibleMarriageStatus = GetMarriageStatus();
            ViewBag.PossibleDegrees = GetAzmoonDegrees(AID);
            ViewBag.PossibleEducatedSkills = GetAzmoonEducatedSkills(AID);
            ViewBag.PossibleJobs = GetAzmoonJobs(AID);
            ViewBag.PossiblePlaces = GetPlaces();
            ViewBag.PossiblePlanStatus = GetPlanStatus();
            ViewBag.PossibleCooperationHistories = GetCooperationHistories();


            Jazb.Model.ValentearModel.RegisterModel RM = new Model.ValentearModel.RegisterModel();

            foreach (var t in context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == AID).ToList())
                lstADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = t.DevotionTypeTitle, Grade = t.Grade, Id = t.Id });


            foreach (var t in context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == AID).ToList())
                lstQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = t.QoutaTypeTitle, Grade = t.Grade, Id = t.Id });

            RM.DevotionTypes = lstADT.OrderBy(x => x.Grade).ToList();
            RM.QoutaTypes = lstQTM;
            RM.Valentear = new Model.ValentearModel.ValentearModel { AzmoonId = AID };

            Session["SRefKey"] = vsesSRefKey;
            if (!string.IsNullOrEmpty(vsesSRefKey))
                if (checkRefIsUse(vsesSRefKey, AID))
                {
                    Session["SRefKey"] = null;
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = AID });
                }
            return View(RM);
        }
        [HttpPost]
        public virtual ActionResult Register(Jazb.Model.ValentearModel.RegisterModel RM)
        {


            ViewBag.PossibleAzmoon = RM.Valentear.AzmoonId;
            var qa = context.Azmoons.Find(RM.Valentear.AzmoonId);

            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowRegisters != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);





            string MsgIsOnlinePayment = "";
            if (qa.AzmoonPaymentType == PaymentType.OnlinePayment)
                MsgIsOnlinePayment = "Yes";
            else
                MsgIsOnlinePayment = "No";

            ViewBag.MsgIsOnlinePayment = MsgIsOnlinePayment;

            if (MsgIsOnlinePayment == "Yes")
            {
                if (Session["SRefKey"] == null)
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
            }

            var vsesSRefKey = "";
            if (Session["SRefKey"] != null)
                vsesSRefKey = Session["SRefKey"].ToString();
            Session["SRefKey"] = null;

            if (!string.IsNullOrEmpty(vsesSRefKey))
                if (checkRefIsUse(vsesSRefKey, RM.Valentear.AzmoonId))
                {
                    Session["SRefKey"] = null;
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
                }


            //if (Request.IsAuthenticated)
            //    if (User.Identity.Name != "ImportsUser")
            //        if (qa.EndRegister || !qa.Active)
            //            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);


            #region "Initialize Data For Return View"
            IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

            IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstSelectedADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
            IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstSelectedQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();

            ViewBag.PossibleGenders = GetGender();
            ViewBag.PossibleReligions = GetReligions();
            ViewBag.PossibleFaiths = GetFaithses();
            ViewBag.PossibleConscriptStatus = GetConscriptStatuses();
            ViewBag.PossibleMarriageStatus = GetMarriageStatus();
            ViewBag.PossibleDegrees = GetAzmoonDegrees(RM.Valentear.AzmoonId);
            ViewBag.PossibleEducatedSkills = GetAzmoonEducatedSkills(RM.Valentear.AzmoonId);
            ViewBag.PossibleJobs = GetAzmoonJobs(RM.Valentear.AzmoonId);
            ViewBag.PossiblePlaces = GetPlaces();
            ViewBag.PossiblePlanStatus = GetPlanStatus();
            ViewBag.PossibleCooperationHistories = GetCooperationHistories();


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
                var qPDT = context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Id.Equals(idD)).ToList();
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
                var qPDT = context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == RM.Valentear.AzmoonId && x.Id.Equals(idD)).ToList();
                if (qPDT.Count() > 0)
                    lstSelectedQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = qPDT.First().QoutaTypeTitle, Grade = qPDT.First().Grade, Id = qPDT.First().Id });

            }


            RM.DevotionTypes = lstADT.OrderBy(x => x.Grade).ToList();
            RM.QoutaTypes = lstQTM;
            RM.SelectedDevotionTypes = lstSelectedADT;
            RM.SelectedQoutaTypes = lstSelectedQTM;
            #endregion


            if (IsNationalIdRegister(RM.Valentear.AzmoonId, RM.Valentear.NationalID))
            {

                ModelState.AddModelError("", "شما قبلا در این آزمون ثبت نتم کرده اید");

                Session["SRefKey"] = vsesSRefKey;
                return View(RM);
            }


            RM.Valentear.GenderTitle = GetGender(RM.Valentear.GenderId);
            RM.Valentear.ReligionTitle = GetReligions(RM.Valentear.ReligionId);
            RM.Valentear.FaithTitle = GetFaithses(RM.Valentear.FaithId);
            RM.Valentear.MarriageStatusTitle = GetMarriageStatus(RM.Valentear.MarriageStatusId);
            RM.Valentear.DegreeTitle = GetDegreeTitle(RM.Valentear.DegreeId, RM.Valentear.AzmoonId);
            RM.Valentear.EducatedSkillTitle = GetEducatedSkillTitle(RM.Valentear.EducatedSkillId, RM.Valentear.AzmoonId);
            RM.Valentear.JobTitle = GetJobTitle(RM.Valentear.JobId, RM.Valentear.AzmoonId);
            RM.Valentear.PlaceTitle = GetPlaceTitle(RM.Valentear.PlaceId, RM.Valentear.JobId, RM.Valentear.AzmoonId);
            RM.Valentear.PlanStatusTitle = GetPlanStatus(RM.Valentear.PlanStatusId);
            RM.Valentear.ConscriptStatusTitle = GetConscriptStatuses(RM.Valentear.ConscriptStatusId);
            RM.Valentear.CooperationHistoryTitle = GetCooperationHistories(RM.Valentear.CooperationHistoryId);

            Jazb.Servicelayer.EFServices.GenerateUniqCode GUC = new Servicelayer.EFServices.GenerateUniqCode();
            string UnicText = GUC.CheckUniqCode(8);

            RM.Valentear.TrackingCode = UnicText;

            var personel_picture = Request.Files["personel_picture"];
            var fish_variz = Request.Files["fish_variz"];
            var Pic_Tarh = Request.Files["Pic_Tarh"];
            var Pic_Conscript = Request.Files["Pic_Conscript"];
            var Pic5 = Request.Files["Pic5"];
            var Pic6 = Request.Files["Pic6"];
            var Pic7 = Request.Files["Pic7"];
            var Pic8 = Request.Files["Pic8"];

            // var fish_variz2 = Request.Files["fish_variz2"];

            if ((personel_picture == null) || (personel_picture.ContentLength == 0) || (string.IsNullOrEmpty(personel_picture.FileName)))
            {
                ModelState.AddModelError("personel_picture", "لطفا تصویر داوطلب را وارد کنید");

                Session["SRefKey"] = vsesSRefKey;
                return View(RM);
            }
            else
            {
                RM.Valentear.PictureValenteer = getBytefile(personel_picture);
            }


            if ((fish_variz == null) || (fish_variz.ContentLength == 0) || (string.IsNullOrEmpty(fish_variz.FileName)))
            {
                //ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
                //return View(RM);
                var i = 0;
            }
            else
            {
                if (fish_variz.ContentLength > 0)
                    RM.Valentear.FishVariszi = getBytefile(fish_variz);
            }

            if ((Pic_Tarh == null) || (Pic_Tarh.ContentLength == 0) || (string.IsNullOrEmpty(Pic_Tarh.FileName)))
            {
                ModelState.AddModelError("Pic_Tarh", "لطفا تصویر طرح / مدرک تحصیلی را وارد کنید");

                Session["SRefKey"] = vsesSRefKey;
                return View(RM);
            }
            else
            {
                RM.Valentear.Pic_Tarth = getBytefile(Pic_Tarh);
            }

            if ((Pic_Conscript == null) || (Pic_Conscript.ContentLength == 0) || (string.IsNullOrEmpty(Pic_Conscript.FileName)))
            {
                ModelState.AddModelError("Pic_Conscript", "لطفا تصویر کارت پایان خدمت را وارد کنید");

                Session["SRefKey"] = vsesSRefKey;
                return View(RM);
            }
            else
            {
                RM.Valentear.Pic_Conscript = getBytefile(Pic_Conscript);
            }


            if ((Pic5 == null) || (Pic5.ContentLength == 0) || (string.IsNullOrEmpty(Pic5.FileName)))
            {
                //ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
                //return View(RM);
                var i = 0;
            }
            else
            {
                if (Pic5.ContentLength > 0)
                    RM.Valentear.Pic5 = getBytefile(Pic5);
            }

            if ((Pic6 == null) || (Pic6.ContentLength == 0) || (string.IsNullOrEmpty(Pic6.FileName)))
            {
                //ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
                //return View(RM);
                var i = 0;
            }
            else
            {
                if (Pic6.ContentLength > 0)
                    RM.Valentear.Pic6 = getBytefile(Pic6);
            }


            if ((Pic7 == null) || (Pic7.ContentLength == 0) || (string.IsNullOrEmpty(Pic7.FileName)))
            {
                //ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
                //return View(RM);
                var i = 0;
            }
            else
            {
                if (Pic7.ContentLength > 0)
                    RM.Valentear.Pic7 = getBytefile(Pic7);
            }

            if ((Pic8 == null) || (Pic8.ContentLength == 0) || (string.IsNullOrEmpty(Pic8.FileName)))
            {
                //ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
                //return View(RM);
                var i = 0;
            }
            else
            {
                if (Pic8.ContentLength > 0)
                    RM.Valentear.Pic8 = getBytefile(Pic8);
            }






            if (!ModelState.IsValid)
            {
                foreach (var t in ModelState)
                {
                    if (t.Value.Errors.Count > 0)
                    {

                        ErrorCode ec = new ErrorCode();
                        ec.FieldName = t.Key;
                        ec.Melicode = RM.Valentear.NationalID;
                        ec.TrackingCode = UnicText;
                        ec.value = (t.Value.Value == null ? "" : t.Value.Value.AttemptedValue);
                        context.ErrorCodes.Add(ec);
                        context.SaveChanges();
                        ModelState.AddModelError(t.Key, "خطا را برطرف کنید");

                    }
                }
            }


            var QA = context.Azmoons.Find(RM.Valentear.AzmoonId);
            ViewBag.PossibleSignTextValenter = QA.SignTextValentear;
            if (ModelState.IsValid)
            {
                Session["KRBRM"] = RM;
                Session["SRefKey"] = vsesSRefKey;
                if (!string.IsNullOrEmpty(vsesSRefKey))
                    if (checkRefIsUse(vsesSRefKey, RM.Valentear.AzmoonId))
                    {
                        Session["SRefKey"] = null;
                        return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
                    }
                return View(MVC.Valentear.Views.ViewDataBeforSave, RM);
            }

            if (!string.IsNullOrEmpty(vsesSRefKey))
                if (checkRefIsUse(vsesSRefKey, RM.Valentear.AzmoonId))
                {
                    Session["SRefKey"] = null;
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
                }

            return View(RM);
        }
        [HttpPost]
        public virtual ActionResult SaveData(Jazb.Model.ValentearModel.RegisterModel RM)
        {
            ViewBag.PossibleAzmoon = RM.Valentear.AzmoonId;
            var qa = context.Azmoons.Find(RM.Valentear.AzmoonId);

            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowRegisters != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);




            string MsgIsOnlinePayment = "";
            if (qa.AzmoonPaymentType == PaymentType.OnlinePayment)
                MsgIsOnlinePayment = "Yes";
            else
                MsgIsOnlinePayment = "No";

            ViewBag.MsgIsOnlinePayment = MsgIsOnlinePayment;

            if (MsgIsOnlinePayment == "Yes")
            {
                if (Session["SRefKey"] == null)
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
            }

            var vsesSRefKey = "";
            if (Session["SRefKey"] != null)
                vsesSRefKey = Session["SRefKey"].ToString();
            Session["SRefKey"] = null;

            if (!string.IsNullOrEmpty(vsesSRefKey))
                if (checkRefIsUse(vsesSRefKey, RM.Valentear.AzmoonId))
                {
                    Session["SRefKey"] = null;
                    return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
                }

            //if (Request.IsAuthenticated)
            //    if (User.Identity.Name != "ImportsUser")
            //        if (qa.EndRegister || !qa.Active)
            //            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (Session["KRBRM"] == null)
                return RedirectToAction(MVC.Valentear.ActionNames.Register, MVC.Valentear.Name);



            if (IsNationalIdRegister(RM.Valentear.AzmoonId, RM.Valentear.NationalID))
            {
                ModelState.AddModelError("", "شما قبلا در این آزمون ثبت نتم کرده اید");
                return RedirectToAction(MVC.Valentear.ActionNames.Register, MVC.Valentear.Name);
            }


            if (Session["KRBRM"] != null)
            {
                RM = (Jazb.Model.ValentearModel.RegisterModel)Session["KRBRM"];

                RM.Valentear.DateRegister = DateTime.Now;
                Valentear V = new Valentear();

                AutoMapper.Mapper.Map(RM.Valentear, V);
                V.MaxQoutaType = int.Parse(RM.SelectedQoutaTypes.Max(x => x.Grade).ToString());
                V.SumQoutaType = int.Parse(RM.SelectedQoutaTypes.Sum(x => x.Grade).ToString());
                V.TextQoutaType = ConvertStringArrayToStringJoin("-", RM.SelectedQoutaTypes.Select(x => x.QoutaTypeTitle).ToArray());

                V.MaxDevotionType = int.Parse(RM.SelectedDevotionTypes.Max(x => x.Grade).ToString());
                V.SumDevotionType = int.Parse(RM.SelectedDevotionTypes.Sum(x => x.Grade).ToString());
                V.TextDevotionType = ConvertStringArrayToStringJoin("-", RM.SelectedDevotionTypes.Select(x => x.DevotionTypeTitle).ToArray());


                if (User.Identity.IsAuthenticated)
                    V.CreateUser = User.Identity.Name;
                V.AmountValue = "";
                V.IsPaymented = false;
                V.Azmoon = qa;
                ViewBag.PossibleSignTextValenter = qa.SignTextValentear;
                ViewBag.PossiblePicture = qa.PicturAzmoon;

                var ListAQV = AutoMapper.Mapper.Map<IEnumerable<Jazb.Model.ValentearModel.QoutaTypeModel>, List<AzmoonQoutasValentear>>(RM.SelectedQoutaTypes);
                var ListADV = AutoMapper.Mapper.Map<IEnumerable<Jazb.Model.ValentearModel.DevotionTypeModel>, List<AzmoonDevotionValentear>>(RM.SelectedDevotionTypes);

                if (!string.IsNullOrEmpty(vsesSRefKey))
                    if (checkRefIsUse(vsesSRefKey, RM.Valentear.AzmoonId))
                    {
                        Session["SRefKey"] = null;
                        return RedirectToAction(MVC.Valentear.ActionNames.BeforeRegister, MVC.Valentear.Name, new { AID = RM.Valentear.AzmoonId });
                    }

                context.Valentears.Add(V);
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


                context.SaveChanges();
                Session["KRBRM"] = null;
                Session["SRefKey"] = null;

                if (!string.IsNullOrEmpty(vsesSRefKey))
                {
                    long sessFlagRefKey = long.Parse(vsesSRefKey);
                    UpdatePayment(sessFlagRefKey, V.Id);
                }

            }

            return RedirectToAction(MVC.Valentear.ActionNames.PrintData, MVC.Valentear.Name);

        }


        public virtual ActionResult EditRegister(int AID)
        {

            ViewBag.PossibleAzmoon = AID;
            var qa = context.Azmoons.Find(AID);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanEditResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);



            string MsgIsManualPayment = "";
            if (qa.AzmoonPaymentType == PaymentType.ManualPayment)
                MsgIsManualPayment = "Yes";
            else
                MsgIsManualPayment = "No";
            ViewBag.IsManualPayment = MsgIsManualPayment;


            EditVlentearPageVeiwMode EVPVM = new EditVlentearPageVeiwMode
            {
                AzmoonId = AID
            };

            return View(EVPVM);
        }

        [HttpPost]
        public virtual ActionResult EditRegister(EditVlentearPageVeiwMode model)
        {
            ViewBag.PossibleAzmoon = model.AzmoonId;
            var qa = context.Azmoons.Find(model.AzmoonId);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanEditResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);




            string MsgIsManualPayment = "";
            if (qa.AzmoonPaymentType == PaymentType.ManualPayment)
                MsgIsManualPayment = "Yes";
            else
                MsgIsManualPayment = "No";
            ViewBag.IsManualPayment = MsgIsManualPayment;


            if (model.AzmoonId == null)
            {
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else if (model.AzmoonId == 0)
            {
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
            }
            else
            {
                if (string.IsNullOrEmpty(model.Melicode) || string.IsNullOrWhiteSpace(model.Melicode))
                {
                    return RedirectToAction(MVC.Valentear.ActionNames.EditRegister, MVC.Valentear.Name, new { AID = model.AzmoonId });
                }
            }



            var qvlst = context.Valentears.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.TrackingCode.Equals(model.Melicode)).ToList();
            if (qvlst.Count() > 0)
            {
                var V = qvlst.First();

                IList<Jazb.Model.ValentearModel.DevotionTypeModel> lstADT = new List<Jazb.Model.ValentearModel.DevotionTypeModel>();
                IList<Jazb.Model.ValentearModel.QoutaTypeModel> lstQTM = new List<Jazb.Model.ValentearModel.QoutaTypeModel>();





                ViewBag.PossibleGenders = GetGender();
                ViewBag.PossibleReligions = GetReligions();
                ViewBag.PossibleFaiths = GetFaithses();
                ViewBag.PossibleConscriptStatus = GetConscriptStatuses();
                ViewBag.PossibleMarriageStatus = GetMarriageStatus();
                ViewBag.PossibleDegrees = GetAzmoonDegrees(model.AzmoonId);
                ViewBag.PossibleEducatedSkills = GetAzmoonEducatedSkills(model.AzmoonId);
                ViewBag.PossibleJobs = GetAzmoonJobs(model.AzmoonId);
                ViewBag.PossiblePlaces = GetPlaces();
                ViewBag.PossiblePlanStatus = GetPlanStatus();
                ViewBag.PossibleCooperationHistories = GetCooperationHistories();


                Jazb.Model.ValentearModel.EditRegisterModel RM = new Model.ValentearModel.EditRegisterModel();
                EditValentearModel VM = new EditValentearModel();
                AutoMapper.Mapper.Map(V, VM);
                VM.PlaceTitle = GetPlaceTitle(V.PlaceId, V.JobId, V.Azmoon.AzmoonId);
                VM.JobTitle = GetJobTitle(V.JobId, V.Azmoon.AzmoonId);
                VM.AzmoonId = model.AzmoonId;
                V.Azmoon.AzmoonId = model.AzmoonId;


                foreach (var t in context.AzmoonDevotionTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                    lstADT.Add(new Model.ValentearModel.DevotionTypeModel { DevotionTypeTitle = t.DevotionTypeTitle, Grade = t.Grade, Id = t.Id });


                foreach (var t in context.AzmoonQoutaTypes.Where(x => x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList())
                    lstQTM.Add(new Model.ValentearModel.QoutaTypeModel { QoutaTypeTitle = t.QoutaTypeTitle, Grade = t.Grade, Id = t.Id });


                var ListAQV = AutoMapper.Mapper.Map<List<AzmoonQoutasValentear>, IEnumerable<Jazb.Model.ValentearModel.QoutaTypeModel>>(V.AzmoonQoutasValentears.ToList());
                var ListADV = AutoMapper.Mapper.Map<List<AzmoonDevotionValentear>, IEnumerable<Jazb.Model.ValentearModel.DevotionTypeModel>>(V.AzmoonDevotionValentearas.ToList());

                RM.Valentear = VM;
                RM.DevotionTypes = lstADT.OrderBy(x => x.Grade);
                RM.SelectedDevotionTypes = ListADV.OrderBy(x => x.Grade).ToList();

                RM.QoutaTypes = lstQTM;
                RM.SelectedQoutaTypes = ListAQV.ToList();

                return View(MVC.Valentear.Views.EditValentear, RM);
            }
            return RedirectToAction(MVC.Valentear.ActionNames.EditRegister, MVC.Valentear.Name, new { AID = model.AzmoonId });
        }

        [HttpPost]
        public virtual ActionResult EditValentear(Model.ValentearModel.EditRegisterModel RM)
        {
            ViewBag.PossibleAzmoon = RM.Valentear.AzmoonId;
            var qa = context.Azmoons.Find(RM.Valentear.AzmoonId);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanEditResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);


            string MsgIsManualPayment = "";
            if (qa.AzmoonPaymentType == PaymentType.ManualPayment)
                MsgIsManualPayment = "Yes";
            else
                MsgIsManualPayment = "No";
            ViewBag.IsManualPayment = MsgIsManualPayment;

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
            RM.Valentear.DegreeTitle = GetDegreeTitle(RM.Valentear.DegreeId, RM.Valentear.AzmoonId);
            RM.Valentear.EducatedSkillTitle = GetEducatedSkillTitle(RM.Valentear.EducatedSkillId, RM.Valentear.AzmoonId);
            RM.Valentear.JobTitle = GetJobTitle(RM.Valentear.JobId, RM.Valentear.AzmoonId);
            RM.Valentear.PlaceTitle = GetPlaceTitle(RM.Valentear.PlaceId, RM.Valentear.JobId, RM.Valentear.AzmoonId);
            RM.Valentear.PlanStatusTitle = GetPlanStatuTitle(RM.Valentear.PlanStatusId);
            RM.Valentear.ConscriptStatusTitle = GetConscriptStatuseTitle(RM.Valentear.ConscriptStatusId);
            RM.Valentear.CooperationHistoryTitle = GetCooperationHistorieTitle(RM.Valentear.CooperationHistoryId);


            var personel_picture = Request.Files["personel_picture"];
            var fish_variz = Request.Files["fish_varizi"];
            var Pic_Tarh = Request.Files["Pic_Tarh"];
            var Pic_Conscript = Request.Files["Pic_Conscript"];
            var Pic5 = Request.Files["Pic5"];
            var Pic6 = Request.Files["Pic6"];
            var Pic7 = Request.Files["Pic7"];
            var Pic8 = Request.Files["Pic8"];
            // var fish_variz2 = Request.Files["fish_variz2"];

            //if ((personel_picture == null) || (personel_picture.ContentLength == 0) || (string.IsNullOrEmpty(personel_picture.FileName)))
            //{
            //    ModelState.AddModelError("personel_picture", "لطفا تصویر داوطلب را وارد کنید");
            //    return View(RM);
            //}


            //if ((fish_variz == null) || (fish_variz.ContentLength == 0) || (string.IsNullOrEmpty(fish_variz.FileName)))
            //{
            //    ModelState.AddModelError("fish_variz", "لطفا تصویر فیش واریزی را وارد کنید");
            //    return View(RM);
            //}

            //if ((Pic_Tarh == null) || (Pic_Tarh.ContentLength == 0) || (string.IsNullOrEmpty(Pic_Tarh.FileName)))
            //{
            //    ModelState.AddModelError("Pic_Tarh", "لطفا تصویر طرح را وارد کنید");
            //    return View(RM);
            //}

            Session["KRB_personel_picture"] = "0";
            Session["KRB_fish_variz"] = "0";
            Session["KRB_Pic_Tarh"] = "0";
            Session["KRB_Pic_Conscript"] = "0";
            Session["KRB_Pic5"] = "0";
            Session["KRB_Pic6"] = "0";
            Session["KRB_Pic7"] = "0";
            Session["KRB_Pic8"] = "0";
            if ((personel_picture.ContentLength != 0) || (!string.IsNullOrEmpty(personel_picture.FileName)))
            {
                RM.Valentear.PictureValenteer = getBytefile(personel_picture);
                Session["KRB_personel_picture"] = "1";
            }


            if ((fish_variz.ContentLength != 0) || (!string.IsNullOrEmpty(fish_variz.FileName)))
            {
                RM.Valentear.FishVariszi = getBytefile(fish_variz);
                Session["KRB_fish_variz"] = "1";
            }

            if ((Pic_Tarh.ContentLength != 0) || (!string.IsNullOrEmpty(Pic_Tarh.FileName)))
            {
                RM.Valentear.Pic_Tarth = getBytefile(Pic_Tarh);
                Session["KRB_Pic_Tarh"] = "1";
            }

            if ((Pic_Conscript.ContentLength != 0) || (!string.IsNullOrEmpty(Pic_Conscript.FileName)))
            {
                RM.Valentear.Pic_Conscript = getBytefile(Pic_Conscript);
                Session["KRB_Pic_Conscript"] = "1";
            }


            if ((Pic5.ContentLength != 0) || (!string.IsNullOrEmpty(Pic5.FileName)))
            {
                RM.Valentear.Pic5 = getBytefile(Pic5);
                Session["KRB_Pic5"] = "1";
            }

            if ((Pic6.ContentLength != 0) || (!string.IsNullOrEmpty(Pic6.FileName)))
            {
                RM.Valentear.Pic6 = getBytefile(Pic6);
                Session["KRB_Pic6"] = "1";
            }

            if ((Pic7.ContentLength != 0) || (!string.IsNullOrEmpty(Pic7.FileName)))
            {
                RM.Valentear.Pic7 = getBytefile(Pic7);
                Session["KRB_Pic7"] = "1";
            }

            if ((Pic8.ContentLength != 0) || (!string.IsNullOrEmpty(Pic8.FileName)))
            {
                RM.Valentear.Pic8 = getBytefile(Pic8);
                Session["KRB_Pic8"] = "1";
            }




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
                Session["KRBRMEDIT"] = RM;
                return View(MVC.Valentear.Views.ViewDataBeforEdit, RM);
            }

            return View(RM);
        }

        [HttpPost]
        public virtual ActionResult EditData(Jazb.Model.ValentearModel.EditRegisterModel RM)
        {
            ViewBag.PossibleAzmoon = RM.Valentear.AzmoonId;
            var qa = context.Azmoons.Find(RM.Valentear.AzmoonId);


            string MsgIsManualPayment = "";
            if (qa.AzmoonPaymentType == PaymentType.ManualPayment)
                MsgIsManualPayment = "Yes";
            else
                MsgIsManualPayment = "No";
            ViewBag.IsManualPayment = MsgIsManualPayment;

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowCanEditResult != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (Session["KRBRMEDIT"] == null)
                return RedirectToAction(MVC.Admin.Valentear.ActionNames.Edit, MVC.Admin.Valentear.Name);



            if (Session["KRBRMEDIT"] != null)
            {
                RM = (Jazb.Model.ValentearModel.EditRegisterModel)Session["KRBRMEDIT"];

                RM.Valentear.DateRegister = DateTime.Now;
                var V = context.Valentears.Find(RM.Valentear.Id);
                AutoMapper.Mapper.Map(RM.Valentear, V);
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


                if ((!string.IsNullOrEmpty(V.WoundedPercent)))
                {
                    V.SumQoutaType = V.SumQoutaType + 32768;
                    V.MaxQoutaType = 32768;
                    V.TextQoutaType = "جانباز " + V.WoundedPercent + " درصد - " + V.TextQoutaType;
                }


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
                context.Entry(V).Property(x => x.ChairCode).IsModified = false;
                context.Entry(V).Property(x => x.NationalID).IsModified = false;



                if (Session["KRB_Pic_Tarh"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic_Tarth).IsModified = false;

                if (Session["KRB_personel_picture"].ToString() == "0")
                    context.Entry(V).Property(x => x.PictureValenteer).IsModified = false;

                if (Session["KRB_fish_variz"].ToString() == "0")
                    context.Entry(V).Property(x => x.FishVariszi).IsModified = false;

                if (Session["KRB_Pic_Conscript"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic_Conscript).IsModified = false;

                if (Session["KRB_Pic5"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic5).IsModified = false;

                if (Session["KRB_Pic6"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic6).IsModified = false;

                if (Session["KRB_Pic7"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic7).IsModified = false;

                if (Session["KRB_Pic8"].ToString() == "0")
                    context.Entry(V).Property(x => x.Pic8).IsModified = false;

                context.SaveChanges();
                Session["KRBRMEDIT"] = null;

            }

            return RedirectToAction(MVC.Valentear.ActionNames.PrintData, MVC.Valentear.Name);
        }

        public virtual ActionResult PrintData()
        {
            if (Session["KRBKEYVID"] == null)
            {
                return View(MVC.Valentear.Views._errorPrintData);
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
                VM.AzmoonId = V.Azmoon.AzmoonId;

                VM.GenderTitle = GetGender(VM.GenderId);
                VM.ReligionTitle = GetReligions(VM.ReligionId);
                VM.FaithTitle = GetFaithses(VM.FaithId);
                VM.MarriageStatusTitle = GetMarriageStatus(VM.MarriageStatusId);
                VM.DegreeTitle = GetDegreeTitle(VM.DegreeId, VM.AzmoonId);
                VM.EducatedSkillTitle = GetEducatedSkillTitle(VM.EducatedSkillId, VM.AzmoonId);
                VM.JobTitle = GetJobTitle(VM.JobId, VM.AzmoonId);
                VM.PlaceTitle = GetPlaceTitle(VM.PlaceId, VM.JobId, VM.AzmoonId);
                VM.PlanStatusTitle = GetPlanStatus(VM.PlanStatusId);
                VM.ConscriptStatusTitle = GetConscriptStatuses(VM.ConscriptStatusId);
                VM.CooperationHistoryTitle = GetCooperationHistories(VM.CooperationHistoryId);


                RM.Valentear = VM;
                RM.SelectedDevotionTypes = qADT;
                RM.SelectedQoutaTypes = qAQT;
                Session["KRBKEYVID"] = null;

                ViewBag.PossibleSignTextValenter = V.Azmoon.SignTextValentear;
                ViewBag.PossiblePicture = V.Azmoon.PicturAzmoon;
                return View(MVC.Valentear.Views.PrintData, RM);

            }

            return View();
        }
        public virtual ActionResult PrintDataByMelicode(int azmoonzid, string natinalId)
        {
            var qV = context.Valentears.Where(x => x.Azmoon.AzmoonId == azmoonzid && x.NationalID.Equals(natinalId)).ToList();
            if (qV.Count() > 0)
            {
                Session["KRBKEYVID"] = qV.First().Id;

                return RedirectToAction(MVC.Valentear.ActionNames.PrintData, MVC.Valentear.Name);
            }
            Session["KRBKEYVID"] = null;
            return RedirectToAction(MVC.Valentear.ActionNames.PrintData, MVC.Valentear.Name);
        }



        //public virtual ActionResult ViewCard(int AID)
        //{
        //    var qa = context.Azmoons.Find(AID);

        //    //if(!Request.IsAuthenticated)
        //    if (qa.Active != true)
        //        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

        //    if (qa.ShowPrintCard != true)
        //        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

        //    ViewCardModel VCM = new ViewCardModel
        //    {
        //        AzmoonId = AID
        //    };

        //    return View(MVC.Valentear.Views.ViewCard, VCM);
        //}
        //[HttpPost]

        //public virtual ActionResult ViewCard(Jazb.Model.ValentearModel.ViewCardModel model)
        //{
        //    var qa = context.Azmoons.Find(model.AzmoonId);

        //    //if(!Request.IsAuthenticated)
        //    if (qa.Active != true)
        //        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

        //    if (qa.ShowPrintCard != true)
        //        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);



        //    if (!ModelState.IsValid)
        //        return View(model);

        //    if (ModelState.IsValid)
        //    {
        //        var vlist = context.Valentears.Where(x => x.NationalID.Equals(model.NationalCode) &&
        //                                               (x.Azmoon.AzmoonId == model.AzmoonId) &&
        //                                                x.Azmoon.Active == true &&
        //                                                x.IsPaymented == true
        //                                                )
        //                                                .Select(x => new { x.Id, x.Accept, x.JobId, x.PlaceId, x.Azmoon })
        //                                                .ToList();


        //        if (vlist.Count() > 0)
        //        {
        //            var V = vlist.First();
        //            if (V.Accept == 1)
        //            {

        //                var placejobs = context.AzmoonPlaces.Include(x => x.Azmoon)
        //                                              .Where(x => x.JobId == V.JobId &&
        //                                                     x.PlaceCode == V.PlaceId &&
        //                                                     x.Azmoon.AzmoonId == V.Azmoon.AzmoonId)
        //                                                .ToList();
        //                if (placejobs.Count() > 0)
        //                {
        //                    var placejob = placejobs.First();
        //                    if (placejob.AcceptValentears)
        //                    {
        //                        // قبولی همه داوطلبان و نمایش پیغام
        //                        Session["KRBKEYVID"] = V.Id;
        //                        return RedirectToAction(MVC.Valentear.ActionNames.DontAccept, MVC.Valentear.Name);
        //                    }
        //                }

        //                Session["KRBKEYVID"] = V.Id;
        //                return RedirectToAction(MVC.Valentear.ActionNames.PrintCard, MVC.Valentear.Name);


        //            }
        //            {
        //                Session["KRBKEYVID"] = V.Id;
        //                return RedirectToAction(MVC.Valentear.ActionNames.DontAccept, MVC.Valentear.Name);
        //            }
        //        }
        //        else
        //        {
        //            return PartialView(MVC.Valentear.Views._errorPrintData);
        //        }
        //    }
        //    Session["KRBKEYVID"] = null;

        //    return View();
        //}


        public virtual ActionResult ViewCard(int AID)
        {
            var qa = context.Azmoons.Find(AID);

            //if(!Request.IsAuthenticated)
            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowPrintCard != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            ViewCardModel VCM = new ViewCardModel
            {
                AzmoonId = AID
            };

            return View(MVC.Valentear.Views.ViewCard, VCM);
        }
        [HttpPost]

        public virtual ActionResult ViewCard(Jazb.Model.ValentearModel.ViewCardModel model)
        {
            var qa = context.Azmoons.Find(model.AzmoonId);

            if (qa.Active != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            if (qa.ShowPrintCard != true)
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);



            if (!ModelState.IsValid)
                return View(model);

            if (ModelState.IsValid)
            {
                var vl = context.Valentears.Where(x => x.Azmoon.AzmoonId == model.AzmoonId && x.NationalID == model.NationalCode).Select(x => new { x.Id }).ToList();
                if(vl.Count()>0)
                {
                    var v = vl.First();
                    var VADList = context.AzmoonDevotionValentears.Where(x => x.Valentear.Id == v.Id).ToList();
                    bool DTU25 = false;
                    foreach (var VAD in VADList)
                    {
                        var ADList = context.DevotionTypes.Where(x => x.DevotionTypeTitle == VAD.DevotionTitle).ToList();
                        if (ADList.Count() > 0)
                        {
                            var AD = ADList.First();
                            if (AD.Grade >= 128)
                                DTU25 = true;
                        }
                    }

                    if(DTU25)
                    {
                        Session["KRBKEYVID"] = v.Id;
                        return RedirectToAction(MVC.Valentear.ActionNames.PrintCard, MVC.Valentear.Name);
                    }

                    var pay = context.Payments.Where(x => x.AzmoonId == model.AzmoonId && x.ValentearId == v.Id && x.PaymentFinished == true && x.StatusPayment == "OK").ToList();
                    if (pay.Count() > 0)
                    {
                        Session["KRBKEYVID"] = v.Id;
                       return RedirectToAction(MVC.Valentear.ActionNames.PrintCard, MVC.Valentear.Name);
                    }
                    else
                    {
                        return RedirectToAction(MVC.Payment.ActionNames.PIndex, MVC.Payment.Name, new { AID = model.AzmoonId, Vid = v.Id });
                         Session["KRBKEYVID"] = v.Id;
                    }

                }
               

            }
            Session["KRBKEYVID"] = null;

            return View();
        }


        public virtual ActionResult PrintCard()
        {

            if (Session["KRBKEYVIDBACKOFBANK"] != null)
                if (Session["KRBKEYVIDBACKOFBANK"] != "0")
                {
                    Session["KRBKEYVID"] = Session["KRBKEYVIDBACKOFBANK"];
                    Session["KRBKEYVIDBACKOFBANK"] = null;
                }
                   
            Session["RegionExam"] = null;
            if (Session["KRBKEYVID"] == null)
            {
                return PartialView(MVC.Valentear.Views._errorPrintData);
            }

            if (Session["KRBKEYVID"] != null)
            {
                var VID = (int)Session["KRBKEYVID"];

                Valentear V = context.Valentears.Find(VID);
                List<Option> lstOptions = context.Options.ToList();

                /// ------------------- بررسی بازبودن امکان چاپ کارت ورود به جلسه--------------------
                var qa = context.Azmoons.Find(V.Azmoon.AzmoonId);
                if (qa.Active != true)
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

                if (qa.ShowPrintCard != true)
                    return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

                //------------------------------------------------

                V.LastDatePrinted = DateTime.Now;
                V.RecordPrinted = 1;
                context.Valentears.Attach(V);
                context.Entry(V).Property(x => x.LastDatePrinted).IsModified = true;
                context.Entry(V).Property(x => x.RecordPrinted).IsModified = true;
                context.SaveChanges();



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




                CardAuthorizationExam RM = new CardAuthorizationExam();
                RM.ChairCode = V.ChairCode;
                RM.CompanyLogo = lstOptions.Where(x => x.Name == "CompanyLogo" && x.Value != null).First().Value;
                RM.CompanyAddress = lstOptions.Where(x => x.Name == "CompanyAddress" && x.Value != null).First().Value;
                RM.CompanyName = lstOptions.Where(x => x.Name == "SiteName" && x.Value != null).First().Value;
                RM.SiteUrl = lstOptions.Where(x => x.Name == "SiteUrl" && x.Value != null).First().Value;
                RM.DateExecuteAzmoon = V.Azmoon.DateExecuteAzmoon.ToPersianDate();
                RM.Description = "";
                RM.FatherName = V.FatherName;
                RM.FirstName = V.FirstName;
                RM.ValentearPicture = V.PictureValenteer;
                RM.GenderTitle = GetGender(V.GenderId);
                RM.DegreeTitle = GetDegreeTitle(V.DegreeId, V.Azmoon.AzmoonId);
                RM.InputCardTitle = V.Azmoon.InputCardTitle;
                RM.JobTitle = GetJobTitle(V.JobId, V.Azmoon.AzmoonId);
                RM.LastName = V.LastName;
                RM.LocationExecuteAzmoon = V.Azmoon.LocationExecuteAzmoon;
                RM.NationalId = V.NationalID;
                RM.PlaceTitle = GetPlaces(V.PlaceId);
                RM.TimeExecuteAzmoon = V.Azmoon.TimeExecuteAzmoon;
                RM.TrackingCodePaymentOnline = "";
                RM.TypePayment = "";

                RM.SelectedDevotionTypes = qADT;
                RM.SelectedQoutaTypes = qAQT;

                var azjobs = context.AzmoonJobs.Where(x => x.AzmoonJobCode == V.JobId && x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).ToList();
                if (azjobs.Count() > 0)
                {

                    RM.AreaExecuteAzmoon = V.AreaExecuteAzmoon;
                    //if (V.GenderId == 2)
                    //{
                    //    RM.AreaExecuteAzmoon = azjobs.First().RegionExam == null ? "" : azjobs.First().RegionExam;
                    //}
                    //else
                    //{
                    //    RM.AreaExecuteAzmoon  = azjobs.First().RegionExamWoman == null ? "" : azjobs.First().RegionExamWoman;
                    //}

                }


                Session["KRBKEYVID"] = null;
                return View(MVC.Valentear.Views.PrintCard, RM);
            }
            return View();
        }


        public virtual ActionResult DontAccept()
        {
            ViewBag.AzmoonJobTitle = "";
            if (Session["KRBKEYVID"].ToString() == string.Empty)
            {
                return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);

            }
            int ValentearId = int.Parse(Session["KRBKEYVID"].ToString());
            Session["KRBKEYVID"] = "";
            ViewBag.MessageDontAccept = "";
            var V = context.Valentears.Find(ValentearId);

            ViewBag.GenterTitle = V.GenderId == 1 ? "سرکار خانم" : "جناب آقای ";
            ViewBag.FullName = string.Format("{0} {1}", V.FirstName, V.LastName);
            ViewBag.AzmoonTitle = V.Azmoon.Name;
            ViewBag.AzmoonDate = V.Azmoon.DateExecuteAzmoon.ToPersianDate();
            ViewBag.AzmoonJobTitle = context.AzmoonJobs.Where(x => x.AzmoonJobCode == V.JobId && x.Azmoon.AzmoonId == V.Azmoon.AzmoonId).First().AzmoonJobTitle;


            switch (V.Accept)
            {
                case 0:
                    {
                        ViewBag.Check = "No";
                    }
                    break;
                case 1:
                    {
                        var placejobs = context.AzmoonPlaces.Include(x => x.Azmoon)
                                                          .Where(x => x.JobId == V.JobId &&
                                                                 x.PlaceCode == V.PlaceId &&
                                                                 x.Azmoon.AzmoonId == V.Azmoon.AzmoonId)
                                                            .ToList();
                        if (placejobs.Count() > 0)
                        {
                            var placejob = placejobs.First();
                            if (placejob.AcceptValentears)
                            {
                                ViewBag.Check = "YesNo";
                                ViewBag.MessageDontAccept = placejob.Description;
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        ViewBag.Check = "Yes";
                        ViewBag.MessageDontAccept = V.Description;
                    }
                    break;
            }



            return View();

        }

        public virtual ActionResult EditSahmiyeh()
        {
            List<Valentear> lstFiveDevotionValentear = new List<Valentear>();
            Valentear V;
            string[] strspilit = { "-" };
            int azmoonId = 26;
            int i = 0;
            var Devolist = context.DevotionTypes.ToList();
            var qouta = context.QoutaTypes.ToList();
            var v = context.Valentears.Where(x => x.Azmoon.AzmoonId == azmoonId).Select(x => new { x.Id, x.TextDevotionType, x.TextQoutaType }).AsQueryable().ToList();
            foreach (var t in v)
            {

                V = context.Valentears.Find(t.Id);

                i++;
                System.Diagnostics.Debug.WriteLine(i.ToString() + " - " + t.Id.ToString());
                var lstdevotionsValentear = t.TextDevotionType.Split(strspilit, StringSplitOptions.None).ToList();
                var lstQoutassValentear = t.TextQoutaType.Split(strspilit, StringSplitOptions.None).ToList();

                var lstDevotions = (from d in context.DevotionTypes
                                    where lstdevotionsValentear.Contains(d.DevotionTypeTitle)
                                    select new
                                    {
                                        d.DevotionTypeTitle,
                                        d.Grade,
                                        d.Id
                                    }).ToList();

                var lstQoutas = (from d in context.QoutaTypes
                                 where lstQoutassValentear.Contains(d.QoutaTypeTitle)
                                 select new
                                 {
                                     d.QoutaTypeTitle,
                                     d.Grade,
                                     d.Id
                                 }).ToList();



                V.MaxQoutaType = 0;
                V.SumQoutaType = 0;

                V.SumDevotionType = 0;
                V.MaxDevotionType = 0;

                V.MaxQoutaType = int.Parse(lstQoutas.Max(x => x.Grade).ToString());
                V.SumQoutaType = int.Parse(lstQoutas.Sum(x => x.Grade).ToString());

                V.MaxDevotionType = int.Parse(lstDevotions.Max(x => x.Grade).ToString());
                V.SumDevotionType = int.Parse(lstDevotions.Sum(x => x.Grade).ToString());

                context.Valentears.Attach(V);
                context.Entry(V).Property(x => x.MaxQoutaType).IsModified = true;
                context.Entry(V).Property(x => x.SumQoutaType).IsModified = true;
                context.Entry(V).Property(x => x.MaxDevotionType).IsModified = true;
                context.Entry(V).Property(x => x.SumDevotionType).IsModified = true;
                context.SaveChanges();

                //foreach (var devs in lstDevotions)
                //{
                //    AzmoonDevotionValentear ADV = new AzmoonDevotionValentear
                //    {
                //        DevotionCode = devs.Grade,
                //        DevotionTitle = devs.DevotionTypeTitle,
                //        Valentear = t
                //    };
                //    context.AzmoonDevotionValentears.Add(ADV);
                //}


                //foreach (var qouts in lstQoutas)
                //{
                //    AzmoonQoutasValentear AQV = new AzmoonQoutasValentear
                //    {
                //        QoutaCode = qouts.Grade,
                //        QoutaTitle = qouts.QoutaTypeTitle,
                //        Valentear = t
                //    };
                //    context.AzmoonQoutasValentears.Add(AQV);
                //}


                //  context.SaveChanges();
            }

           // _uow.BulkUpdateAllData(lstFiveDevotionValentear);
            return View();
        }


        protected bool IsNationalIdRegister(int AzmoonId, string nationalid)
        {
            var q = context.Valentears.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.NationalID.Equals(nationalid)).ToList();
            if (q.Count() > 0) return true;
            return false;
        }
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public virtual ActionResult IsNationalId([Bind(Prefix = "Valentear.NationalID")]string nationalid, [Bind(Prefix = "Valentear.AzmoonId")] int AzmoonId)
        {
            var q = context.Valentears.Where(x => x.NationalID.Equals(nationalid) && x.Azmoon.AzmoonId == AzmoonId).ToList();
            if (q.Count() > 0) return Json(false);
            return Json(true);
        }
        #region "Private Methds"


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


        protected string ConvertStringArrayToStringJoin(string seperator, string[] array)
        {
            //
            // Use string Join to concatenate the string elements.
            //
            string result = string.Join(seperator, array);
            return result;
        }

        public virtual ActionResult GetLocationRequest(int azmoonId, int? Jobid, int? GenderId)
        {
            List<AzmoonPlace> q;
            if (GenderId == 1)
                q = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == azmoonId && x.JobId == Jobid && x.WillWoman == true).ToList();
            else
                q = context.AzmoonPlaces.Where(x => x.Azmoon.AzmoonId == azmoonId && x.JobId == Jobid && x.WillMan == true).ToList();


            string strOption = "<option value='{0}'>{1}</option>";
            string strOptions = "";
            strOptions += string.Format(strOption, 0, "--- انتخاب ---");
            foreach (var item in q)
            {
                strOptions += string.Format(strOption, item.PlaceCode, item.PlaceTitle);
            }

            return Content(strOptions);
        }

        private SelectList GetGender()
        {
            var qGender = (from p in context.Genders
                           select new SelectListItem
                           {
                               Text = p.GenderTitle,
                               Value = p.GenderCode.ToString()
                           }).ToList();

            return new SelectList(qGender, "Value", "Text");

        }
        private SelectList GetConscriptStatuses()
        {
            var qConscriptStatus = (from p in context.ConscriptStatuses
                                    select new SelectListItem
                                    {
                                        Text = p.ConscriptStatusTitle,
                                        Value = p.ConscriptStatusCode.ToString()
                                    }).ToList();

            return new SelectList(qConscriptStatus, "Value", "Text");
        }
        private SelectList GetReligions()
        {
            var qReligion = (from p in context.Religions
                             select new SelectListItem
                             {
                                 Text = p.ReligionTitle,
                                 Value = p.ReligionCode.ToString()
                             }).ToList();

            return new SelectList(qReligion, "Value", "Text");
        }
        private SelectList GetFaithses()
        {
            var qFaithse = (from p in context.Faiths
                            select new SelectListItem
                            {
                                Text = p.FaithTitle,
                                Value = p.FaithCode.ToString()
                            }).ToList();

            return new SelectList(qFaithse, "Value", "Text");


        }
        private SelectList GetAzmoonEducatedSkills(int AzmoonId)
        {
            var qEducatedSkill = (from p in context.AzmooneducatedSkills
                                  where p.Azmoon.AzmoonId == AzmoonId
                                  select new SelectListItem
                                  {
                                      Text = p.AzmooneducatedSkillTitle,
                                      Value = p.AzmooneducatedSkillCode.ToString()
                                  }).ToList();

            return new SelectList(qEducatedSkill, "Value", "Text");
        }
        private SelectList GetMarriageStatus()
        {
            var qMarriage = (from p in context.marriageStatuses
                             select new SelectListItem
                             {
                                 Text = p.marriageStatusTitle,
                                 Value = p.marriageStatusCode.ToString()
                             }).ToList();
            return new SelectList(qMarriage, "Value", "Text");
        }
        private SelectList GetAzmoonDegrees(int AzmoonId)
        {
            var qDegree = (from p in context.AzmoonDegrees
                           where p.Azmoon.AzmoonId == AzmoonId
                           orderby p.Periority ascending
                           select new SelectListItem
                           {
                               Text = p.Title,
                               Value = p.Code.ToString()
                           }).ToList();
            return new SelectList(qDegree, "Value", "Text");
        }
        private SelectList GetAzmoonJobs(int AzmoonId)
        {
            var qJob = (from p in context.AzmoonJobs
                        where p.Azmoon.AzmoonId == AzmoonId
                        select new SelectListItem
                        {
                            Text = p.AzmoonJobTitle,
                            Value = p.AzmoonJobCode.ToString()
                        }).ToList();
            return new SelectList(qJob, "Value", "Text");
        }
        private SelectList GetPlaces()
        {

            //var qPlace = (from p in context.AzmoonPlaces
            //              select new SelectListItem
            //              {
            //                  Text = p.PlaceTitle,
            //                  Value = p.PlaceCode.ToString()
            //              }).ToList();

            var qPlace = new List<SelectListItem>();

            return new SelectList(qPlace, "Value", "Text");
        }
        private SelectList GetPlanStatus()
        {

            var qPlanStatus = (from p in context.PlaneStatuses
                               select new SelectListItem
                               {
                                   Text = p.PlaneStatusTitle,
                                   Value = p.PlaneStatusCode.ToString()
                               }).ToList();
            return new SelectList(qPlanStatus, "Value", "Text");
        }

        private SelectList GetCooperationHistories()
        {

            var qcooperationHistories = (from p in context.cooperationHistories
                                         select new SelectListItem
                                         {
                                             Text = p.cooperationHistoryTitle,
                                             Value = p.cooperationHistoryCode.ToString()
                                         }).ToList();
            return new SelectList(qcooperationHistories, "Value", "Text");
        }
        private string GetGender(int Id)
        {
            var q = context.Genders.Where(x => x.GenderCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().GenderTitle;


            return "مشخص نشده";


        }
        private string GetConscriptStatuses(int Id)
        {
            var q = context.ConscriptStatuses.Where(x => x.ConscriptStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().ConscriptStatusTitle;


            return "مشخص نشده";
        }
        private string GetReligions(int Id)
        {
            var q = context.Religions.Where(x => x.ReligionCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().ReligionTitle;


            return "مشخص نشده";
        }
        private string GetFaithses(int Id)
        {
            var q = context.Faiths.Where(x => x.FaithCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().FaithTitle;


            return "مشخص نشده";


        }
        private string GetEducatedSkills(int Id)
        {
            var q = context.AzmooneducatedSkills.Where(x => x.AzmooneducatedSkillCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().AzmooneducatedSkillTitle;


            return "مشخص نشده";
        }
        private SelectList GetEducatedSkills(int Id, int AzmoonId)
        {
            var qEducatedSkill = (from p in context.AzmooneducatedSkills.Include(x => x.Azmoon)
                                                                        .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                                  select new SelectListItem
                                  {
                                      Text = p.AzmooneducatedSkillTitle,
                                      Value = p.AzmooneducatedSkillCode.ToString()
                                  }).ToList();

            return new SelectList(qEducatedSkill, "Value", "Text", Id);
        }
        private string GetMarriageStatus(int Id)
        {
            var q = context.marriageStatuses.Where(x => x.marriageStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().marriageStatusTitle;


            return "مشخص نشده";
        }
        private string GetDegrees(int Id)
        {
            var q = context.AzmoonDegrees.Where(x => x.Code.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().Title;


            return "مشخص نشده";
        }
        private string GetJobs(int Id)
        {
            var q = context.AzmoonJobs.Where(x => x.AzmoonJobCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().AzmoonJobTitle;


            return "مشخص نشده";
        }

        private SelectList GetDegrees(int Id, int AzmoonId)
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
        private string GetPlaces(long Id)
        {

            var q = context.AzmoonPlaces.Where(x => x.PlaceCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().PlaceTitle;


            return "مشخص نشده";
        }
        private string GetPlanStatus(int Id)
        {
            var q = context.PlaneStatuses.Where(x => x.PlaneStatusCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().PlaneStatusTitle;


            return "مشخص نشده";
        }
        private string GetCooperationHistories(int Id)
        {
            var q = context.cooperationHistories.Where(x => x.cooperationHistoryCode.Equals(Id)).ToList();
            if (q.Count() > 0)
                return q.First().cooperationHistoryTitle;


            return "مشخص نشده";
        }

        private string GetPlaceTitle(long Id, int JobId, int AzmoonId)
        {

            var q = context.AzmoonPlaces.Where(x => x.PlaceCode.Equals(Id) && x.JobId == JobId && x.Azmoon.AzmoonId == AzmoonId).ToList();
            if (q.Count() > 0)
                return q.First().PlaceTitle;


            return "مشخص نشده";
        }
        private string GetJobTitle(int Id, int AzmoonId)
        {
            var q = context.AzmoonJobs.Where(x => x.AzmoonJobCode.Equals(Id) && x.Azmoon.AzmoonId == AzmoonId).ToList();
            if (q.Count() > 0)
                return q.First().AzmoonJobTitle;


            return "مشخص نشده";
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
        private string GetEducatedSkillTitle(int Id, int AzmoonId)
        {
            var q = context.AzmooneducatedSkills.Where(x => x.AzmooneducatedSkillCode.Equals(Id) && x.Azmoon.AzmoonId == AzmoonId).ToList();
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
        private string GetDegreeTitle(int Id, int AzmoonId)
        {
            var q = context.AzmoonDegrees.Where(x => x.Code.Equals(Id) && x.Azmoon.AzmoonId == AzmoonId).ToList();
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
