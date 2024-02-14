using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class FAQ
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "الزامی")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الزامی")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "الزامی")]
        [Display(Name = "کد ملی")]
        public string MeliCode { get; set; }

        [Required(ErrorMessage = "الزامی")]
        [Display(Name = "موضوع سئوال")]
        public string Subject { get; set; }

        [Display(Name = "متن سئوال")]
        public string Body { get; set; }

        [Display(Name = "پاسخ به سئوال")]
        public string BodyAnswer { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "تاریخ ایجاد")]
        public DateTime QuestionDate { get; set; }


        public virtual int AzmoonId { get; set; }
    }
}
