using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
  

    public class GenderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان جسیت"), Required(ErrorMessage = "عنوان جسیت الزامی است")]
        public string GenderTitle { get; set; }

        [Display(Name = "کد جسیت"), Required(ErrorMessage = "کد جسیت الزامی است")]
        public long GenderCode { get; set; }
    }
}
