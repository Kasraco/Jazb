using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Sanjesh
{
    public class LoginSanjesh
    {


        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "لطفا کد ملی خود را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "شماره داوطلبی")]
        [Required(ErrorMessage = "لطفا شماره داوطلبی خود را وارد کنید")]
        public string Password { get; set; }
    }
}
