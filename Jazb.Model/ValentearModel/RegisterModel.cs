using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.ValentearModel
{
    public  class RegisterModel
    {
        public ValentearModel Valentear { get; set; }

        public PostedDevotionType PostedDevotionType { get; set; }
        public IEnumerable<DevotionTypeModel> DevotionTypes { get; set; }
        public IEnumerable<DevotionTypeModel> SelectedDevotionTypes { get; set; }


        public PostedQoutaType PostedQoutaType { get; set; }
        public IEnumerable<QoutaTypeModel> QoutaTypes { get; set; }
        public IEnumerable<QoutaTypeModel> SelectedQoutaTypes { get; set; }
    }


    public class EditRegisterModel
    {
        public EditValentearModel Valentear { get; set; }

        public PostedDevotionType PostedDevotionType { get; set; }
        public IEnumerable<DevotionTypeModel> DevotionTypes { get; set; }
        public IEnumerable<DevotionTypeModel> SelectedDevotionTypes { get; set; }


        public PostedQoutaType PostedQoutaType { get; set; }
        public IEnumerable<QoutaTypeModel> QoutaTypes { get; set; }
        public IEnumerable<QoutaTypeModel> SelectedQoutaTypes { get; set; }
    }


    public class CardAuthorizationExam
    {
        public byte[] ValentearPicture { get; set; }
        public string CompanyLogo { get; set; }
        public string SiteUrl { get; set; }
        public string CompanyAddress { get; set; }
        public string GenderTitle { get; set; }
        public string Description { get; set; }
        public string DegreeTitle { get; set; }

        public string InputCardTitle { get; set; }
        public string CompanyName { get; set; }

        public string ChairCode { get; set; }

        public string TypePayment { get; set; }
        public string TrackingCodePaymentOnline { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalId { get; set; }

        public string JobTitle { get; set; }
        public string PlaceTitle { get; set; }

        public List<DevotionTypeModel> SelectedDevotionTypes { get; set; }
        public List<QoutaTypeModel> SelectedQoutaTypes { get; set; }

        public string DateExecuteAzmoon { get; set; }
        public string TimeExecuteAzmoon { get; set; }
        public string LocationExecuteAzmoon { get; set; }
        public string AreaExecuteAzmoon { get; set; }
        
        

    }



}

