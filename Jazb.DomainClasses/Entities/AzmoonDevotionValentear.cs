using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonDevotionValentear
    {
        public virtual int Id { get; set; }
        public virtual string DevotionTitle { get; set; }
        public virtual long DevotionCode { get; set; }

        public virtual Valentear Valentear { get; set; }
    }
}
