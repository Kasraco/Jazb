using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    public class ConscriptStatusViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان وضعیت نظام وظیفه"), Required(ErrorMessage = "عنوان وضعیت نظام وظیفه الزامی است")]
        public string ConscriptStatusTitle { get; set; }

        [Display(Name = "کد وضعیت نظام وظیفه"), Required(ErrorMessage = "کد وضعیت نظام وظیفه الزامی است")]
        public long ConscriptStatusCode { get; set; }
    }
}
