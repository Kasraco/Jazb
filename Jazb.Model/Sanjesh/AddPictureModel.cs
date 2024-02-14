using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Sanjesh
{
    public class AddPictureModel
    {
        public long ID { get; set; }
        [Display(Name="شماره پرونده")]
        public string PNO { get; set; }

          [Display(Name = "شماره داوطلبی")]
        public string DavNO { get; set; }

          [Display(Name = "نام")]
        public string Fname { get; set; }

          [Display(Name = "نام خانوادگی")]
        public string Lname { get; set; }

          [Display(Name = "کد ملی")]
        public string Melicode { get; set; }
        public byte[] personel_picture { get; set; }
        public byte[] personel_Sanjesh { get; set; }
        public byte[] personel_GVT { get; set; }
        public byte[] personel_Shsh1 { get; set; }
        public byte[] personel_Shsh2 { get; set; }
        public byte[] personel_Shsh3 { get; set; }
        public byte[] personel_melicode_On { get; set; }
        public byte[] personel_melicode_Back { get; set; }
        public byte[] personel_Kardani { get; set; }
        public byte[] personel_Karshenasi { get; set; }
        public byte[] personel_KarshenasiArshad { get; set; }
        public byte[] personel_Doctor { get; set; }
        public byte[] personel_Isargari1 { get; set; }
        public byte[] personel_Isargari2 { get; set; }
        public byte[] personel_Isargari3 { get; set; }
        public byte[] personel_Nezamvazifeh1 { get; set; }
        public byte[] personel_Nezamvazifeh2 { get; set; }
        public byte[] personel_Gavahinameh_on { get; set; }
        public byte[] personel_Gavahinameh_back { get; set; }
        public byte[] personel_Bomi1 { get; set; }
        public byte[] personel_Bomi2 { get; set; }
        public byte[] personel_Bomi3 { get; set; }
        public byte[] personel_Bomi4 { get; set; }
        public byte[] personel_SaghfSeni1 { get; set; }
        public byte[] personel_SaghfSeni2 { get; set; }
        public byte[] personel_SaghfSeni3 { get; set; }
        public byte[] personel_CoronaForm { get; set; }
        public byte[] personel_OtheDoc1 { get; set; }
        public byte[] personel_OtheDoc2 { get; set; }
        public byte[] personel_OtheDoc3 { get; set; }

    }
}
