using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jazb.Model.Report;
using Jazb.Model;

namespace Jazb.Servicelayer.EFServices
{
    public class ReportService : IReportService
    {


        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Azmoon> _Azmoon;
        private readonly IDbSet<ConscriptStatus> _ConscriptStatus;
        private readonly IDbSet<cooperationHistory> _cooperationHistory;
        private readonly IDbSet<AzmoonDegree> _degree;
        private readonly IDbSet<AzmooneducatedSkill> _educatedSkill;
        private readonly IDbSet<Faith> _Faith;
        private readonly IDbSet<Gender> _Gender;
        private readonly IDbSet<AzmoonJob> _AzmoonJob;
        private readonly IDbSet<marriageStatus> _marriageStatus;
        private readonly IDbSet<AzmoonPlace> _AzmoonPlace;
        private readonly IDbSet<PlaneStatus> _PlaneStatus;
        private readonly IDbSet<Religion> _Religion;
        private readonly IDbSet<Valentear> _valentear;
        private readonly IDbSet<Option> _options;
        private readonly IDbSet<AzmoonValentearResult> _azmoonValentearResult;

        private JazbDbContext db;
        public ReportService(IUnitOfWork uow)
        {

            _uow = uow;
            _valentear = _uow.Set<Valentear>();
            _ConscriptStatus = _uow.Set<ConscriptStatus>();
            _cooperationHistory = _uow.Set<cooperationHistory>();
            _degree = _uow.Set<AzmoonDegree>();
            _educatedSkill = _uow.Set<AzmooneducatedSkill>();
            _Faith = _uow.Set<Faith>();
            _Gender = _uow.Set<Gender>();
            _AzmoonJob = _uow.Set<AzmoonJob>();
            _marriageStatus = _uow.Set<marriageStatus>();
            _AzmoonPlace = _uow.Set<AzmoonPlace>();
            _PlaneStatus = _uow.Set<PlaneStatus>();
            _Religion = _uow.Set<Religion>();
            _options = _uow.Set<Option>();
            _azmoonValentearResult = _uow.Set<AzmoonValentearResult>();

            db = new JazbDbContext();
        }

        public List<ValenteraFullReportsModel> GetAllValentear(int AzmoonId)
        {

            var lstVal = (from V in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).AsNoTracking().AsQueryable()
                          join con in _ConscriptStatus.AsQueryable() on V.ConscriptStatusId equals con.ConscriptStatusCode
                          join coo in _cooperationHistory.AsQueryable() on V.CooperationHistoryId equals coo.cooperationHistoryCode
                          join deg in _degree.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.DegreeId equals deg.Code
                          join edu in _educatedSkill.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.EducatedSkillId equals edu.AzmooneducatedSkillCode
                          join fai in _Faith.AsQueryable() on V.FaithId equals fai.FaithCode
                          join job in _AzmoonJob.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.JobId equals job.AzmoonJobCode
                          join pla in _AzmoonPlace.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable()
                                       on new { A = V.PlaceId, B = V.JobId } equals new { A = pla.PlaceCode, B = pla.JobId }
                          join mar in _marriageStatus.AsQueryable() on V.MarriageStatusId equals mar.marriageStatusCode
                          join gen in _Gender.AsQueryable() on V.GenderId equals gen.GenderCode
                          join pln in _PlaneStatus.AsQueryable() on V.PlanStatusId equals pln.PlaneStatusCode
                          join rel in _Religion.AsQueryable() on V.ReligionId equals rel.ReligionCode

                          select new ValenteraFullReportsModel
                          {
                              AzmoonName = V.Azmoon.Title,
                              ConscriptStatusTitle = con.ConscriptStatusTitle,
                              CooperationHistoryTitle = coo.cooperationHistoryTitle,
                              DegreeTitle = deg.Title,
                              EducatedSkillTitle = edu.AzmooneducatedSkillTitle,
                              FaithTitle = fai.FaithTitle,
                              GenderName = gen.GenderTitle,
                              JobName = job.AzmoonJobTitle,
                              MarriageStatusTitle = mar.marriageStatusTitle,
                              PlaceName = pla.PlaceTitle,
                              PlanStatusTitle = pln.PlaneStatusTitle,
                              ReligionTitle = rel.ReligionTitle,
                              intChaireCode = 0,
                              Accept = V.Accept,
                              Address_CityName = V.Address_CityName,
                              Address_ProvinceName = V.Address_ProvinceName,
                              Address_ZoneName = V.Address_ZoneName,
                              Adress_Adress = V.Adress_Adress,
                              Average = V.Average,
                              BirthCertificateNo = V.BirthCertificateNo,
                              BirthDate = V.BirthDate,
                              ChaireCode = V.ChairCode,
                              CooperationDay = V.CooperationDay,
                              CooperationMonth = V.CooperationMonth,
                              CooperationYear = V.CooperationYear,
                              Description = V.Description,
                              EducatedField = V.EducatedField,
                              FatherName = V.FatherName,
                              FirstName = V.FirstName,
                              grade = V.grade,
                              IssuancePlace_City = V.IssuancePlace_City,
                              LastName = V.LastName,
                              LicenseDate = V.LicenseDate,
                              MaxDevotionType = V.MaxDevotionType,
                              MaxQoutaType = V.MaxQoutaType,
                              Mobile = V.Mobile,
                              NationalID = V.NationalID,
                              PlaceKhedmat = V.PlaceKhedmat,
                              PlanDay = V.PlanDay,
                              PlanMonth = V.PlanMonth,
                              PlanYear = V.PlanYear,
                              PostalCode = V.PostalCode,
                              SchoolName = V.SchoolName,
                              score = V.score,
                              SumDevotionType = V.SumDevotionType,
                              SumQoutaType = V.SumQoutaType,
                              Tel = V.Tel,
                              TellNeed = V.TellNeed,
                              TextDevotionType = V.TextDevotionType,
                              TextQoutaType = V.TextQoutaType,
                              UniversityProvinceTitle = V.UniversityProvinceTitle,
                              University_Name = V.University_Name,
                              DateRegister = V.DateRegister,
                              AzmoonLocation = V.AreaExecuteAzmoon

                          }).AsQueryable();

