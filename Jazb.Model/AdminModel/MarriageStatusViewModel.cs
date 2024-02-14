using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    
    public class MarriageStatusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان وضعیت تاهل"), Required(ErrorMessage = "عنوان وضعیت تاهل الزامی است")]
        public string marriageStatusTitle { get; set; }

        [Display(Name = "کد وضعیت تاهل"), Required(ErrorMessage = "کد وضعیت تاهل الزامی است")]
        public long marriageStatusCode { get; set; }
    }

}
