using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.JobCity
{
    public class JobCityModel
    {

        public string JobName { get; set; }
        public long JobCode { get; set; }
        public int JobPeriority { get; set; }
        public long PlaceCode { get; set; }
        public string PlaceTitle { get; set; }
        public int PlacePeriority { get; set; }
    }
}
