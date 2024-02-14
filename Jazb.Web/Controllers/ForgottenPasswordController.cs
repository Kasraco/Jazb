using System;
using System.Globalization;
using System.Web.Mvc;
using CaptchaMvc.Attributes;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model;
using Jazb.Model.EmailModel;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.DateAndTime;
using Jazb.Utilities.Mail;
using Jazb.Utilities.Security;
using Jazb.Web.Caching;
using Jazb.Web.Email;
using Jazb.Web.Infrastructure;

namespace Jazb.Web.Controllers
{
    public partial class ForgottenPasswordController : Controller
    {
        private readonly IForgottenPasswordService _forgttenPasswordService;
        private readonly IOptionService _optionService;
        private readonly IUnitOfWork _uow;
        private readonly IUserService _userService;

        public ForgottenPasswordController(IUnitOfWork uow, IUserService userService,
            IForgottenPasswordService forgottenPasswordService, IOptionService optionService)
        {
            _uow = uow;
            _userService = userService;
            _forgttenPasswordService = forgottenPasswordService;
            _optionService = optionService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return PartialView(MVC.ForgottenPassword.Views._Index);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaVerify("تصویر امنیتی وارد شده معتبر نیست")]
        public virtual ActionResult Index(ForgottenPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(MVC.ForgottenPassword.Views._Index, model);
            }

            bool isEmailExist = _userService.ExistsByEmail(model.Email);

            if (isEmailExist)
            {
                User selecteduser = _userService.GetUserByEmail(model.Email);
                string key = Guid.NewGuid().ToString();
                var newRequestTicket = new ForgottenPassword
                {
                    User = selecteduser,
                    Key = key,
                    ResetDateTime = DateAndTime.GetDateTime()
                };

                _forgttenPasswordService.Add(newRequestTicket);

                if (
                    SendResetPasswordConfirmation(selecteduser.UserName, model.Email, key,
                        "تایید کردن بازنشانی کلمه عبور") == SendingMailResult.Successful)
                {
                    _uow.SaveChanges();
                }
                else
                {
                    return Json(new
                    {
                        result = "true",
                        message = "متاسفانه خطایی در ارسال ایمیل رخ داده است."
                    });
                }

                return Json(new
                {
                    result = "true",
                    message = "ایمیلی برای تایید بازنشانی کلمه عبور برای شما ارسال شد.اعتبارایمیل ارسالی 24 ساعت است."
                });
            }

            return Json(new
            {
                result = "false",
                message = "این ایمیل در سیستم ثبت نشده است"
            });
        }

        private SendingMailResult SendResetPasswordConfirmation(string userName, string email, string guid,
            string subject)
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            var model = new ResetPasswordModel
            {
                UserName = userName,
                ResetLink =
                    string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority,
                        Url.Action(MVC.ForgottenPassword.ActionNames.ResetPassword, MVC.ForgottenPassword.Name,
                            new { key = guid })),
                SiteDescription = siteConfig.BlogDescription,
                SiteTitle = siteConfig.BlogName
            };


            return EmailService.Send(new MailDocument
            {
                Body =
                    new ViewConvertor().RenderRazorViewToString(
                        MVC.EmailTemplates.Views._ResetPasswordConfirmation, ControllerContext, model),
                Subject = string.Format("{0} - {1} - {2}", siteConfig.SiteUrl, siteConfig.BlogName, subject),
                ToEmail = email,
            }, new MailConfiguration
            {
                EnableSsl = true,
                SmtpServer = siteConfig.MailServerUrl,
                From = siteConfig.AdminEmail,
                Password = siteConfig.MailServerPass,
                UserName = siteConfig.MailServerLogin,
                SmtpPort = Convert.ToInt32(siteConfig.MailServerPort)
            });
        }

        private SendingMailResult SendNewPassword(string userName, string email, string newPassword, string subject)
        {
            SiteConfig siteConfig = JazbCache.GetSiteConfig(HttpContext, _optionService);

            var model = new NewPasswordModel
            {
                UserName = userName,
                NewPassword = newPassword,
                SiteDescription = siteConfig.BlogDescription,
                SiteTitle = siteConfig.BlogName
            };

            return EmailService.Send(new MailDocument
            {
                Body =
                    new ViewConvertor().RenderRazorViewToString(
                        MVC.EmailTemplates.Views._NewPassword, ControllerContext, model),
                Subject = string.Format("{0} - {1} - {2}", siteConfig.SiteUrl, siteConfig.BlogName, subject),
                ToEmail = email,
            }, new MailConfiguration
            {
                EnableSsl = true,
                SmtpServer = siteConfig.MailServerUrl,
                From = siteConfig.AdminEmail,
                Password = siteConfig.MailServerPass,
                UserName = siteConfig.MailServerLogin,
                SmtpPort = Convert.ToInt32(siteConfig.MailServerPort)
            });
        }

        public virtual ActionResult ResetPassword(string key)
        {
            if (_forgttenPasswordService.RequestDate(key).Subtract(DateAndTime.GetDateTime()).TotalDays > 1)
            {
                return View();
            }
            string newPass = new Random().Next(500000, 10000000).ToString(CultureInfo.InvariantCulture);
            User selectedUser = _forgttenPasswordService.FindUser(key);
            _forgttenPasswordService.Remove(key);
            selectedUser.Password = Encryption.EncryptingPassword(newPass);
            selectedUser.LastPasswordChange = DateAndTime.GetDateTime();

            if (SendNewPassword(selectedUser.UserName, selectedUser.Email, newPass, "کلمه عبور جدید") ==
                SendingMailResult.Successful)
            {
                _uow.SaveChanges();
            }
            else
            {
                ViewBag.Message = "متاسفانه خطایی در ارسال ایمیل رخ داده است.";
                return View();
            }

            ViewBag.Message = "کلمه عبور شما با موفقیت باز نشانی شد و به ایمیل شما ارسال گردید";

            return View();
        }
    }
}