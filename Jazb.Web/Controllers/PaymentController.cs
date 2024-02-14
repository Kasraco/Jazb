using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model.Payment;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Jazb.Web.Controllers
{
    public partial class PaymentController : Controller
    {

        protected JazbDbContext context = new JazbDbContext();
        //
        // GET: /Payment/
        protected JazbDbContext db = new JazbDbContext();

        private bool CheckPayment(int Vid, int Aid)
        {
            if (Aid == 0 || Vid == 0)
            {
                return false;
            }

            var pay = db.Payments.Where(x => x.AzmoonId == Aid && x.ValentearId == Vid && x.PaymentFinished == true && x.StatusPayment == "OK").ToList();
            if (pay.Count() > 0)
                return true;


            return false;
        }

        public virtual ActionResult PIndex(int AID, int Vid)
        {
            VewPayment vPayment = new VewPayment();
            if (AID == 0 || Vid==0)
            {
                return PartialView(MVC.Valentear.Views._errorPrintData);
            }

            var chPay = CheckPayment(AID, Vid);
            var VList = db.Valentears.Where(x => x.Id == Vid).Select(x => new { x.FirstName, x.LastName, x.Mobile }).ToList();
            Azmoon azmoon = context.Azmoons.Find(AID);
            var VADList = db.AzmoonDevotionValentears.Where(x => x.Valentear.Id == Vid).ToList();
            bool DTU25 = false;
            bool DTU5 = false;
            string AMValue=azmoon.AmountValue;
           foreach(var VAD in VADList)
           {
               var ADList = db.DevotionTypes.Where(x =>  x.DevotionTypeTitle == VAD.DevotionTitle).ToList();
               if (ADList.Count() > 0)
               {
                   var AD = ADList.First();
                   if (AD.Grade > 1 && AD.Grade < 127)
                       DTU5 = true;
                   if (AD.Grade >= 128)
                       DTU25 = true;
               }
           }
            if(DTU5)
            {
                AMValue = (int.Parse(AMValue) / 2).ToString();
            }

            if (DTU25)
            {
                AMValue = "0";
            }
            
            if (VList.Count() > 0)
            {
                var V = VList.First();
                vPayment = new VewPayment
                {
                    AmountValue = AMValue,
                    AzmoonId = azmoon.AzmoonId,
                    AzmoonTitle = azmoon.Title,
                    ValentearID = Vid,
                    EmailAddress = "AjumsBehvarze@ajums.ac.ir",
                    FirstName = V.FirstName,
                    LastName = V.LastName,
                    Mobile = V.Mobile
                };

                TempData["vPayment"] = vPayment;
                return RedirectToAction(MVC.Payment.ActionNames.PShowDataPayment,MVC.Payment.Name );
        }



            TempData["vPayment"] = vPayment;
            return RedirectToAction(MVC.Payment.ActionNames.PShowDataPayment, MVC.Payment.Name);
        }


         //[Authorize(Roles = "admin")]
        public virtual ActionResult Index(int AID)
        {
            VewPayment vPayment=new VewPayment();
            if (AID == 0)
            {
                return PartialView(MVC.Valentear.Views._errorPrintData);
            }
           
                Azmoon azmoon = context.Azmoons.Find(AID);
                vPayment = new VewPayment
                {
                    AmountValue = azmoon.AmountValue,
                    AzmoonId = azmoon.AzmoonId,
                    AzmoonTitle = azmoon.Title,
                    ValentearID = 0
                };

            
            return View(vPayment);
        }

        public virtual ActionResult PShowDataPayment()
        {

            VewPayment vpayment = (VewPayment)TempData["vPayment"];
            if (vpayment == null)
            {
                return RedirectToAction(MVC.Payment.ActionNames.Index, new { AID = vpayment.AzmoonId });
            }

            try
            {

                long AmountVal = long.Parse(vpayment.AmountValue);
                string dargahpardakht = "Saman";
                string paymentIdstring = InsertPayment(AmountVal, dargahpardakht, vpayment.AzmoonId, vpayment.ValentearID,
                                                      vpayment.FirstName, vpayment.LastName, vpayment.Mobile, vpayment.EmailAddress);

                long paymentId = long.Parse(paymentIdstring);
                if (paymentId > 0)
                {
                    ViewBag.AzmoonTitle = vpayment.AzmoonTitle;
                    var Pay = GetPayment(paymentIdstring);
                    return View(MVC.Payment.Views.ShowDataPayment, Pay);
                }



            }
            catch
            {
                ViewBag.Message = "پاسخی از درگاه دریافت نشد";

            }
            return RedirectToAction(MVC.Payment.ActionNames.Index, new { AID = vpayment.AzmoonId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]
        public virtual ActionResult ShowDataPayment(VewPayment vpayment)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(MVC.Payment.ActionNames.Index, new { AID = vpayment.AzmoonId });
            }

            try
            {
               
                long AmountVal = long.Parse(vpayment.AmountValue);
                string dargahpardakht = "Saman";
                string paymentIdstring = InsertPayment(AmountVal, dargahpardakht, vpayment.AzmoonId, vpayment.ValentearID,
                                                      vpayment.FirstName,vpayment.LastName,vpayment.Mobile,vpayment.EmailAddress );

                long paymentId = long.Parse(paymentIdstring);
                if (paymentId > 0)
                {
                    ViewBag.AzmoonTitle = vpayment.AzmoonTitle;
                    var Pay = GetPayment(paymentIdstring);
                    return View(MVC.Payment.Views.ShowDataPayment, Pay);
                }

                    
               
            }
            catch
            {
                ViewBag.Message = "پاسخی از درگاه دریافت نشد";

            }
            return RedirectToAction(MVC.Payment.ActionNames.Index, new { AID = vpayment.AzmoonId });
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]
        public virtual ActionResult SendRequestPayment(Payment Pay)
        {
            // آدرس برگشت از درگاه
            string redirectPage = "http://Jazb.ajums.ac.ir/Payment/ReturnOfBank";

            long paymentId = long.Parse(Pay.PaymentId.ToString());
            if (paymentId > 0)
            {
                SamanPayment(Pay.Amount, redirectPage, Pay.PaymentId.ToString());
            }
            else
            {
                ViewBag.Message = "پاسخی از درگاه دریافت نشد";
                return RedirectToAction(MVC.Payment.ActionNames.Index, MVC.Payment.Name, new { AID = Pay.AzmoonId });
            }

            

            return View(MVC.Payment.Views.ShowDataPayment,Pay);
        }

        #region اکشن برگشت از درگاه
        public virtual ActionResult ReturnOfBank(string paymentId)
        {
            string strState = "";
            ViewBag.VCtransactionState = "No";
            strState = ViewBag.VCtransactionState;

            if (string.IsNullOrEmpty(paymentId))
                paymentId = Request.Form["ResNum"].ToString();

            ViewBag.BankName = "درگاه بانک سامان";
            SamanReturn();
            strState = ViewBag.VCtransactionState;

            var GPay = GetOKPayment(paymentId);

            if(GPay!=null)
            {
                AcceptPaymentModel APM = new AcceptPaymentModel
                {
                    AzmoonId = GPay.AzmoonId,
                    Message = ViewBag.Message,
                    RefrenceNumber = GPay.ReferenceNumber,
                    PaymentId = GPay.PaymentId.ToString(),
                    SaleReferenceId = GPay.SaleReferenceId.ToString(),
                    ValentearID = GPay.ValentearId
                };
                var azm = context.Azmoons.Where(x => x.AzmoonId == APM.AzmoonId);
                if(azm.Count()>0)
                {
                    var az1 = azm.First();
                    if (az1.AzmoonPaymentType == PaymentType.OnlinePayment)
                        Session["KRBKEYVIDBACKOFBANK"] = "0";
                    else if (az1.AzmoonPaymentType == PaymentType.OnlinePaymentGetCard)
                        Session["KRBKEYVIDBACKOFBANK"] = GPay.ValentearId;

                    return View(MVC.Payment.Views.ReturnOfBank, APM);
                  
                }
                  return View(MVC.Payment.Views.ReturnOfBank, APM);
                
            }
            else
            {
                return View(MVC.Payment.Views.DontPayment);
            }
            

         


            return View();
        }
        #endregion

        /********************************************/
        #region ثبت اطلاعات اولیه پرداخت در دیتابیس

        private string InsertPayment(long price, string bankName,int AzmoonId,int valentearId,string FirstName,string LastName,string Mobile,string EmailAddress)
        {
            long paymentId = 0;
            DateTime DT = DateTime.Now;
            string localDate = DT.ToString("yyyyMMdd");
            string localTime = DT.ToString("HHMM");
            bool blnhascode=true;
            string strCod = "";
            //while (blnhascode)
            //{
            //    DT = DateTime.Now;
            //    localDate = DT.ToString("yyyyMMdd");
            //    localTime = DT.ToString("HHMM");
            //    strCod = string.Format("{0}{1}{2}{3}", AzmoonId, localDate, localTime, DT.Second.ToString()); 
            
            //    blnhascode=db.Payments.Any(x=>x.PaymentIdstring==strCod);

            //}
            
            
            try
            {

                var payment = new Payment();

                // قیمت پرداخت
                payment.Amount = price;

                // شماره پیگیری را در ثبت اولیه 0 ثبت می کنیم
                payment.SaleReferenceId = 0;

                // نام بانک انتخاب شده برای پرداخت
                payment.BankName = bankName;

                // فقط در صورتی که این فید ترو باشد پرداخت موفق بوده است
                payment.PaymentFinished = false;

                // آی دی کاربر درحال پرداخت که ما یک در نظر گرفتیم و شما باید آی دی کاربری که پرداخت را انجام می دهد ثبت کنید
                payment.AzmoonId =AzmoonId;
                payment.ValentearId = valentearId;
                payment.FirstName = FirstName;
                payment.LastName = LastName;
                payment.Mobile = Mobile;
                payment.EmailAddress = EmailAddress;

                payment.PaymentIdstring = strCod;
                // ثبت اطلاعات در دیتابیس
                db.Payments.Add(payment);
                db.SaveChanges();

               
               // // شماره پرداخت که همان آی دی جدول می باشد که به بانک ارسال می کنیم و هنگام بازگشت اطلاعات را از دیتابیس پیدا می کنیم
                paymentId = payment.PaymentId; //long.Parse(strCod);

            }
            catch (Exception ex)
            {
            }

            return paymentId.ToString();//paymentId;
        }
        #endregion

        #region متد ویرایش پرداخت

        private void UpdatePayment(string paymentIdString, string vresult, long saleReferenceId, string refId, bool paymentFinished = false)
        {
            int pid=int.Parse(paymentIdString);
            var ps = db.Payments.Where(x => x.PaymentId == pid).ToList();
            if(ps.Count()>0)
            {
               // var payment = db.Payments.Find(paymentId);
                var payment = ps.First();

                if (payment != null)
                {
                    payment.StatusPayment = vresult;
                    payment.SaleReferenceId = saleReferenceId;
                    payment.PaymentFinished = paymentFinished;

                    if (refId != null)
                    {
                        payment.ReferenceNumber = refId;
                    }

                    db.Entry(payment).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    // اطلاعاتی از دیتابیس پیدا نشد
                }

            }
         
        }
        #endregion

        #region پیدا کردن مبلغ خرید
        private long FindAmountPayment(string paymentIdString)
        {
            long amount = 0;
            int pid = int.Parse(paymentIdString);
            var ps = db.Payments.Where(x => x.PaymentId == pid).ToList();
            if (ps.Count() > 0)
            {
                // var payment = db.Payments.Find(paymentId);
                var payment = ps.First();
                amount = payment.Amount;
            }
          

            return amount;
        }
        private Payment GetPayment(string paymentIdString)
        {
            long amount = 0;
            Payment P = new Payment();
            int PID=int.Parse(paymentIdString);
            var ps = db.Payments.Where(x => x.PaymentId == PID).ToList();
            if (ps.Count() > 0)
            {
                // var payment = db.Payments.Find(paymentId);
                P = ps.First();
            }
          

            return P;
        }


        private Payment GetOKPayment(string paymentIdString)
        {
            long amount = 0;
            Payment P = new Payment();
            int pid = int.Parse(paymentIdString);
            var ps = db.Payments.Where(x => x.PaymentId == pid && x.PaymentFinished==true && x.StatusPayment=="OK").ToList();
            if (ps.Count() > 0)
            {
                // var payment = db.Payments.Find(paymentId);
                P = ps.First();
            }


            return P;
        }
        #endregion

        /********************************************/
        #region متد پرداخت از درگاه سامان
        private void SamanPayment(long price, string redirectPage, string paymentIdString)
        {
            try
            {
                // شماره ترمینال
                string termId = "123456";

                // ایجاد یک شی از 
                //SepInitPayment
                //برای پرداخت
                var initPayment = new BankSaman.PaymentIFBinding();
                
                // ارسال اطلاعات به درگاه بانک به روش توکن
                // 1- شماره ترمینال
                // 2- شماره پرداخت
                // 3- مبلغ
                // 5- مبلغ
                // 6-مبلغ
                // 7-مبلغ
                // 8-مبلغ
                // 9-مبلغ
                // 10- اطلاعات اضافی
                // 11- اطلاعات اضافی
                // 12- 
                string token = initPayment.RequestToken(termId, paymentIdString, long.Parse(price.ToString()), 0, 0, 0, 0, 0, 0, "", "", 0);

                // بررسی وجود توکن ارسال شده از درگاه بانک
                if (!String.IsNullOrEmpty(token))
                {
                    if (Infrastructure.PaymentResult.Saman(token) == "خطای نا مشخص")
                    {
                      
                        // اگر عدد برگشت مانند -18 آی پی بسته است و.. به کلاس 
                        //SepResult
                        //مراجعه شود

                        // ایجاد یک شی از نیم ولیو کالکشن
                        NameValueCollection datacollection = new NameValueCollection();

                        // اضافه کردن توکن به شی ساخت شده از نیم ولیو کالکشن
                        datacollection.Add("Token", token);

                        // اضافه کردن آدرس برگشت از درگاه در به شی ساخت شده از نیم ولیو کالکشن
                        datacollection.Add("RedirectURL", redirectPage);

                        // ارسال اطلاعات به درگاه
                        Response.Write(Helpers.HttpHelper.PreparePOSTForm("https://sep.shaparak.ir/payment.aspx", datacollection));
                    }
                    else
                    {
                        // فرا خوانی متد آپدیت پی منت برای ویرایش اطلاعات ارسالی از درگاه در صورت عدم اتصال
                        UpdatePayment(paymentIdString, token, 0, null, false);

                        // نمایش خطا به کاربر
                        ViewBag.Message = Infrastructure.PaymentResult.Saman(token);
                    }
                }
                else
                {
                    // فرا خوانی متد آپدیت پی منت برای ویرایش اطلاعات ارسالی از درگاه در صورت عدم اتصال
                    UpdatePayment(paymentIdString, token, 0, null, false);

                    // نمایش خطا به کاربر
                    ViewBag.Message = Infrastructure.PaymentResult.Saman(token);
                }
            }
            catch (Exception ex)
            {
                ViewBag.message = "در حال حاظر امکان اتصال به این درگاه وجود ندارد ";
            }
        }
        #endregion

        #region متد برگشت از درگاه سامان
        private void SamanReturn()
        {
            try
            {
                ViewBag.VCtransactionState = "No";
                // بررسی وجود استیت ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                if (Request.Form["state"].ToString().Equals(string.Empty))
                {
                    //ViewBag.Message = "خريد شما توسط بانک تاييد شده است اما رسيد ديجيتالي شما تاييد نگشت! مشکلي در فرايند رزرو خريد شما پيش آمده است";
                    ViewBag.Message = "پاسخی از درگاه بانکی دریافت نشد";
                    ViewBag.SaleReferenceId = "**************";
                    ViewBag.Image = "~/Images/notaccept.png";
                    ViewBag.VCtransactionState = "No";
                }

                // بررسی وجود رف نام ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                else if (Request.Form["RefNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
                {
                    ViewBag.Message = "فرايند انتقال وجه با موفقيت انجام شده است اما فرايند تاييد رسيد ديجيتالي با خطا مواجه گشت";
                    ViewBag.SaleReferenceId = "**************";
                    ViewBag.Image = "~/Images/notaccept.png";
                    ViewBag.VCtransactionState = "No";
                }

                // بررسی وجود رس نام ارسالی از درگاه
                // در صورت عدم وجود خطا را نمایش می دهیم
                else if (Request.Form["ResNum"].ToString().Equals(string.Empty) && Request.Form["state"].ToString().Equals(string.Empty))
                {
                    ViewBag.Message = "خطا در برقرار ارتباط با بانک";
                    ViewBag.SaleReferenceId = "**************";
                    ViewBag.Image = "~/Images/notaccept.png";
                    ViewBag.VCtransactionState = "No";
                }
                else
                {
                    // تغییر های مورد تعریف شده برای قرار دادن اطلاعات دریافتی از درگاه
                    string refrenceNumber = string.Empty;
                    string reservationNumber = string.Empty;
                    string transactionState = string.Empty;
                    string traceNumber = string.Empty;
                    string SecurePan = string.Empty;
                    string RRN = string.Empty;


                    // کد سفارش که به صورت عدد و حروف می باشد
                    refrenceNumber = Request.Form["RefNum"].ToString();

                    // کد ارسالی از طرف سایت که شماره آی دی همان اطلاعات ثبتی در هنگام اتصال به درگاه
                    reservationNumber = Request.Form["ResNum"].ToString();

                    // وضعیت پرداخت
                    transactionState = Request.Form["state"].ToString();

                    // شماره پیگیری
                    traceNumber = Request.Form["TraceNo"].ToString();

                    // شماره کارت
                    SecurePan = Request.Form["SecurePan"].ToString();
                    RRN = Request.Form["RRN"].ToString();

                    string paymentIdString = reservationNumber;
                    ViewBag.VCtransactionState = transactionState;
                    if (transactionState.Equals("OK"))
                    {
                        ///////////////////////////////////////////////////////////////////////////////////
                        //   *** IMPORTANT  ****   ATTENTION
                        // Here you should check refrenceNumber in your DataBase tp prevent double spending
                        ///////////////////////////////////////////////////////////////////////////////////

                        // جلوگیری از خطای اس اس ال
                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        // ایجاد یک شی از 
                        //PaymentIFBindingSoapClient
                        //برای پرداخت
                        var srv = new BankSamanVerify.PaymentIFBinding();

                        // تایید اطلاعات پرداخت و دریافت نتیجه
                        var result = srv.verifyTransaction(Request.Form["RefNum"], Request.Form["MID"]);

                        // بررسی نتیجه برگشتی از درگاه
                        if (result > 0)
                        {
                            // پیدا کردن مبلغ پرداختی از درگاه
                            long amount = FindAmountPayment(paymentIdString);

                            // تبدیل مبلغ به ریال
                            //amount = amount * 10;

                            // چک کردن مبلغ بازگشتی از سرویس با مبلغ تراکنش
                            if ((long)result == amount)
                            {
                                // در اینجا خرید موفق بوده و اطلاعات دریافتی از درگاه را در دیتابیس ذخیره می کنیم
                                UpdatePayment(paymentIdString, transactionState, Convert.ToInt64(traceNumber), refrenceNumber, true);

                                // قرار دادن اطلاعات پرداخت در ویوبگ ها
                                //ViewBag.Message = "بانک صحت رسيد ديجيتالي شما را تصديق نمود. فرايند خريد تکميل گشت";
                                ViewBag.Message = "پرداخت با موفقیت انجام شد.";
                                ViewBag.SaleReferenceId = traceNumber;
                                ViewBag.RefrenceNumber = refrenceNumber;
                                ViewBag.Image = "~/Images/accept.png";
                            }
                            // عدم یکسان بودن مبلغ پرداختی با مبلغ موجود در دیتابیس
                            else
                            {
                                //نام کاربری همان ام آی دی است
                                string userName = Request.Form["MID"];

                                // رمز عبور برای شما توسط سامان کیش ایمیل شده است
                                string pass = "4495598";

                                // فراخوانی متد ریورس ترنزاکشن برای بازگشت دادن مبلغ به حساب خریدار
                                srv.reverseTransaction(Request.Form["RefNum"], Request.Form["MID"], userName, pass);

                                // پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                                UpdatePayment(paymentIdString, transactionState, 0, refrenceNumber, false);

                                // نمایش اطلاعات خرید و وضعیت خرید به خریدار
                                ViewBag.Message = Infrastructure.PaymentResult.Saman(transactionState);
                                ViewBag.SaleReferenceId = "**************";
                                ViewBag.Image = "~/Images/notaccept.png";

                            }
                        }
                        // بعد از وریفای کردن خرید نتیجه بزرگتر از صفر نبود این قسمت اجرا می شود
                        else
                        {
                            // پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                            UpdatePayment(paymentIdString, result.ToString(), 0, refrenceNumber, false);

                            // نمایش اطلاعات خرید و وضعیت خرید به خریدار
                            ViewBag.Message = Infrastructure.PaymentResult.Saman(transactionState);
                            ViewBag.SaleReferenceId = "**************";
                            ViewBag.Image = "~/Images/notaccept.png";
                        }
                    }
                    // در صورتی که 
                    // transactionState
                    // برابر 
                    // ok
                    // نبود این قسمت اجرا می شود
                    else
                    {
                        // پرداخت ناموفق بوده و اطلاعات دریافتی را در دیتابیس ثبت می کنیم
                        UpdatePayment(paymentIdString, transactionState, 0, refrenceNumber, false);

                        if (!String.IsNullOrEmpty(Infrastructure.PaymentResult.Saman(transactionState)))
                        {
                            ViewBag.Message = Infrastructure.PaymentResult.Saman(transactionState);
                        }
                        else
                        {
                            ViewBag.Message = "متاسفانه بانک خريد شما را تاييد نکرده است";
                        }
                        ViewBag.SaleReferenceId = "**************";
                        ViewBag.Image = "~/Images/notaccept.png";

                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.SaleReferenceId = "**************";
                ViewBag.Image = "~/Images/notaccept.png";
                ViewBag.Message = "مشکلی در پرداخت به وجود آمده است ، در صورتیکه وجه پرداختی از حساب بانکی شما کسر شده است آن مبلغ به صورت خودکار برگشت داده خواهد شد";
            }
        }
        #endregion


        /********************************************/
        #region دیس پوز کردن کانکشن
        /// <summary>
        /// متد اورراید دیسپوز که در صورتی که پارامتر ورودی دیسپوز نشده باشد آنرا دیسپوز میکند
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion


    }
}
