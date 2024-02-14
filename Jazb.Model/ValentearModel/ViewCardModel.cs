using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.ValentearModel
{
   public class ViewCardModel
    {
       [Display(Name = "کد ملی")]
       [Required(ErrorMessage = "لطفا کد ملی خود را وارد کنید")]
       public string NationalCode { get; set; }

       public int AzmoonId { get; set; }
    }
}
