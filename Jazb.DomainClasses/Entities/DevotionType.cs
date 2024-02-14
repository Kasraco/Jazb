using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class DevotionType
    {
        public virtual int Id { get; set; }
        public virtual string DevotionTypeTitle { get; set; }
        public virtual long Grade { get; set; }
    }
}
