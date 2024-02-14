using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Report
{
    public class ValentearReportModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GenderName { get; set; }
        public string BirthCertificateNo { get; set; }
        public string NationalID { get; set; }
        public string JobName { get; set; }
        public string PlaceName { get; set; }
        public string TextDevotionType { get; set; }
        public int MaxDevotionType { get; set; }
        public int MaxQoutaType { get; set; }
        public string TextQoutaType { get; set; }
        public string Mobile { get; set; }
    }

    public class ValentearCardReportModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GenderName { get; set; }
        public string BirthCertificateNo { get; set; }
        public string NationalID { get; set; }
        public string JobName { get; set; }
        public string PlaceName { get; set; }
        public string TextDevotionType { get; set; }
        public string TextQoutaType { get; set; }
        public string ChairCode { get; set; }
        public byte[] ValentearPicture { get; set; }
        public string CompanyLogo { get; set; }
        public string LocationExecuteAzmoon { get; set; }
        //public string AreaExecuteAzmoon { get; set; }
        public string InputCardTitle { get; set; }
       // public string DateExecuteAzmoon { get; set; }
        public string TimeExecute { get; set; }
        public string CompanyName { get; set; }

    }

    public class ValentearCardReportEnModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GenderName { get; set; }
        public string BirthCertificateNo { get; set; }
        public string NationalID { get; set; }
        public string JobName { get; set; }
        public string PlaceName { get; set; }
        public string TextDevotionType { get; set; }
        public string TextQoutaType { get; set; }
        public string ChairCode { get; set; }
        public byte[] ValentearPicture { get; set; }
        public string CompanyLogo { get; set; }
        public string LocationExecuteAzmoon { get; set; }
        public string AreaExecuteAzmoon { get; set; }
        public string InputCardTitle { get; set; }
         public DateTime DateExecuteAzmoon { get; set; }
        public string TimeExecute { get; set; }
        public string CompanyName { get; set; }

    }

    public class ValentearChairNumberReportModel
    {
        public string ChairCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobName { get; set; }
        public string InputCardTitle { get; set; }
        public byte[] ValentearPicture { get; set; }
        public string CompanyName { get; set; }

    }

  
}
