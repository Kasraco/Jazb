using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AzmoonValentearAnswer
    {
        public int Id { get; set; }
        public Azmoon Azmoon { get; set; }
        public int KeyCode { get; set; }
        public string  ChairCode { get; set; }
        public string ValentearAnswer { get; set; }
        public int Count { get; set; }
    }
}