            return lstVal.OrderBy(x=>x.intChaireCode).ToList();
        }

        public List<ValenteraFullWithNumberReportsModel> GetAllValentearWithNumber(int AzmoonId)
        {
            var qAnswerVal = _azmoonValentearResult.Include(x => x.Azmoon)
                                                 .Include(x => x.Valentear)
                                                 .AsNoTracking()
                                                 .Where(x => x.Azmoon.AzmoonId == AzmoonId)
                                                 .ToList();

            var lstVal = (from V in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).AsNoTracking().AsQueryable()
                          join con in _ConscriptStatus.AsQueryable() on V.ConscriptStatusId equals con.ConscriptStatusCode
                          join coo in _cooperationHistory.AsQueryable() on V.CooperationHistoryId equals coo.cooperationHistoryCode
                          join deg in _degree.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.DegreeId equals deg.Code
                          join edu in _educatedSkill.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.EducatedSkillId equals edu.AzmooneducatedSkillCode
                          join fai in _Faith.AsQueryable() on V.FaithId equals fai.FaithCode
                          join job in _AzmoonJob.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.JobId equals job.AzmoonJobCode
                          join pla in _AzmoonPlace.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable()
                                       on new { A = V.PlaceId, B = V.JobId } equals new { A = pla.PlaceCode, B = pla.JobId }
                          join mar in _marriageStatus.AsQueryable() on V.MarriageStatusId equals mar.marriageStatusCode
                          join gen in _Gender.AsQueryable() on V.GenderId equals gen.GenderCode
                          join pln in _PlaneStatus.AsQueryable() on V.PlanStatusId equals pln.PlaneStatusCode
                          join rel in _Religion.AsQueryable() on V.ReligionId equals rel.ReligionCode
                          from avr in _azmoonValentearResult.Where(mapp=> V.Id==mapp.Valentear.Id).DefaultIfEmpty()

                          select new ValenteraFullWithNumberReportsModel
                          {
                              AzmoonName = V.Azmoon.Title,
                              ConscriptStatusTitle = con.ConscriptStatusTitle,
                              CooperationHistoryTitle = coo.cooperationHistoryTitle,
                              DegreeTitle = deg.Title,
                              EducatedSkillTitle = edu.AzmooneducatedSkillTitle,
                              FaithTitle = fai.FaithTitle,
                              GenderName = gen.GenderTitle,
                              JobName = job.AzmoonJobTitle,
                              MarriageStatusTitle = mar.marriageStatusTitle,
                              PlaceName = pla.PlaceTitle,
                              PlanStatusTitle = pln.PlaneStatusTitle,
                              ReligionTitle = rel.ReligionTitle,
                              intChaireCode = 0,
                              Accept = V.Accept,
                              Address_CityName = V.Address_CityName,
                              Address_ProvinceName = V.Address_ProvinceName,
                              Address_ZoneName = V.Address_ZoneName,
                              Adress_Adress = V.Adress_Adress,
                              Average = V.Average,
                              BirthCertificateNo = V.BirthCertificateNo,
                              BirthDate = V.BirthDate,
                              ChaireCode = V.ChairCode,
                              CooperationDay = V.CooperationDay,
                              CooperationMonth = V.CooperationMonth,
                              CooperationYear = V.CooperationYear,
                              Description = V.Description,
                              EducatedField = V.EducatedField,
                              FatherName = V.FatherName,
                              FirstName = V.FirstName,
                              grade = V.grade,
                              IssuancePlace_City = V.IssuancePlace_City,
                              LastName = V.LastName,
                              LicenseDate = V.LicenseDate,
                              MaxDevotionType = V.MaxDevotionType,
                              MaxQoutaType = V.MaxQoutaType,
                              Mobile = V.Mobile,
                              NationalID = V.NationalID,
                              PlaceKhedmat = V.PlaceKhedmat,
                              PlanDay = V.PlanDay,
                              PlanMonth = V.PlanMonth,
                              PlanYear = V.PlanYear,
                              PostalCode = V.PostalCode,
                              SchoolName = V.SchoolName,
                              score = V.score,
                              SumDevotionType = V.SumDevotionType,
                              SumQoutaType = V.SumQoutaType,
                              Tel = V.Tel,
                              TellNeed = V.TellNeed,
                              TextDevotionType = V.TextDevotionType,
                              TextQoutaType = V.TextQoutaType,
                              UniversityProvinceTitle = V.UniversityProvinceTitle,
                              University_Name = V.University_Name,
                              DateRegister = V.DateRegister,
                              GeneralNumber = avr.GeneralFinalScore.ToString(),
                              TechnicalNumber = avr.TechnicalFinalScore.ToString()

                          }).AsQueryable();

