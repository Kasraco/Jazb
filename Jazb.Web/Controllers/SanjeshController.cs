using Jazb.Datalayer.Context;
using Jazb.Model.Sanjesh;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Jazb.Web.Controllers
{

    [Authorize(Roles = "admin,moderator")]
    public partial class SanjeshController : Controller
    {
        private JazbDbContext db = new JazbDbContext();
        // GET: Sanjesh
        public virtual ActionResult Index()
        {
            return View(MVC.Sanjesh.Views.Index);
        }

        public virtual ActionResult CheckData(Jazb.Model.Sanjesh.LoginSanjesh model)
        {
            AddPictureModel VPM;
            if (ModelState.IsValid)
            {
                var lst = db.AttachmentImageSanjeshs.Where(x => x.DavNO == model.Password && x.Melicode == model.UserName).ToList();
                if (lst != null)
                    if (lst.Count() > 0)
                    {
                        var at = lst.First();
                        if (at.AzmoonResult == 1 || at.AzmoonResult == 4 || at.AzmoonResult == 15)
                        {
                         if(at.EditCode!=1)
                         {
                             VPM = new AddPictureModel { DavNO = at.DavNO, Fname = at.Fname, Lname = at.Lname, ID = at.ID, Melicode = at.Melicode, PNO = at.PNO };
                             return View(MVC.Sanjesh.Views.UploadPicture, VPM);
                         }
                         else
                         {
                             ModelState.AddModelError("", "داوطلب گرامی شما یک بار مدارک خود را ارسال کرده اید.");
                             return View(MVC.Sanjesh.Views.Index);
                         }
                        }
                        ModelState.AddModelError("", "کد ملی یا شماره داوطلبی اشتباه است");
                        return View(MVC.Sanjesh.Views.Index);
                    }
                    else
                    {
                        ModelState.AddModelError("", "کد ملی یا شماره داوطلبی اشتباه است");
                        return View(MVC.Sanjesh.Views.Index);
                    }
            }

            return View(MVC.Sanjesh.Views.Index);
        }

        private byte[] ConvertToArray(HttpPostedFileBase Image)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                Image.InputStream.CopyTo(stream);
                bytes = stream.ToArray();
            }
            return bytes;
        }
        public virtual ActionResult ViewDataBeforeSave()
        {

            ViewBag.PossibleSignTextValenter = "";
            PrintDataSanjeshModel PDSM = new PrintDataSanjeshModel();
            AddPictureModel APM = new AddPictureModel();
            List<string> strError = new List<string>();
            Session["Molahezat"] = "";

            try
            {
                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    var personel_picture = HttpContext.Request.Files["personel_picture"];
                    var personel_Sanjesh = HttpContext.Request.Files["personel_Sanjesh"];
                    var personel_GVT = HttpContext.Request.Files["personel_GVT"];
                    var personel_Shsh1 = HttpContext.Request.Files["personel_Shsh1"];
                    var personel_Shsh2 = HttpContext.Request.Files["personel_Shsh2"];
                    var personel_Shsh3 = HttpContext.Request.Files["personel_Shsh3"];
                    var personel_melicode_On = HttpContext.Request.Files["personel_melicode_On"];
                    var personel_melicode_Back = HttpContext.Request.Files["personel_melicode_Back"];
                    var personel_Kardani = HttpContext.Request.Files["personel_Kardani"];
                    var personel_Karshenasi = HttpContext.Request.Files["personel_Karshenasi"];
                    var personel_KarshenasiArshad = HttpContext.Request.Files["personel_KarshenasiArshad"];
                    var personel_Doctor = HttpContext.Request.Files["personel_Doctor"];
                    var personel_Isargari1 = HttpContext.Request.Files["personel_Isargari1"];
                    var personel_Isargari2 = HttpContext.Request.Files["personel_Isargari2"];
                    var personel_Isargari3 = HttpContext.Request.Files["personel_Isargari3"];
                    var personel_Nezamvazifeh1 = HttpContext.Request.Files["personel_Nezamvazifeh1"];
                    var personel_Nezamvazifeh2 = HttpContext.Request.Files["personel_Nezamvazifeh2"];
                    var personel_Gavahinameh_on = HttpContext.Request.Files["personel_Gavahinameh_on"];
                    var personel_Gavahinameh_back = HttpContext.Request.Files["personel_Gavahinameh_back"];
                    var personel_Bomi1 = HttpContext.Request.Files["personel_Bomi1"];
                    var personel_Bomi2 = HttpContext.Request.Files["personel_Bomi2"];
                    var personel_Bomi3 = HttpContext.Request.Files["personel_Bomi3"];
                    var personel_Bomi4 = HttpContext.Request.Files["personel_Bomi4"];
                    var personel_SaghfSeni1 = HttpContext.Request.Files["personel_SaghfSeni1"];
                    var personel_SaghfSeni2 = HttpContext.Request.Files["personel_SaghfSeni2"];
                    var personel_SaghfSeni3 = HttpContext.Request.Files["personel_SaghfSeni3"];
                    var personel_CoronaForm = HttpContext.Request.Files["personel_CoronaForm"];
                    var personel_OtheDoc1 = HttpContext.Request.Files["personel_OtheDoc1"];
                    var personel_OtheDoc2 = HttpContext.Request.Files["personel_OtheDoc2"];
                    var personel_OtheDoc3 = HttpContext.Request.Files["personel_OtheDoc3"];


                    var DavNO = HttpContext.Request.Form["DavNO"].ToString();
                    var Fname = HttpContext.Request.Form["Fname"].ToString();
                    var Lname = HttpContext.Request.Form["Lname"].ToString();
                    var Melicode = HttpContext.Request.Form["Melicode"].ToString();
                    var PNO = HttpContext.Request.Form["PNO"].ToString();
                    var TextID = HttpContext.Request.Form["ID"].ToString();
                    long ID = long.Parse(TextID);

                    if (ID > 0)
                    {
                        var vl = db.AttachmentImageSanjeshs.Find(ID);


                        if (vl != null)
                        {
                            APM.DavNO = vl.DavNO;
                            APM.Fname = vl.Fname;
                            APM.Lname = vl.Lname;
                            APM.Melicode = vl.Melicode;
                            APM.PNO = vl.PNO;
                            APM.ID = vl.ID;
                           

                            if (personel_picture != null && personel_picture.ContentLength>0)
                                vl.personel_picture = ConvertToArray(personel_picture);
                            else
                                strError.Add("تصویر داوطلب");
                            if (personel_Sanjesh != null && personel_Sanjesh.ContentLength > 0)
                                vl.personel_Sanjesh = ConvertToArray(personel_Sanjesh);
                            else
                                strError.Add("تصویر کارنامه سنجش");
                            if (personel_GVT != null && personel_GVT.ContentLength > 0)
                                vl.personel_GVT = ConvertToArray(personel_GVT);
                            else
                                strError.Add("تصویر گواهی وضعیت طرح");
                            if (personel_Shsh1 != null && personel_Shsh1.ContentLength > 0) vl.personel_Shsh1 = ConvertToArray(personel_Shsh1);
                            else
                                strError.Add("تصویر صفحه اول شناسنامه");
                            if (personel_Shsh2 != null && personel_Shsh2.ContentLength > 0) vl.personel_Shsh2 = ConvertToArray(personel_Shsh2);
                            else
                                strError.Add("تصویر صفحه دوم شناسنامه");
                            if (personel_Shsh3 != null && personel_Shsh3.ContentLength > 0) vl.personel_Shsh3 = ConvertToArray(personel_Shsh3);
                            else
                                strError.Add("تصویر صفحه سوم شناسنامه");
                            if (personel_melicode_On != null && personel_melicode_On.ContentLength > 0) vl.personel_melicode_On = ConvertToArray(personel_melicode_On);
                            else
                                strError.Add("تصویر روی کارت ملی");
                            if (personel_melicode_Back != null && personel_melicode_Back.ContentLength > 0) vl.personel_melicode_Back = ConvertToArray(personel_melicode_Back);
                            else
                                strError.Add("تصویر پشت کارت ملی");
                            if (personel_Kardani != null && personel_Kardani.ContentLength > 0) vl.personel_Kardani = ConvertToArray(personel_Kardani);
                            else
                                strError.Add("تصویر مدرک کاردانی");
                            if (personel_Karshenasi != null && personel_Karshenasi.ContentLength > 0) vl.personel_Karshenasi = ConvertToArray(personel_Karshenasi);
                            else
                                strError.Add("تصویر مدرک کارشناسی");
                            if (personel_KarshenasiArshad != null && personel_KarshenasiArshad.ContentLength > 0) vl.personel_KarshenasiArshad = ConvertToArray(personel_KarshenasiArshad);
                            else
                                strError.Add("تصویر مدرک کارشناسی ارشد");
                            if (personel_Doctor != null && personel_Doctor.ContentLength > 0) vl.personel_Doctor = ConvertToArray(personel_Doctor);
                            else
                                strError.Add("تصویر مدرک دکتری");
                            if (personel_Isargari1 != null && personel_Isargari1.ContentLength > 0) vl.personel_Isargari1 = ConvertToArray(personel_Isargari1);
                            else
                                strError.Add("تصویر اول ایثارگری");
                            if (personel_Isargari2 != null && personel_Isargari2.ContentLength > 0) vl.personel_Isargari2 = ConvertToArray(personel_Isargari2);
                            else
                                strError.Add("تصویر دوم ایثارگری");
                            if (personel_Isargari3 != null && personel_Isargari3.ContentLength > 0) vl.personel_Isargari3 = ConvertToArray(personel_Isargari3);
                            else
                                strError.Add("تصویر سوم ایثارگری");
                            if (personel_Nezamvazifeh1 != null && personel_Nezamvazifeh1.ContentLength > 0) vl.personel_Nezamvazifeh1 = ConvertToArray(personel_Nezamvazifeh1);
                            else
                                strError.Add("تصویر اول وضعیت نظام وظیفه");
                            if (personel_Nezamvazifeh2 != null && personel_Nezamvazifeh2.ContentLength > 0) vl.personel_Nezamvazifeh2 = ConvertToArray(personel_Nezamvazifeh2);
                            else
                                strError.Add("تصویر دوم وضعیت نظام وظیفه");
                            if (personel_Gavahinameh_on != null && personel_Gavahinameh_on.ContentLength > 0) vl.personel_Gavahinameh_on = ConvertToArray(personel_Gavahinameh_on);
                            else
                                strError.Add("تصویر روی گواهینامه ب2 مخصوص رشته فوریت های پزشکی ");
                            if (personel_Gavahinameh_back != null && personel_Gavahinameh_back.ContentLength > 0) vl.personel_Gavahinameh_back = ConvertToArray(personel_Gavahinameh_back);
                            else
                                strError.Add("تصویر پشت گواهینامه ب2 مخصوص رشته فوریت های پزشکی");
                            if (personel_Bomi1 != null && personel_Bomi1.ContentLength > 0) vl.personel_Bomi1 = ConvertToArray(personel_Bomi1);
                            else
                                strError.Add("تصویر اول بومی بودن");
                            if (personel_Bomi2 != null && personel_Bomi2.ContentLength > 0) vl.personel_Bomi2 = ConvertToArray(personel_Bomi2);
                            else
                                strError.Add("تصویر دوم بومی بودن");
                            if (personel_Bomi3 != null && personel_Bomi3.ContentLength > 0) vl.personel_Bomi3 = ConvertToArray(personel_Bomi3);
                            else
                                strError.Add("تصویر سوم بومی بودن");
                            if (personel_Bomi4 != null && personel_Bomi4.ContentLength > 0) vl.personel_Bomi4 = ConvertToArray(personel_Bomi4);
                            else
                                strError.Add("تصویر چهارم بومی بودن");
                            if (personel_SaghfSeni1 != null && personel_SaghfSeni1.ContentLength > 0) vl.personel_SaghfSeni1 = ConvertToArray(personel_SaghfSeni1);
                            else
                                strError.Add("تصویر اول افزایش سقف سنی");
                            if (personel_SaghfSeni2 != null && personel_SaghfSeni2.ContentLength > 0) vl.personel_SaghfSeni2 = ConvertToArray(personel_SaghfSeni2);
                            else
                                strError.Add("تصویر دوم افزایش سقف سنی");
                            if (personel_SaghfSeni3 != null && personel_SaghfSeni3.ContentLength > 0) vl.personel_SaghfSeni3 = ConvertToArray(personel_SaghfSeni3);
                            else
                                strError.Add("تصویر سوم افزایش سقف سنی");
                            if (personel_CoronaForm != null && personel_CoronaForm.ContentLength > 0) vl.personel_CoronaForm = ConvertToArray(personel_CoronaForm);
                            else
                                strError.Add("تصویر فرم کرونا");
                            if (personel_OtheDoc1 != null && personel_OtheDoc1.ContentLength > 0) vl.personel_OtheDoc1 = ConvertToArray(personel_OtheDoc1);
                            else
                                strError.Add("تصویر اول سایر مدارک");
                            if (personel_OtheDoc2 != null && personel_OtheDoc2.ContentLength > 0) vl.personel_OtheDoc2 = ConvertToArray(personel_OtheDoc2);
                            else
                                strError.Add("تصویر دوم سایر مدارک");
                            if (personel_OtheDoc3 != null && personel_OtheDoc3.ContentLength > 0) vl.personel_OtheDoc3 = ConvertToArray(personel_OtheDoc3);
                            else
                                strError.Add("تصویر سوم سایر مدارک");

                            string strUlL = "<ul>{0}</ul>";
                            string strLi = "<li>{0}</li>";
                            string strLiList = "";
                            foreach (var st in strError)
                            {
                                string str1 = string.Format(strLi, st);
                                strLiList += str1;
                            }
                            vl.Molahezat = string.Format(strUlL, strLiList);

                            vl.TrakingCode = "";
                            vl.EditCode = 1;
                            vl.EditDate = DateTime.Now;


                            string GenderTitle = vl.Gender == "1" ? "سرکار خانم" : "جناب آقای";
                            string strmessage1 = "{0} {1} {2} به شماره کد ملی {3} و شماره داطلبی {4} و شماره پرونده {5}  مدارک ارسالی شما با موفقیت ثبت شد";
                            string strmessage2 = "{0} {1} {2} به شماره کد ملی {3} و شماره داطلبی {4} و شماره پرونده {5}  مدارک  ارسالی شما با ملاحظات ذیل در سیستم ثبت شد";

                            PDSM.StrMessage = string.Format(strmessage1, "داوطلب گرامی", GenderTitle,vl.Fname + " " + vl.Lname, vl.Melicode, vl.DavNO, vl.PNO);
                            PDSM.TrackingCode = vl.TrakingCode;
                            PDSM.DateSign = vl.EditDate;
                            if (strError.Count() > 0)
                            {
                                PDSM.StrMessage = string.Format(strmessage2, "داوطلب گرامی", GenderTitle, vl.Fname + " " + vl.Lname, vl.Melicode, vl.DavNO, vl.PNO);
                                PDSM.Hasmolahezat = true;
                                PDSM.Molahezat = vl.Molahezat;
                                Session["Molahezat"] = vl.Molahezat;
                            }
                            string strsign = "اینجانب <strong>{0} {1}</strong> با درنظرگرفتن ملاحظات بالا مدارک لازم را ارسال کرده ام.";
                            ViewBag.PossibleSignTextValenter = string.Format(strsign, vl.Fname, vl.Lname);

                            APM.personel_Bomi1 = vl.personel_Bomi1;
                            APM.personel_Bomi2 = vl.personel_Bomi2;
                            APM.personel_Bomi3 = vl.personel_Bomi3;
                            APM.personel_Bomi4 = vl.personel_Bomi4;
                            APM.personel_CoronaForm = vl.personel_CoronaForm;
                            APM.personel_Doctor = vl.personel_Doctor;
                            APM.personel_Gavahinameh_back = vl.personel_Gavahinameh_back;
                            APM.personel_Gavahinameh_on = vl.personel_Gavahinameh_on;
                            APM.personel_GVT = vl.personel_GVT;
                            APM.personel_Isargari1 = vl.personel_Isargari1;
                            APM.personel_Isargari2 = vl.personel_Isargari2;
                            APM.personel_Isargari3 = vl.personel_Isargari3;
                            APM.personel_Kardani = vl.personel_Kardani;
                            APM.personel_Karshenasi = vl.personel_Karshenasi;
                            APM.personel_KarshenasiArshad = vl.personel_KarshenasiArshad;
                            APM.personel_melicode_Back = vl.personel_melicode_Back;
                            APM.personel_melicode_On = vl.personel_melicode_On;
                            APM.personel_Nezamvazifeh1 = vl.personel_Nezamvazifeh1;
                            APM.personel_Nezamvazifeh2 = vl.personel_Nezamvazifeh2;
                            APM.personel_OtheDoc1 = vl.personel_OtheDoc1;
                            APM.personel_OtheDoc2 = vl.personel_OtheDoc2;
                            APM.personel_OtheDoc3 = vl.personel_OtheDoc3;
                            APM.personel_picture = vl.personel_picture;
                            APM.personel_SaghfSeni1 = vl.personel_SaghfSeni1;
                            APM.personel_SaghfSeni2 = vl.personel_SaghfSeni2;
                            APM.personel_SaghfSeni3 = vl.personel_SaghfSeni3;
                            APM.personel_Sanjesh = vl.personel_Sanjesh;
                            APM.personel_Shsh1 = vl.personel_Shsh1;
                            APM.personel_Shsh2 = vl.personel_Shsh2;
                            APM.personel_Shsh3 = vl.personel_Shsh3;

                            PDSM.AddPM = APM;
                          
                            return View(MVC.Sanjesh.Views.BeforeSave, PDSM);


                        }

                    }


                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "هنگام ذخیره داده ها خطایی رخ داده");
            }


            return View(MVC.Sanjesh.Views.UploadPicture, APM);
        }

        public virtual ActionResult SaveDate(PrintDataSanjeshModel model)
        {
            ViewBag.PossibleSignTextValenter = "";
            PrintDataModel PDM = new PrintDataModel();
            AddPictureModel APM = new AddPictureModel();
            Jazb.Servicelayer.EFServices.GenerateUniqCode GUC = new Servicelayer.EFServices.GenerateUniqCode();
            string UnicText = GUC.CheckUniqCode(8);

            try
            {
                long ID=model.AddPM.ID;
              
                    if (ID > 0)
                    {
                        var vl = db.AttachmentImageSanjeshs.Find(ID);
                        

                        if (vl != null)
                        {
                            APM.DavNO = vl.DavNO;
                            APM.Fname = vl.Fname;
                            APM.Lname = vl.Lname;
                            APM.Melicode = vl.Melicode;
                            APM.PNO = vl.PNO;
                            APM.ID = vl.ID;

                            vl.EditDate = DateTime.Now;
                            vl.EditCode = 1;
                            vl.TrakingCode = UnicText;
                            vl.Molahezat = Session["Molahezat"].ToString();
                            vl.personel_picture = model.AddPM.personel_picture;
                            vl.personel_Sanjesh = model.AddPM.personel_Sanjesh;
                            vl.personel_GVT = model.AddPM.personel_GVT;
                            vl.personel_Shsh1 = model.AddPM.personel_Shsh1;
                            vl.personel_Shsh2 = model.AddPM.personel_Shsh2;
                            vl.personel_Shsh3 = model.AddPM.personel_Shsh3;
                            vl.personel_melicode_On = model.AddPM.personel_melicode_On;
                            vl.personel_melicode_Back = model.AddPM.personel_melicode_Back;
                            vl.personel_Kardani = model.AddPM.personel_Kardani;
                            vl.personel_Karshenasi = model.AddPM.personel_Karshenasi;
                            vl.personel_KarshenasiArshad = model.AddPM.personel_KarshenasiArshad;
                            vl.personel_Doctor = model.AddPM.personel_Doctor;
                            vl.personel_Isargari1 = model.AddPM.personel_Isargari1;
                            vl.personel_Isargari2 = model.AddPM.personel_Isargari2;
                            vl.personel_Isargari3 = model.AddPM.personel_Isargari3;
                            vl.personel_Nezamvazifeh1 = model.AddPM.personel_Nezamvazifeh1;
                            vl.personel_Nezamvazifeh2 = model.AddPM.personel_Nezamvazifeh2;
                            vl.personel_Gavahinameh_on = model.AddPM.personel_Gavahinameh_on;
                            vl.personel_Gavahinameh_back = model.AddPM.personel_Gavahinameh_back;
                            vl.personel_Bomi1 = model.AddPM.personel_Bomi1;
                            vl.personel_Bomi2 = model.AddPM.personel_Bomi2;
                            vl.personel_Bomi3 = model.AddPM.personel_Bomi3;
                            vl.personel_Bomi4 = model.AddPM.personel_Bomi4;
                            vl.personel_SaghfSeni1 = model.AddPM.personel_SaghfSeni1;
                            vl.personel_SaghfSeni2 = model.AddPM.personel_SaghfSeni2;
                            vl.personel_SaghfSeni3 = model.AddPM.personel_SaghfSeni3;
                            vl.personel_CoronaForm = model.AddPM.personel_CoronaForm;
                            vl.personel_OtheDoc1 = model.AddPM.personel_OtheDoc1;
                            vl.personel_OtheDoc2 = model.AddPM.personel_OtheDoc2;
                            vl.personel_OtheDoc3 = model.AddPM.personel_OtheDoc3;
                           
                            db.Entry(vl).Property(x => x.personel_Bomi1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Bomi2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Bomi3).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Bomi4).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_CoronaForm).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Doctor).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Gavahinameh_back).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Gavahinameh_on).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_GVT).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Isargari1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Isargari2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Isargari3).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Kardani).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Karshenasi).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_KarshenasiArshad).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_melicode_Back).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_melicode_On).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Nezamvazifeh1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Nezamvazifeh2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_OtheDoc1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_OtheDoc2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_OtheDoc3).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_picture).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_SaghfSeni1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_SaghfSeni2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_SaghfSeni3).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Sanjesh).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Shsh1).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Shsh2).IsModified = true;
                            db.Entry(vl).Property(x => x.personel_Shsh3).IsModified = true;

                            db.Entry(vl).Property(x => x.TrakingCode).IsModified = true;
                            db.Entry(vl).Property(x => x.EditDate).IsModified = true;
                            db.Entry(vl).Property(x => x.EditCode).IsModified = true;
                            db.Entry(vl).Property(x => x.Molahezat).IsModified = true;

                            db.SaveChanges();

                            PDM.DateSign = DateTime.Now.Date;
                            PDM.Hasmolahezat = model.Hasmolahezat;
                            PDM.Molahezat = Session["Molahezat"].ToString();
                            PDM.StrMessage = model.StrMessage;
                            PDM.TrackingCode = UnicText;
                          
                            string strsign = "اینجانب <strong>{0} {1}</strong> با درنظرگرفتن ملاحظات بالا مدارک لازم را ارسال کرده ام.";
                            ViewBag.PossibleSignTextValenter = string.Format(strsign, vl.Fname, vl.Lname);

                            return View(MVC.Sanjesh.Views.PrintDataSanjesh, PDM);
                            
                        }

                    }


                }

            
            catch (Exception ex)
            {
                ModelState.AddModelError("", "هنگام ذخیره داده ها خطایی رخ داده");
            }


            return View(MVC.Sanjesh.Views.Index);
        }


      

    }
}