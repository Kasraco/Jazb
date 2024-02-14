using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class Religion
    {
        public int Id { get; set; }
        public string ReligionTitle { get; set; }
        public long ReligionCode { get; set; }
    }
}
