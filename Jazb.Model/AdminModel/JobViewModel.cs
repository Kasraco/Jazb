using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
   

    public class JobViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان رشته شغلی"), Required(ErrorMessage = "عنوان رشته شغلی الزامی است")]
        public string JobTitle { get; set; }

        [Display(Name = "کد رشته شغلی"), Required(ErrorMessage = "کد رشته شغلی الزامی است")]
        public long JobCode { get; set; }
    }
}
