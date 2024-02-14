using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    
    public class PlaceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان محل مورد تقاضا"), Required(ErrorMessage = "عنوان محل مورد تقاضا الزامی است")]
        public string PlaceTitle { get; set; }

        [Display(Name = "کد محل مورد تقاضا"), Required(ErrorMessage = "کد محل مورد تقاضا الزامی است")]
        public long PlaceCode { get; set; }
    }
}
