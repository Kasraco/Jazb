using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class AttachmentImageSanjesh
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public string TrakingCode { get; set; }
        public int EditCode { get; set; }

         [Column(TypeName = "datetime2")]
        public DateTime EditDate { get; set; }
        public string PNO { get; set; }
        public string DavNO { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string FatherName { get; set; }
        public string Melicode { get; set; }
        public string Gender { get; set; }
        public string SHSh { get; set; }
        public string BirthDate { get; set; }
        public int AzmoonResult { get; set; }
        public string TellNumber { get; set; }
        public string MobileNumber { get; set; }
        public string JobName { get; set; }
        public string LocationName { get; set; }
        public string EducationName { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Molahezat { get; set; }
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
