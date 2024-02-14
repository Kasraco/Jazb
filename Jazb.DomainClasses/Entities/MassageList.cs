using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class MassageList
    {
        public int Id { get; set; }
        public string NationalID { get; set; }
        public int AzmoonId { get; set; }
        public string MessageBody { get; set; }
    }
}
