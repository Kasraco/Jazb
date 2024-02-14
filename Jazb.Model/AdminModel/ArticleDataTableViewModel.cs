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
    public class ArticleDataTableViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان خبر الزامی است")]
        [Display(Name = "عنوان خبر")]
        public string Title { get; set; }

        [Required(ErrorMessage = "انتخاب تاریخ خبر الزامی است")]
        [Display(Name = "تاریخ خبر")]
      
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ انقضاء")]
      
        public DateTime ExpireDate { get; set; }

      

        [Display(Name = "آدرس منبع خبر")]
        public string SourceUrl { get; set; }

        [Required(ErrorMessage = "وارد کردن منبع خبر الزامی است")]
        [Display(Name = "منبع خبر")]
        public string SourceTitle { get; set; }

        [Required(ErrorMessage = "وارد کردن چکیده خبر الزامی است")]
        [Display(Name = " چکیده")]
        public string MinBody { get; set; }

        [Required(ErrorMessage = "وارد کردن متن خبر الزامی است")]
        [Display(Name = "متن خبر")]
        [AllowHtml]
        public string Body { get; set; }
        
        [Display(Name = "دسته خبری")]
        public string CategoryName { get; set; }


        [Display(Name = "تصویر خبر")]
        public byte[] Picture { get; set; }


        public int AzmoonId { get; set; }
        
        public string UserName { get; set; }
    }
}
