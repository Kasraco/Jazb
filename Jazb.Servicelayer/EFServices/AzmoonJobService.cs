using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob;
using Jazb.Datalayer.Context;
using System.Data.Entity;
using Jazb.DomainClasses.Entities;

namespace Jazb.Servicelayer.EFServices
{
    public class AzmoonJobService : IAzmoonJobService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AzmoonJob> _azmoonJob;
        private readonly IDbSet<Valentear> _valentear;
        public AzmoonJobService(IUnitOfWork uow)
        {
            _uow = uow;
            _azmoonJob = _uow.Set<AzmoonJob>();
            _valentear = _uow.Set<Valentear>();
        }

        public int AddAzmoonJob(AzmoonJobModel model)
        {
            throw new NotImplementedException();
        }

        public int EditAzmoonJobs(ChaireNumberViewModel model)
        {
            List<AzmoonJob> lstAJ = new List<AzmoonJob>();
            foreach (var t in model.ChairNModel)
            {
                var aj = _azmoonJob.Include(x => x.Azmoon).Where(x => x.Id == t.Id).First();
                aj.ManFrom = t.ManFrom;
                aj.ManTo = t.ManTo;
                aj.WomanFrom = t.WomanFrom;
                aj.WomanTo = t.WomanTo;
                aj.RegionExam = t.ManRegionExam;
                aj.RegionExamWoman = t.WomanRegionExam;
                lstAJ.Add(aj);
            }
            _uow.BulkUpdateAllData(lstAJ.AsEnumerable());
            return 0;
        }

        public int EditAzmoonJob(EditAzmoonJobModel model)
        {
            throw new NotImplementedException();
        }

        public ChaireNumberViewModel GetAzmoonJobsModel(int AzmoonId)
        {


            var qaj = (from aj in _azmoonJob.Include(x => x.Azmoon).Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList()

                       select new ChaireNumberModel
                       {
                           Id = aj.Id,
                           JobName = aj.AzmoonJobTitle,
                           ManRegionExam = aj.RegionExam,
                           WomanRegionExam = aj.RegionExamWoman,
                           ManFrom = aj.ManFrom,
                           ManTo = aj.ManTo,
                           WomanFrom = aj.WomanFrom,
                           WomanTo = aj.WomanTo,
                           ManCapacity = GetCountValentear(aj.Azmoon.AzmoonId, 2, aj.AzmoonJobCode),
                           WomanCapacity = GetCountValentear(aj.Azmoon.AzmoonId, 1, aj.AzmoonJobCode)

                       }).ToList();


            return new ChaireNumberViewModel { AzmoonId = AzmoonId, ChairNModel = qaj };
        }
        private int GetCountValentear(int AzmoonId,int GenderId,long JobId)
        {
            return _valentear.Include(x => x.Azmoon)
                             .Select(x => new { x.Azmoon, x.GenderId, x.JobId, x.Accept })
                             .Where(x => x.Azmoon.AzmoonId == AzmoonId &&
                                         x.GenderId == GenderId &&
                                         x.JobId == JobId &&
                                         x.Accept == 1).ToList().Count();
        }

        public EditAzmoonJobModel GetAzmoonJob(int Id)
        {
            throw new NotImplementedException();
        }

        public bool ExistJobPlace(ExsitAzmoonJobModel model)
        {
            throw new NotImplementedException();
        }
    }
}
