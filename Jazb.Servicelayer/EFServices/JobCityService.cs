using Jazb.Datalayer.Context;
using Jazb.DomainClasses.Entities;
using Jazb.Servicelayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.EFServices
{
    public class JobCityService : IJobCityService
    {
        private static int _searchTakeCount;
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AzmoonJob> _Job;
        private readonly IDbSet<AzmoonPlace> _Place;


        public JobCityService(IUnitOfWork uow)
        {
            _uow = uow;
            _Job = _uow.Set<AzmoonJob>();
            _Place = _uow.Set<AzmoonPlace>();
            _searchTakeCount = 10;
        }



        public IList<Model.AdminModel.JobCity.JobCityModel> GetJobCityByAgahi(int AzmoonId)
        {
            var qJobs = _Job.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();
            var qPlace = _Place.Where(x => x.Azmoon.AzmoonId == AzmoonId).ToList();

            List<Model.AdminModel.JobCity.JobCityModel> lstRVJC = new List<Model.AdminModel.JobCity.JobCityModel>();           
            var qAgahi = (from j in qJobs
                          join p in qPlace on j.AzmoonJobCode equals p.JobId
                          select new Model.AdminModel.JobCity.JobCityModel
                          {
                              JobName = j.AzmoonJobTitle,
                              JobCode = j.AzmoonJobCode,
                              JobPeriority = j.Periority,
                              PlaceCode = p.PlaceCode,
                              PlaceTitle = p.PlaceTitle,
                              PlacePeriority = p.Periority
                          }).ToList();

            return qAgahi.OrderBy(x => x.JobPeriority).ThenBy(x => x.PlacePeriority).ToList() ;
        }
    }
}
