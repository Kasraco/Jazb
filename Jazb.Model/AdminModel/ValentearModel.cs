using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    public class ValentearModel
    {

    }

    public class ValentearGroups
    {
        public string JobName { get; set; }
        public int CountMan { get; set; }
        public int CountWoman { get; set; }
        public int TotlaCount { get; set; }
    }

    public class ValentearJobCity
    {
        public string JobName { get; set; }
        public string CityName { get; set; }
        public int CountMan { get; set; }
        public int CountWoman { get; set; }
        public int TotlaCount { get; set; }
    }

    public class ReportValentearJobCity
    {
        public long JobCode { get; set; }
        public long PlaceCode { get; set; }
        public int JobPriority { get; set; }
        public int PlacePriority { get; set; }
        public string JobName { get; set; }
        public string PlaceTitle { get; set; }
        public int CountMan { get; set; }
        public int CountWoman { get; set; }
        public int TotlaCount { get; set; }
    }

    public class ReportValentearByGender
    {
        public int GenderId { get; set; }
        public string GenderTitle { get; set; }
        public List<ReportValentearByAgahiModel> ValentearsGroups { get; set; }
    }

    public class ReportSplitValentear
    {
        public int CountMan { get; set; }
        public int CountWoman { get; set; }
        public int TotlaCount { get; set; }
    }



    public class ReportValentearByAgahiModel
    {
        public long JobCode { get; set; }
        public long PlaceCode { get; set; }
        public int JobPriority { get; set; }
        public int PlacePriority { get; set; }
        public string JobName { get; set; }
        public string PlaceTitle { get; set; }
        public int CountMan { get; set; }
        public int CountWoman { get; set; }
        public int TotlaCount { get; set; }
        public List<Model.ValentearModel.ValentearModel> Valentears { get; set; }

    }

}
