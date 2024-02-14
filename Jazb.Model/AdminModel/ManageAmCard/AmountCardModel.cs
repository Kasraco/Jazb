using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.ManageAmCard
{
    public class AddAmountCardViewModel
    {

        [Display(Name = "تعداد کارت")]
        public int AmountCardCount { get; set; }

        [Display(Name = "مبلغ")]
        public string Amount { get; set; }

        [Display(Name = "آزمون")]
        public int AzmoonId { get; set; }

    }

    public class AddAmountCardModel
    {

        [Display(Name = "مبلغ")]
        public string Amount { get; set; }

        [Display(Name = "سریال پرداخت")]
        public string AmountSerial { get; set; }
        
        [Display(Name = "نوع آزمون")]
        public int AzmoonId { get; set; }
    }


    public class EditAmountCardModel
    {
        public int Id { get; set; }

        [Display(Name = "مبلغ")]
        public string Amount { get; set; }

        [Display(Name = "سریال پرداخت")]
        public string AmountSerial { get; set; }

        [Display(Name = "نام پرداخت کننده")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        
        [Display(Name = "شماره همراه")]
        public string PhoneNumber { get; set; }

        [Display(Name = "پست الکترونیکی")]
        public string EmailAddress { get; set; }

        [Display(Name = "سریال رسید پرداخت")]
        public string PaymentNumber { get; set; }

        [Display(Name = "نوع آزمون")]
        public int AzmoonId { get; set; }
    }
}
