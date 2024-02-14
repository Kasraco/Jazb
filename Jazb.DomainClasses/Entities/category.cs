using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class category
    {
        public  int CategoryId { get; set; }
        public  string Name { get; set; }
        public  string Description { get; set; }
        public  int? Order { get; set; }
      //  public virtual ICollection<article> Articles { get; set; }
    }
}
