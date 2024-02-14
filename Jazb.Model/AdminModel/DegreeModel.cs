using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    public class DegreeModel
    {
        public int Id { get; set; }

        [Display(Name ="عنوان مقطع تحصیلی"),Required(ErrorMessage ="عنوان مقطع تحصیلی الزامی است")]
        public string degreeTitle { get; set; }

        [Display(Name = "کد مقطع تحصیلی"), Required(ErrorMessage = "کد مقطع تحصیلی الزامی است")]
        public long degreeCode { get; set; }
    }
}
