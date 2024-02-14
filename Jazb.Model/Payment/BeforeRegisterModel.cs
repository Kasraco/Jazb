using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Payment
{
    public class BeforeRegisterModel
    {
        public int AzmoonId { get; set; }
        
        [Display(Name ="شماره سفارش")]
        [Required(ErrorMessage ="این آیتم الزامی است")]
        public string PaymentId { get; set; }

        [Display(Name = "کد ثبت نام")]
        [Required(ErrorMessage = "این آیتم الزامی است")]
        public long SaleRefrenceId { get; set; }
    }
}
