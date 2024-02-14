using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel.ManageAzmoon
{
    public class ButtonLinksViewModel
    {
        public int AzmoonId { get; set; }
        public bool Active { get; set; }

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

        [Display(Name = "نمایش لینک آزمون برای همه دیده شود یا نه")]
        public bool ShowEveryOneSee { get; set; }
    }
}
