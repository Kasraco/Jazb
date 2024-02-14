using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonPlace
    {
        public virtual int Id { get; set; }

        public virtual Azmoon Azmoon { get; set; }
        public virtual int JobId { get; set; }
        public virtual string PlaceTitle { get; set; }
        public virtual long PlaceCode { get; set; }

        public virtual int Capacity { get; set; }
        public virtual int Periority { get; set; }
        public virtual bool WillWoman { get; set; }
        public virtual bool WillMan { get; set; }

        public bool AcceptValentears { get; set; }
        public string Description { get; set; }


    }
}
