using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonQoutasValentear
    {
        public virtual int Id { get; set; }
        public virtual string QoutaTitle { get; set; }
        public virtual long QoutaCode { get; set; }
        public virtual Valentear Valentear { get; set; }
    }
}
