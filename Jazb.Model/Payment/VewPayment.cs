using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Payment
{
    public class VewPayment
    {
        public int ValentearID { get; set; }
        public int AzmoonId { get; set; }

        [Display(Name ="نام")]
        [Required(ErrorMessage ="این آیتم الزامی است")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "این آیتم الزامی است")]
        public string LastName { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "این آیتم الزامی است")]
        public string Mobile { get; set; }

        [Display(Name = "پست الکترونیک")]
        public string EmailAddress { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string AzmoonTitle { get; set; }

        [Display(Name = "هزینه ثبت نام")]
        public string AmountValue { get; set; }




        
       
    }
}
