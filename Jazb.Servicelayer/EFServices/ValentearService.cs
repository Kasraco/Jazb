using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Model;
using Jazb.Model.AdminModel;
using Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob;
using Jazb.Model.Report;
using Jazb.Model.ValentearModel;
using Jazb.Servicelayer.EFServices.Enums;
using Jazb.Servicelayer.Interfaces;
using Jazb.Utilities.DateAndTime;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.EFServices
{
    public class ValentearService : IValentearService 
    {
        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Valentear> _valentear;
        private readonly IDbSet<Job> _job;
        private readonly IDbSet<Place> _place;
        private readonly IDbSet<Gender> _gender;
        private readonly IDbSet<AzmoonJob> _AzmoonJob;
        private readonly IDbSet<Option> _options;

        public ValentearService(IUnitOfWork uow)
        {
            _uow = uow;
            _valentear = _uow.Set<Valentear>();
            _job = _uow.Set<Job>();
            _place = _uow.Set<Place>();
            _gender = _uow.Set<Gender>();
            _AzmoonJob = _uow.Set<AzmoonJob>();
            _options = _uow.Set<Option>();
            _searchTakeCount = 10;

        }
        public int Count
        {
            get { return _valentear.Count(); }
        }

        public int CountValentear(int AzmoonId)
        {
            return _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId).Count();
        }
   

        public IList<string> SearchByNationalId(string nationalId)
        {
            return
                 _valentear.Where(v => v.NationalID.Contains(nationalId))
                     .Take(_searchTakeCount)
                     .Select(v => v.NationalID)
                     .ToList();
        }

        public IList<string> SearchByBirthCertificateNo(string birthCertificateNo)
        {
            return
                 _valentear.Where(v => v.BirthCertificateNo.Contains(birthCertificateNo))
                     .Take(_searchTakeCount)
                     .Select(v => v.BirthCertificateNo)
                     .ToList();
        }

        public IList<string> SearchByFirstName(string firstName)
        {
            return
                _valentear.Where(v => v.FirstName.Contains(firstName))
                    .Take(_searchTakeCount)
                    .Select(v => v.FirstName)
                    .ToList();
        }

        public IList<string> SearchByLastName(string lastName)
        {
            return
                   _valentear.Where(v => v.LastName.Contains(lastName))
                       .Take(_searchTakeCount)
                       .Select(v => v.LastName)
                       .ToList();
        }

        public IList<string> SearchByFatherName(string fatherName)
        {

            return
                   _valentear.Where(v => v.FatherName.Contains(fatherName))
                       .Take(_searchTakeCount)
                       .Select(v => v.FatherName)
                       .ToList();
        }


        public IList<Valentear> GetAllValentearDataTable(string term, int page, int count, ValentearSearchBy searchBy)
        {
            IQueryable<Valentear> selectedValentear = _valentear.Include(x => x.AzmoonDevotionValentearas).Include(x => x.AzmoonQoutasValentears).AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ValentearSearchBy.BirthCertificateNo:
                        selectedValentear = selectedValentear.Where(x => x.BirthCertificateNo.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FatherName:
                        selectedValentear = selectedValentear.Where(x => x.FatherName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FirstName:
                        selectedValentear = selectedValentear.Where(x => x.FirstName.Contains(term)).AsQueryable();
                        break;

                    case ValentearSearchBy.LastName:
                        selectedValentear = selectedValentear.Where(x => x.LastName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.Melicode:
                        selectedValentear = selectedValentear.Where(x => x.NationalID.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.City:
                        {
                            int pid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.PlaceId.Equals(pid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Job:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.JobId.Equals(jid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Accept:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.Accept.Equals(jid)).AsQueryable();
                            break;
                        }
                }
            }

            selectedValentear = selectedValentear.OrderBy(x => x.PlaceId).AsQueryable();

            return selectedValentear.Skip(page * count).Take(count).ToList();
        }




        public IList<Valentear> GetAllValentearDataTable(int AzmoonId, string term, int page, int count, ValentearSearchBy searchBy)
        {
            IQueryable<Valentear> selectedValentear = _valentear.Include(x => x.AzmoonDevotionValentearas)
                                                                .Include(x => x.AzmoonQoutasValentears)
                                                                .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                                                                .AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ValentearSearchBy.BirthCertificateNo:
                        selectedValentear = selectedValentear.Where(x => x.BirthCertificateNo.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FatherName:
                        selectedValentear = selectedValentear.Where(x => x.FatherName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FirstName:
                        selectedValentear = selectedValentear.Where(x => x.FirstName.Contains(term)).AsQueryable();
                        break;

                    case ValentearSearchBy.LastName:
                        selectedValentear = selectedValentear.Where(x => x.LastName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.Melicode:
                        selectedValentear = selectedValentear.Where(x => x.NationalID.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.City:
                        {
                            int pid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.PlaceId.Equals(pid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Job:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.JobId.Equals(jid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Accept:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.Accept.Equals(jid)).AsQueryable();
                            break;
                        }
                }
            }

            selectedValentear = selectedValentear.OrderBy(x => x.PlaceId).AsQueryable();

            return selectedValentear.Skip(page * count).Take(count).ToList();
        }
        public int GetAllValentearDataTable(int AzmoonId, string term, ValentearSearchBy searchBy)
        {
            IQueryable<Valentear> selectedValentear = _valentear.Include(x => x.AzmoonDevotionValentearas)
                                                                .Include(x => x.AzmoonQoutasValentears)
                                                                .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                                                                .AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                switch (searchBy)
                {
                    case ValentearSearchBy.BirthCertificateNo:
                        selectedValentear = selectedValentear.Where(x => x.BirthCertificateNo.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FatherName:
                        selectedValentear = selectedValentear.Where(x => x.FatherName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.FirstName:
                        selectedValentear = selectedValentear.Where(x => x.FirstName.Contains(term)).AsQueryable();
                        break;

                    case ValentearSearchBy.LastName:
                        selectedValentear = selectedValentear.Where(x => x.LastName.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.Melicode:
                        selectedValentear = selectedValentear.Where(x => x.NationalID.Contains(term)).AsQueryable();
                        break;
                    case ValentearSearchBy.City:
                        {
                            int pid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.PlaceId.Equals(pid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Job:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.JobId.Equals(jid)).AsQueryable();
                            break;
                        }
                    case ValentearSearchBy.Accept:
                        {
                            int jid = int.Parse(term);
                            selectedValentear = selectedValentear.Where(x => x.Accept.Equals(jid)).AsQueryable();
                            break;
                        }
                }
            }

            selectedValentear = selectedValentear.OrderBy(x => x.PlaceId).AsQueryable();

            return selectedValentear.Count();
        }





        public ReportSplitValentear CountGenderByJobPlace(int AzmoonId, int JobId, long PlaceId, int BlockState, int ActionType)
        {
            var v = _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                         x.JobId == JobId &&
                                         x.PlaceId == PlaceId &&
                                         x.RecordBlocked == BlockState &&
                                         x.ActionTypeId == ActionType)
                                         .Select(x => new { x.GenderId, x.Id }).AsEnumerable();
            var qResult = new Jazb.Model.AdminModel.ReportSplitValentear
            {
                CountMan = v.Count(x => x.GenderId == 2),
                CountWoman = v.Count(x => x.GenderId == 1),
                TotlaCount = v.Count()
            };
            return qResult;
        }


        public IList<Valentear> GetBlockedValentears(int AzmoonId)
        {
            return _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.RecordBlocked == 1).ToList();

        }

        public IList<Valentear> GetValentears(int AzmoonId)
        {
            return _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
        }

        public IList<Valentear> GetValentears(int AzmoonId, int JobId, long PlaceId, int BlockState, int ActionType)
        {
            return _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                         x.JobId == JobId &&
                                         x.PlaceId == PlaceId &&
                                         x.RecordBlocked == BlockState &&
                                         x.ActionTypeId == ActionType
                                   ).ToList();
        }

        public IList<ValentearReportModel> GetAllValentear(int azmoonId)
        {
            return (from x in _valentear.Include(x=>x.Azmoon).Where(x=>x.Azmoon.AzmoonId==azmoonId)
                    join j in _job on x.JobId equals j.JobCode
                    join p in _place on x.PlaceId equals p.PlaceCode
                    join G in _gender on x.GenderId equals G.GenderCode
                    select new ValentearReportModel()
                    {
                        BirthCertificateNo = x.BirthCertificateNo,
                        FatherName = x.FatherName,
                        FirstName = x.FirstName,
                        GenderName = G.GenderTitle,
                        JobName = j.JobTitle,
                        LastName = x.LastName,
                        MaxDevotionType = x.MaxDevotionType,
                        MaxQoutaType = x.MaxQoutaType,
                        Mobile = x.Mobile,
                        NationalID = x.NationalID,
                        PlaceName = p.PlaceTitle,
                        TextDevotionType = x.TextDevotionType,
                        TextQoutaType = x.TextQoutaType

                    }).AsNoTracking().ToList();


        }

       

        public IList<ValentearCardReportModel> GetAllCardValentear(int azmoonId)
        {    
            var option = GetAllOption();

            return (from x in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == azmoonId && x.Accept == 1 ).AsNoTracking()
                            //join j in _job on x.JobId equals j.JobCode
                            join p in _place on x.PlaceId equals p.PlaceCode
                            join G in _gender on x.GenderId equals G.GenderCode
                            join aj in _AzmoonJob.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == azmoonId) on x.JobId equals aj.AzmoonJobCode
                            select (new ValentearCardReportEnModel()
                            {
                                BirthCertificateNo = x.BirthCertificateNo,
                                FatherName = x.FatherName,
                                FirstName = x.FirstName,
                                GenderName = G.GenderTitle,
                                JobName = aj.AzmoonJobTitle,
                                LastName = x.LastName,
                                NationalID = x.NationalID,
                                PlaceName = p.PlaceTitle,
                                TextDevotionType = x.TextDevotionType,
                                TextQoutaType = x.TextQoutaType,
                               // AreaExecuteAzmoon = x.GenderId == 1 ? aj.RegionExamWoman : aj.RegionExam,
                                AreaExecuteAzmoon = x.AreaExecuteAzmoon,
                                
                                ChairCode = x.ChairCode,
                                 DateExecuteAzmoon=x.Azmoon.DateExecuteAzmoon,
                                InputCardTitle=x.Azmoon.InputCardTitle,
                                LocationExecuteAzmoon=x.Azmoon.LocationExecuteAzmoon,
                                TimeExecute = x.Azmoon.TimeExecuteAzmoon,
                                ValentearPicture = x.PictureValenteer,

                                // AreaExecuteAzmoon="",
                                CompanyLogo = option.CompanyLogo,
                                CompanyName = option.BlogName

                            })).AsNoTracking().AsEnumerable()
                            .Select(x => new ValentearCardReportModel()
                            {
                                BirthCertificateNo = x.BirthCertificateNo,                                
                                FatherName = x.FatherName,
                                FirstName = x.FirstName,
                                GenderName = x.GenderName,
                                JobName = x.JobName,
                                LastName = x.LastName,
                                NationalID = x.NationalID,
                                PlaceName = x.PlaceName,
                                TextDevotionType = x.TextDevotionType,
                                TextQoutaType = x.TextQoutaType,
                                ChairCode = x.ChairCode,
                                ValentearPicture = x.ValentearPicture,
                                CompanyLogo = option.CompanyLogo,
                                CompanyName = option.BlogName,
                                InputCardTitle =string.Format(x.InputCardTitle, x.DateExecuteAzmoon.ToPersianDate("")),
                                LocationExecuteAzmoon=string.Format("{0} - {1}", x.LocationExecuteAzmoon, x.AreaExecuteAzmoon),
                                TimeExecute = string.Format(x.TimeExecute, x.DateExecuteAzmoon.ToPersianDate(""))
                            }).Distinct() .ToList();
           


        }

        public IList<ValentearChairNumberReportModel> GetAllChairNumberValentear(int azmoonId)
        {
            var option = GetAllOption();           
            return (from x in _valentear.Include(x => x.Azmoon)
                                        .Where(x => x.Azmoon.AzmoonId == azmoonId && x.Accept == 1)
                                        .Select(x => new { x.FirstName,x.LastName,x.ChairCode,x.JobId,x.Azmoon,x.PictureValenteer})
                                        .AsNoTracking().AsQueryable()
                    join j in _job on x.JobId equals j.JobCode
                    select new ValentearChairNumberReportModel()
                    {
                        FirstName = x.FirstName,
                        JobName = j.JobTitle,
                        LastName = x.LastName,
                        ChairCode = x.ChairCode,
                        InputCardTitle = x.Azmoon.InputCardTitle,
                        ValentearPicture = x.PictureValenteer,
                        CompanyName = option.BlogName
                    }).Distinct().ToList();
        }

        public IList<ValentearReportModel> GetDevotions(int azmoonId)
        {
            return (from x in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == azmoonId && x.MaxDevotionType > 0)
                    join j in _job on x.JobId equals j.JobCode
                    join p in _place on x.PlaceId equals p.PlaceCode
                    join G in _gender on x.GenderId equals G.GenderCode
                    select new ValentearReportModel()
                    {
                        BirthCertificateNo = x.BirthCertificateNo,
                        FatherName = x.FatherName,
                        FirstName = x.FirstName,
                        GenderName = G.GenderTitle,
                        JobName = j.JobTitle,
                        LastName = x.LastName,
                        MaxDevotionType = x.MaxDevotionType,
                        MaxQoutaType = x.MaxQoutaType,
                        Mobile = x.Mobile,
                        NationalID = x.NationalID,
                        PlaceName = p.PlaceTitle,
                        TextDevotionType = x.TextDevotionType,
                        TextQoutaType = x.TextQoutaType

                    }).AsNoTracking().ToList();


        }

      


        public int SetChairNumber(int AzmoonId)
        {
            var qAJ = _AzmoonJob.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();

            foreach (var taj in qAJ)
            {
                CreateNumber(AzmoonId, taj);
            }
            return 0;
        }

        public void CreateNumber(int AzmoonId, AzmoonJob azmoonJob)
        {
            int StartNumberMan = 0;
            int StartNumberWoman = 0;
            int Index = 0;

            StartNumberMan = azmoonJob.ManFrom;
            StartNumberWoman = azmoonJob.WomanFrom;

            var VManlist = _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.GenderId == 2 && x.JobId == azmoonJob.AzmoonJobCode && x.Accept == 1).OrderBy(x=>x.DegreeId).AsQueryable().ToList();
            var VWomanlist = _valentear.Where(x => x.Azmoon.AzmoonId == AzmoonId && x.GenderId == 1 && x.JobId == azmoonJob.AzmoonJobCode && x.Accept == 1).OrderBy(x=>x.DegreeId).AsQueryable().ToList();


            Index = StartNumberWoman;
            foreach (var v in VWomanlist)
            {
                v.ChairCode = Index.ToString();
                v.AreaExecuteAzmoon = azmoonJob.RegionExamWoman;
                Index = Index + 1;
            }

            Index = StartNumberMan;
            foreach (var v in VManlist)
            {
                v.ChairCode = Index.ToString();
                v.AreaExecuteAzmoon = azmoonJob.RegionExam;
                Index = Index + 1;
            }

            _uow.BulkUpdateAllData(VManlist);
            _uow.BulkUpdateAllData(VWomanlist);

        }


        #region "---------------- Private Methods-----------------"

        private int GetCountValentear(int AzmoonId, int GenderId, long JobId)
        {
            return _valentear.Include(x => x.Azmoon)
                             .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                         x.GenderId == GenderId &&
                                         x.JobId == JobId &&
                                         x.Accept == 1).ToList().Count();
        }

        private SiteConfig GetAllOption()
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
        #endregion
    }
}
