using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Jazb.DomainClasses.Entities
{
    public class article
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن عنوان خبر الزامی است")]
        [Display(Name = "عنوان خبر")]
        public string Title { get; set; }

        [Required(ErrorMessage = "انتخاب تاریخ خبر الزامی است")]
        [Display(Name = "تاریخ خبر")]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ انقضاء")]
        [Column(TypeName = "datetime2")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "تاریخ انقضاء ندارد")]
        public bool HasExpire { get; set; }

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


        //[ForeignKey("CategoryId")]
        //public virtual category Category { get; set; }

        //[Required(ErrorMessage = "انتخاب دسته خبر الزامی است")]
        //[Display(Name = "دسته خبری")]
        //public int CategoryId { get; set; }


        [Display(Name = "تصویر خبر")]
        public byte[] Picture { get; set; }

        [Display(Name = "فایل پیوست")]
        public byte[] AttachmentFile { get; set; }
        public string AttachmentFiletype { get; set; }
        public string AttachmentFileName { get; set; }


        public int AzmoonId { get; set; }








        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int UserId { get; set; }
    }
}
