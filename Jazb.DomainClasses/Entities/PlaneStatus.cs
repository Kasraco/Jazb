using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class PlaneStatus
    {
        public int Id { get; set; }
        public string PlaneStatusTitle { get; set; }
        public long PlaneStatusCode { get; set; }
    }
}
