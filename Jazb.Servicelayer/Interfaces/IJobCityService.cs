using Jazb.Model.AdminModel.JobCity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Servicelayer.Interfaces
{
    public interface IJobCityService
    {

        IList<JobCityModel> GetJobCityByAgahi(int AzmoonId);
    }
}
