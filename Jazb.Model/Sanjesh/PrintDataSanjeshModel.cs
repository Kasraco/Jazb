using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Sanjesh
{
    public class PrintDataSanjeshModel
    {

        public string StrMessage { get; set; }
        public string TrackingCode { get; set; }
        public bool Hasmolahezat { get; set; }
        public DateTime DateSign { get; set; }

        [Display(Name = "ملاحظات")]
         public string Molahezat { get; set; }

        public AddPictureModel AddPM { get; set; }
    }

    public class PrintDataModel
    {

        public string StrMessage { get; set; }
        public string TrackingCode { get; set; }
        public bool Hasmolahezat { get; set; }
        public DateTime DateSign { get; set; }

        [Display(Name = "ملاحظات")]
        public string Molahezat { get; set; }

    }
}
