using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonQoutaType
    {
        public virtual int Id { get; set; }
        public virtual string QoutaTypeTitle { get; set; }
        public virtual long Grade { get; set; }
        public virtual int Grade2 { get; set; }

        public virtual Azmoon Azmoon { get; set; }

    }
}
