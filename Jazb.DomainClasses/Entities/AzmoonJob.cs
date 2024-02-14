using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonJob
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
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
}
