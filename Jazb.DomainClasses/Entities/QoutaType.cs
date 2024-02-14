using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class QoutaType
    {
        public virtual int Id { get; set; }
        public virtual string QoutaTypeTitle { get; set; }
        public virtual long Grade { get; set; }
    }
}
