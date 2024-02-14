using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    

    public class QoutaTypeViewModel
    {
        public virtual int Id { get; set; }

        [Display(Name = "عنوان نوع سهمیه"), Required(ErrorMessage = "عنوان نوع سهمیه الزامی است")]
        public virtual string QoutaTypeTitle { get; set; }

        [Display(Name = "کد نوع سهمیه"), Required(ErrorMessage = "کد نوع سهمیه الزامی است")]
        public virtual long Grade { get; set; }
    }
}
