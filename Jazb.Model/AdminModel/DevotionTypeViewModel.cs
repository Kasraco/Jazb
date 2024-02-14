using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.AdminModel
{
    public class DevotionTypeViewModel
    {
        public virtual int Id { get; set; }

        [Display(Name = "عنوان نوع سهمیه ایثارگری"), Required(ErrorMessage = "عنوان نوع سهمیه ایثارگری الزامی است")]
        public virtual string DevotionTypeTitle { get; set; }

        [Display(Name = "کد نوع سهمیه ایثارگری"), Required(ErrorMessage = "کد نوع سهمیه ایثارگری الزامی است")]
        public virtual long Grade { get; set; }
    }
}
