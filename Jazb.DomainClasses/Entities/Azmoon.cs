using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Jazb.DomainClasses.Entities
{
    public class Azmoon
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AzmoonId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string Name { get; set; }

        [Display(Name = "تیتر آزمون")]
        public string Title { get; set; }

        [Display(Name = "توضیحات آزمون")]
        public string Discription { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان ")]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        [Display(Name = "تاریخ برگزاری آزمون ")]
        [Column(TypeName = "datetime2")]
        public DateTime DateExecuteAzmoon { get; set; }

        [Display(Name = "زمان برگزاری آزمون")]
        public string TimeExecuteAzmoon { get; set; }

        [Display(Name = "محل برگزاری ")]
        public string LocationExecuteAzmoon { get; set; }

        [Display(Name = "عنوان کارت ورود به جلسه")]
        public string InputCardTitle { get; set; }

        [Display(Name = "فعال / غیر فعال")]
        public bool Active { get; set; }
        public bool EndRegister { get; set; }

        [Display(Name = "نمایش آزمون")]
        public bool ShowAzmoon { get; set; }

        [Display(Name = "نمایش لینک دریافت آکهی")]
        public bool ShowDownloadAgahi { get; set; }

        [Display(Name = "نمایش لینک ثبت نام داوطلبان")]
        public bool ShowRegisters { get; set; }

        [Display(Name = "نمایش لینک دریافت کارت ورود به جلسه")]
        public bool ShowPrintCard { get; set; }

         [Display(Name = "نمایش لینک دریافت نتایج آزمون")]
        public bool ShowCanPrintResult { get; set; }

        [Display(Name = "نمایش لینک ویرایش ثبت نام")]
        public bool ShowCanEditResult { get; set; }
        
        public AzmoonStatuse AzmoonStatuse { get; set; }


        public byte[] AghiFileData { get; set; }
        public string AghiFileDataContentType { get; set; }
        public string AghiFileDataFileName { get; set; }

        public byte[] PicturAzmoon { get; set; }
        public string PicturAzmoonContentType { get; set; }
        public string PicturAzmoonFileName { get; set; }


        [Display(Name = " تاریخ شروع اعلان نتایج")]
        [Column(TypeName = "datetime2")]
        public DateTime ResultStartDate { get; set; }

        [Display(Name = "تاریخ پایان  اعلان نتایج ")]
        [Column(TypeName = "datetime2")]
        public DateTime ResultEndDate { get; set; }

        public string SignTextValentear { get; set; }

        public string AmountValue { get; set; }

        public bool EveryOneSee { get; set; }

        /// <summary>
        /// 0 = ندارد
        /// 1 = پرداخت بصورت دستی
        /// 2 = پرداخت آنلاین
        /// </summary>
        public PaymentType AzmoonPaymentType { get; set; }
        

        public virtual ICollection<Valentear> Valentears { get; set; }

    }

    public enum PaymentType
    {
        [Display(Name = "بدون پرداخت")]
        NoPayment = 0,

        [Display(Name = "پرداخت دستی")]
        ManualPayment = 1,

        [Display(Name = "پرداخت الکترونیکی")]
        OnlinePayment = 2,

        [Display(Name = "پرداخت الکترونیکی هنگام دریافت کارت ازمون")]
        OnlinePaymentGetCard = 3
    }
}
