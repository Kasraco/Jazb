using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    
    public class FaithViewModel
    {
        public int Id { get; set; }

        [Display(Name = "نام مذهب"), Required(ErrorMessage = "نام مذهب الزامی است")]
        public string FaithTitle { get; set; }

        [Display(Name = "کد مذهب"), Required(ErrorMessage = "کد مذهب الزامی است")]
        public long FaithCode { get; set; }
    }
}