            return lstVal.OrderBy(x => x.intChaireCode).ToList();
        }

        public List<ValenteraFullAcceptPasokhnamehReportsModel> GetAllAcceptValentear(int AzmoonId)
        {



            var lstVal = (from V in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Accept == 1).AsNoTracking().AsQueryable()

                          join deg in _degree.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.DegreeId equals deg.Code
                          join edu in _educatedSkill.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.EducatedSkillId equals edu.AzmooneducatedSkillCode

                          join job in _AzmoonJob.Where(x => x.Azmoon.AzmoonId == AzmoonId).AsQueryable() on V.JobId equals job.AzmoonJobCode
                          join plc in _AzmoonPlace.Where(x=>x.Azmoon.AzmoonId==AzmoonId).AsQueryable() on V.PlaceId equals plc.PlaceCode

                          select new ValenteraFullAcceptPasokhnamehReportsModel
                          {
                              FirstName = V.FirstName,
                              LastName = V.LastName,
                              AzmoonName = "دانشگاه علوم پزشکی وخدمات بهداشتی درمانی",
                              DegreeTitle = deg.Title,
                              BaseClass = job.AzmoonJobTitle,
                              EducatedSkillTitle = edu.AzmooneducatedSkillTitle,
                              JobName =plc.PlaceTitle,
                              intChaireCode = 0,
                              ChaireCode = V.ChairCode,
                              FatherName = V.FatherName,
                              PictureValen = @"D:\Images\" + V.NationalID + ".jpg",
                              MobileNumber = V.Mobile,
                              NatinalID = V.NationalID

                          }).AsQueryable();

            return lstVal.OrderBy(x=>x.intChaireCode).ToList();
        }


        public List<ValenteraFullAcceptPictureValentearModel> GetAllAcceptPictureValentear(int AzmoonId)
        {

            var lstVal = (from V in _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.Accept == 1).AsNoTracking().AsQueryable()

                          select new ValenteraFullAcceptPictureValentearModel
                          {
                              PictureValen = V.NationalID + ".jpg",
                              PictureValentear = V.PictureValenteer

                          }).AsQueryable();

            return lstVal.ToList();
        }

        public List<ValenteraFullResultAnswerReportModel> GetAllValentearAnswer(int AzmoonId,int Grade)
        {
            var qvalentear = _valentear.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId && x.grade < Grade).ToList();
            var qAnswerVal = _azmoonValentearResult.Include(x => x.Azmoon)
                                                   .Include(x => x.Valentear)
                                                   .AsNoTracking()
                                                   .Where(x => x.Azmoon.AzmoonId == AzmoonId)                                                   
                                                   .ToList();
            var qAJobPlace = _AzmoonPlace.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            var qAJob = _AzmoonJob.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();

            var qlist = (from v in qvalentear
                         join avr in qAnswerVal on v.Id equals avr.Valentear.Id into avrg
                         join aj in qAJob on v.JobId equals aj.AzmoonJobCode
                         join ajp in qAJobPlace on new { A = v.JobId, B = v.PlaceId } equals new { A = ajp.JobId, B = ajp.PlaceCode }
                         from avrE in avrg.DefaultIfEmpty(new AzmoonValentearResult())


                         select new ValenteraFullResultAnswerReportModel
                         {                            
                             ChaireCode = v.ChairCode,
                             FirstName = v.FirstName,
                             LastName = v.LastName,
                             FatherName = v.FatherName,
                             NationalID = v.NationalID,
                             JobName = aj.AzmoonJobTitle,
                             PlaceName = ajp.PlaceTitle,
                             Capacity = ajp.Capacity,
                             Mobile = v.Mobile,
                             TextDevotionType = v.TextDevotionType,
                             TextQoutaType = v.TextQoutaType,
                             score = v.score,
                             grade = v.grade,
                             FinalScore = avrE.FinalScore,
                             TechnicalFinalScore = avrE.TechnicalFinalScore,
                             GeneralFinalScore = avrE.GeneralFinalScore,
                             StateResult=avrE.StateResult
                         }).ToList();

            return qlist;
        }





    }
}
