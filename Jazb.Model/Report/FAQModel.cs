using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Report
{
    public class FAQModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MeliCode { get; set; }
        public string Mobile { get; set; }
        public string RequestType { get; set; }
    }

    public class FAQMeliCode
    {
        public string MeliCode { get; set; }
    }
}
