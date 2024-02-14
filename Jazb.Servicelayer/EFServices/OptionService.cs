using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model;
using Jazb.Model.AdminModel;
using Jazb.Servicelayer.Interfaces;

namespace Jazb.Servicelayer.EFServices
{
    public class OptionService : IOptionService
    {
        private readonly IDbSet<Option> _options;
        private IUnitOfWork _uow;

        public OptionService(IUnitOfWork uow)
        {
            _uow = uow;
            _options = _uow.Set<Option>();
        }

        public void Update(UpdateOptionModel model)
        {
            
            List<Option> options = _options.ToList();
            options.Where(op => op.Name.Equals("SiteUrl")).FirstOrDefault().Value = model.SiteUrl;
            options.Where(op => op.Name.Equals("CompanyAddress")).FirstOrDefault().Value = model.CompanyAddress;
            options.Where(op => op.Name.Equals("CompanyLogo")).FirstOrDefault().Value = model.CompanyLogo;
            options.Where(op => op.Name.Equals("SiteName")).FirstOrDefault().Value = model.BlogName;
            options.Where(op => op.Name.Equals("SiteKeywords")).FirstOrDefault().Value = model.BlogKeywords;
            options.Where(op => op.Name.Equals("SiteDescription")).FirstOrDefault().Value = model.BlogDescription;
            options.Where(op => op.Name.Equals("UsersCanRegister")).FirstOrDefault().Value =
                model.UsersCanRegister.ToString();
            options.Where(op => op.Name.Equals("AdminEmail")).FirstOrDefault().Value = model.AdminEmail;        
            options.Where(op => op.Name.Equals("MailServerUrl")).FirstOrDefault().Value = model.MailServerUrl;
            options.Where(op => op.Name.Equals("MailServerLogin")).FirstOrDefault().Value = model.MailServerLogin;
            options.Where(op => op.Name.Equals("MailServerPass")).FirstOrDefault().Value = model.MailServerPass;
            options.Where(op => op.Name.Equals("MailServerPort")).FirstOrDefault().Value =
                model.MailServerPort.ToString();
         
        }

        public SiteConfig GetAll()
        {
            List<Option> options = _options.ToList();
            var model = new SiteConfig
            {
                CompanyAddress = options.Where(op => op.Name.Equals("CompanyAddress")).FirstOrDefault().Value,
                CompanyLogo = options.Where(op => op.Name.Equals("CompanyLogo")).FirstOrDefault().Value,
                AdminEmail = options.Where(op => op.Name.Equals("AdminEmail")).FirstOrDefault().Value,
                BlogDescription = options.Where(op => op.Name.Equals("SiteDescription")).FirstOrDefault().Value,
                BlogKeywords = options.Where(op => op.Name.Equals("SiteKeywords")).FirstOrDefault().Value,
                BlogName = options.Where(op => op.Name.Equals("SiteName")).FirstOrDefault().Value,             
                MailServerLogin = options.Where(op => op.Name.Equals("MailServerLogin")).FirstOrDefault().Value,
                MailServerPass = options.Where(op => op.Name.Equals("MailServerPass")).FirstOrDefault().Value,
                MailServerPort =
                    Convert.ToInt32(options.Where(op => op.Name.Equals("MailServerPort")).FirstOrDefault().Value),
                MailServerUrl = options.Where(op => op.Name.Equals("MailServerUrl")).FirstOrDefault().Value,              
                SiteUrl = options.Where(op => op.Name.Equals("SiteUrl")).FirstOrDefault().Value,
                UsersCanRegister = Convert.ToBoolean(options.Where(op => op.Name.Equals("UsersCanRegister"))
                    .FirstOrDefault().Value)
            };
            return model;
        }


        public bool ModeratingComment
        {
            get
            {
                return Convert.ToBoolean(_options.Where(option => option.Name == "CommentModeratingStatus")
                    .FirstOrDefault().Value);
            }
        }
    }
}