using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    

    public class EducationdSkillViewModel
    {

        //[Display(Name = ""), Required(ErrorMessage = "")]

        public int Id { get; set; }

        [Display(Name = "عنوان رشته تحصیلی"), Required(ErrorMessage = "عنوان رشته تحصیلی الزامی است")]
        public string educatedSkillTitle { get; set; }

        [Display(Name = "کد رشته تحصیلی"), Required(ErrorMessage = "کد رشته تحصیلی الزامی است")]
        public long educatedSkillCode { get; set; }
    }
}
