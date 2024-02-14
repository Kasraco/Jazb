using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.ManageAzmoon.AdminAzmoonJob
{
    public class AzmoonJobModel
    {
        public int AzmoonId { get; set; }
        public int Periority { get; set; }

        public string AzmoonJobTitle { get; set; }
        public long AzmoonJobCode { get; set; }


        public int ManFrom { get; set; }
        public int ManTo { get; set; }
        public int WomanFrom { get; set; }
        public int WomanTo { get; set; }

        public string RegionExam { get; set; }
        public string RegionExamWoman { get; set; }
    }

    public class EditAzmoonJobModel
    {
        public int Id { get; set; }
        public int AzmoonId { get; set; }
        public int Periority { get; set; }

        public string AzmoonJobTitle { get; set; }
        public long AzmoonJobCode { get; set; }


        public int ManFrom { get; set; }
        public int ManTo { get; set; }
        public int WomanFrom { get; set; }
        public int WomanTo { get; set; }

        public string RegionExam { get; set; }
        public string RegionExamWoman { get; set; }
    }

    public class ExsitAzmoonJobModel
    {
        public int AzmoonId { get; set; }
        public string AzmoonJobTitle { get; set; }
        public long AzmoonJobCode { get; set; }
    }
}
