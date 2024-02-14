using Jazb.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Jazb.Model.AdminModel
{
    public class AzmoonDataTableModel
    {
        public int AzmoonId { get; set; }
        public string AzmoonBoxTitle { get; set; }
        public string AzmoonTitle { get; set; }
        public string AzmoonDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ValentearCount { get; set; }
        public int WinCount { get; set; }
        public int RejectCount { get; set; }
        public int AcceptCount { get; set; }

        public byte[] AghiFileData { get; set; }
        public byte[] PicturAzmoon { get; set; }

        [Display(Name = " تاریخ شروع اعلان نتایج")]
        [Column(TypeName = "datetime2")]
        public DateTime ResultStartDate { get; set; }

        [Display(Name = "تاریخ پایان  اعلان نتایج ")]
        [Column(TypeName = "datetime2")]
        public DateTime ResultEndDate { get; set; }

        public string SignTextValentear { get; set; }

        public string AmountValue { get; set; }
        public PaymentType AzoonPaymentTypes { get; set; }
        public bool EveryOneSee { get; set; }

    }

    public class AzmoonModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string Name { get; set; }

        [Display(Name = "تیتر آزمون")]
        public string Title { get; set; }

        [Display(Name = "توضیحات آزمون")]

        public string Discription { get; set; }

        [Display(Name = "تاریخ شروع")]
        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "تاریخ پایان ")]
        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateExecuteAzmoon { get; set; }

        [Display(Name = "زمان برگزاری آزمون")]
        public string TimeExecuteAzmoon { get; set; }

        [Display(Name = "محل برگزاری ")]
        public string LocationExecuteAzmoon { get; set; }

        [Display(Name = "عنوان کارت ورود به جلسه")]
        public string InputCardTitle { get; set; }


        [Display(Name = "فعال / غیر فعال")]
        public bool Active { get; set; }


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

        [AllowHtml]
        public string SignTextValentear { get; set; }

        [Display(Name = "هزینه آزمون ")]
        public string AmountValue { get; set; }

        [Display(Name = "نوع آزمون ")]
        public PaymentType AzPaymentType { get; set; }
        public bool EveryOneSee { get; set; }
    }

    public class AzmoonEditModel
    {
        public int AzmoonId { get; set; }

        [Display(Name = "عنوان آزمون")]
        public string Name { get; set; }

        [Display(Name = "تیتر آزمون")]
        public string Title { get; set; }

        [Display(Name = "توضیحات آزمون")]

        public string Discription { get; set; }

        [Display(Name = "تاریخ شروع")]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان ")]
      
        public string EndDate { get; set; }
        public string DateExecuteAzmoon { get; set; }

        [Display(Name = "زمان برگزاری آزمون")]
        public string TimeExecuteAzmoon { get; set; }

        [Display(Name = "محل برگزاری ")]
        public string LocationExecuteAzmoon { get; set; }

        [Display(Name = "عنوان کارت ورود به جلسه")]
        public string InputCardTitle { get; set; }


        [Display(Name = "فعال / غیر فعال")]
        public bool Active { get; set; }


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

        [AllowHtml]
        public string SignTextValentear { get; set; }

        [Display(Name = "هزینه آزمون ")]
        public string AmountValue { get; set; }

        [Display(Name = "نوع آزمون ")]
        public PaymentType AzPaymentType { get; set; }
        public bool EveryOneSee { get; set; }
    }

    public class AzmoonCardModel
    {

        public int AzmoonId { get; set; }

        [Display(Name = "تاریخ برگزاری آزمون")]
        public string DateExecuteAzmoon { get; set; }

        [Display(Name = "زمان برگزاری آزمون")]
        public string TimeExecuteAzmoon { get; set; }

        [Display(Name = "محل برگزاری ")]
        public string LocationExecuteAzmoon { get; set; }

        [Display(Name = "عنوان کارت ورود به جلسه")]
        public string InputCardTitle { get; set; }
    }
}
