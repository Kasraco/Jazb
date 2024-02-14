using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Payment
{
    public class PaymentModel
    {
        #region سازنده پیش فرض
        public PaymentModel()
        {
            InsertDatetime = System.DateTime.Now;
        }
        #endregion

        #region پراپرتی ها

        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Display(Name = "آی دی جدول")]
        public int PaymentId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "شماره پیگیری")]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string ReferenceNumber { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "شماره پرداخت")]
        public long SaleReferenceId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "وضعیت پرداخت")]
        [System.ComponentModel.DataAnnotations.MaxLength(100)]
        public string StatusPayment { get; set; }

        // فقط در صورتی که این فید ترو باشد پرداخت موفق بوده است
        [System.ComponentModel.DataAnnotations.Display(Name = "وضعیت پرداخت نهایی")]
        public bool PaymentFinished { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "مبلغ")]
        public long Amount { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "نام بانک")]
        [System.ComponentModel.DataAnnotations.MaxLength(50)]
        public string BankName { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "آی دی کاربر")]
        public int UserId { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "تاریخ خرید")]
        public DateTime InsertDatetime { get; set; }
        #endregion
    }
}
