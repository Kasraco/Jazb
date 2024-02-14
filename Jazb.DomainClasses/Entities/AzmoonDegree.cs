using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonDegree
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
        public int Periority { get; set; }
        public string Title { get; set; }
        public long Code { get; set; }
    }
}
