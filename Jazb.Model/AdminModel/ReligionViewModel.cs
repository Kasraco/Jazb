using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{

    public class ReligionViewModel
    {
        public int Id { get; set; }


        [Display(Name = "عنوان دین"), Required(ErrorMessage = "عنوان دین الزامی است")]
        public string ReligionTitle { get; set; }

        [Display(Name = "کد دین"), Required(ErrorMessage = "کد دین الزامی است")]
        public long ReligionCode { get; set; }

    }
}
