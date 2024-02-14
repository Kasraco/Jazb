using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.ManageAzmoon
{
    public class AzmoonDegreeModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "مقطع تحصیلی"), Required(ErrorMessage = "انتخاب مقطع تحصیلی الزامی است")]
        public int DegreeId { get; set; }
    }


    public class AzmoonEducationViewModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "رشته تحصیلی"), Required(ErrorMessage = "انتخاب رشته تحصیلی الزامی است")]
        public int EducationId { get; set; }
    }

    public class AzmoonJobModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "رشته شغلی"), Required(ErrorMessage = "انتخاب رشته شغلی الزامی است")]
        public int JobId { get; set; }  
    }



    public class AzmoonPlaceModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "رشته شغلی"), Required(ErrorMessage = "انتخاب رشته شغلی الزامی است")]
        public int PlaceJobId { get; set; }

        [Display(Name = "محل مورد تقاضا"), Required(ErrorMessage = "انتخاب محل مورد تقاضا الزامی است")]
        public int PlaceId { get; set; }

        [Display(Name = "ظرفیت"), Required(ErrorMessage = "ظرفیت الزامی است")]
        public int Capacity { get; set; }

        [Display(Name = "مرد")]
        public bool WillMan { get; set; }

        [Display(Name = "زن")]
        public bool WillWoman { get; set; }
        
    }



    public class DevotionTypeModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "سهمیه ایثارگری"), Required(ErrorMessage = "انتخاب سهمیه ایثارگری الزامی است")]
        public int DevotionId { get; set; }
    }

    public class AzmoonQoutaTypeModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "سهمیه"), Required(ErrorMessage = "انتخاب سهمیه الزامی است")]
        public int QoutaTypeId { get; set; }
    }


    


}
