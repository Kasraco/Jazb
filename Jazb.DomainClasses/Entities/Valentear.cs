using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.DomainClasses.Entities
{
    public class Valentear
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public virtual Azmoon Azmoon { get; set; }


        public virtual string ChairCode { get; set; }

        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        [Display(Name = "نام")]
        [Required]
        public virtual string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required]
        public virtual string LastName { get; set; }

        [Display(Name = "نام پدر")]
        [Required]
        public virtual string FatherName { get; set; }

        [Display(Name = "جنسیت")]
        public virtual int GenderId { get; set; }

        [Display(Name = "دین")]
        public virtual int ReligionId { get; set; }

        [Display(Name = "مذهب")]
        public virtual int FaithId { get; set; }


        [Display(Name = "شماره شناسنامه")]
        [MaxLength(15, ErrorMessage = "تعداد ارقام شماره شناسنامه بیش از حد مجاز می باشد")]
        public virtual string BirthCertificateNo { get; set; }

        public virtual decimal BirthCertificateNoID { get; set; }

        [Display(Name = "کد ملی (بدون خط تیره)")]
        //////[NationalCode(ErrorMessage = "لطفا کد ملی را به صورت صحیح وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد ارقام کد ملی بیش از حد مجاز می باشد")]
        public virtual string NationalID { get; set; }

        [Display(Name = "وضعیت تاهل")]
        public virtual int MarriageStatusId { get; set; }

        [Display(Name = "شهرستان محل صدور ")]
        public virtual string IssuancePlace_City { get; set; }


        [Display(Name = "محل تولد")]
        public virtual string CityPart_BirthPlace { get; set; }



        [Display(Name = "تاریخ تولد")]
        public virtual string BirthDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public virtual int DegreeId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public virtual int EducatedSkillId { get; set; }


        [Display(Name = "گرایش تحصیلی")]
        public virtual string EducatedField { get; set; }



        [Display(Name = "رشته شغلی")]
        public virtual int JobId { get; set; }


        [Display(Name = "محل مورد تقاضا")]
        public virtual long PlaceId { get; set; }



        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی")]
        public virtual string LicenseDate { get; set; }

        [Display(Name = "استان محل تحصیل")]
        public virtual string UniversityProvinceTitle { get; set; }
        
        [Display(Name = "نام دانشگاه محل تحصیل")]
        public virtual string University_Name { get; set; }

        [Display(Name = "محل اخذ دیپلم")]
        public virtual string SchoolName { get; set; }

        [Display(Name = "معدل")]
        [Required(ErrorMessage = "معدل الزامی است و باید وارد شود")]
        public virtual string Average { get; set; }


        [Display(Name = "درصد جانبازی")]
        public virtual string WoundedPercent { get; set; }

        [Display(Name = "ماه")]
        public virtual string FightInWarMonth { get; set; }
        [Display(Name = "روز")]
        public virtual string FightInWarDay { get; set; }
        [Display(Name = "سال")]
        public virtual string FightInWarYear { get; set; }


        [Display(Name = "ماه")]
        public virtual string CaptivationMonth { get; set; }
        [Display(Name = "روز")]
        public virtual string CaptivationDay { get; set; }
        [Display(Name = "سال")]
        public virtual string CaptivationYear { get; set; }


        public virtual int MaxDevotionType { get; set; }
        public virtual int SumDevotionType { get; set; }
        public string TextDevotionType { get; set; }

        public virtual int MaxQoutaType { get; set; }
        public virtual int SumQoutaType { get; set; }
        public string TextQoutaType { get; set; }

        public virtual AutochthonType AutochthonType { get; set; }
     

        [Display(Name = "وضعیت نظام وظیفه")]
        public virtual int ConscriptStatusId { get; set; }


        [Display(Name = "استان محل سکونت")]
        public virtual string Address_ProvinceName { get; set; }

        [Display(Name = "شهرستان محل سکونت")]
        public virtual string Address_CityName { get; set; }

        [Display(Name = "بخش محل سکونت")]
        public virtual string Address_ZoneName { get; set; }

        [Display(Name = "روستای محل سکونت")]
        public virtual string Address_VillageName { get; set; }


        [Display(Name = "خانه بهداشت")]
        public virtual string hygiene_Home { get; set; }


        [Display(Name = "آدرس محل سکونت")]
        public virtual string Adress_Adress { get; set; }



        [Required(ErrorMessage = "وارد کردن کد پستی الزامی است (1234567890) ")]
        [Display(Name = "کد پستی (1234567890) ")]
        [RegularExpression(@"^\d{5}(\d{5})?$", ErrorMessage = " لطفا کد پستی را به صورت صحیح وارد کنید(1234567890) ")]
        public virtual string PostalCode { get; set; }



        [Display(Name = "شماره تلفن ثابت ")]
        [RegularExpression(@"((\(\d{3,4}\) ?)|(\d{3,4}))?\d{8}", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Tel { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [RegularExpression(@"^09\d{2}\s*?\d{3}\s*?\d{4}$", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Mobile { get; set; }        

        [Display(Name = "شماره تلفن برای تماس ضروری ")]
        public virtual string TellNeed { get; set; }


        [Display(Name = "وضعیت طرح")]
        public virtual int PlanStatusId { get; set; }

        [Display(Name = "محل خدمت طرح")]
        public virtual string PlaceKhedmat { get; set; }



        [Display(Name = "تعداد سال طرح")]
        public virtual int? PlanYear { get; set; }

        [Display(Name = "تعداد ماه طرح")]
        public virtual int? PlanMonth { get; set; }

        [Display(Name = "تعداد روز طرح")]
        public virtual int? PlanDay { get; set; }


        public virtual int CooperationHistoryId { get; set; }
        [Display(Name = "تعداد سال همکاری قراردادی")]
        public virtual int CooperationYear { get; set; }

        [Display(Name = "تعداد ماه همکاری قراردادی")]
        public virtual int CooperationMonth { get; set; }

        [Display(Name = "تعداد روز همکاری قراردادی")]
        public virtual int CooperationDay { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        [Column(TypeName = "datetime2")]
        public DateTime DateRegister { get; set; }

        [Display(Name = "تصویر داوطلب")]
        public virtual byte[] PictureValenteer { get; set; }

        [Display(Name = "تصویر فیش واریزی ")]
        public virtual byte[] FishVariszi { get; set; }

        [Display(Name = "تصویر طرح ")]
        public virtual byte[] Pic_Tarth { get; set; }

        [Display(Name = "تصویر کارت پایان خدمت ")]
        public virtual byte[] Pic_Conscript { get; set; }

        [Display(Name = "تصویر 5 ")]
        public virtual byte[] Pic5 { get; set; }

        [Display(Name = "تصویر 6 ")]
        public virtual byte[] Pic6 { get; set; }

        [Display(Name = "تصویر 7 ")]
        public virtual byte[] Pic7 { get; set; }

        [Display(Name = "تصویر 8 ")]
        public virtual byte[] Pic8 { get; set; }

        public virtual string FormTaahodName { get; set; }



       

        [Column(TypeName = "datetime2")]
        public virtual DateTime LastDateEdit { get; set; }

      

        public virtual int RecordPrinted { get; set; }
        [Column(TypeName = "datetime2")]
        public virtual DateTime LastDatePrinted { get; set; }


        public virtual int EditUser { get; set; } 
        public virtual string CreateUser { get; set; }

        public virtual int Accept { get; set; }
        public virtual int Edit { get; set; }

        public virtual int RecordAbsence { get; set; }

        public virtual int RecordBlocked { get; set; }

        public virtual int RecordUnBlocked { get; set; }

        public virtual int ActionTypeId { get; set; }

        public virtual string Description { get; set; }
        public virtual int score { get; set; }
        public virtual int grade { get; set; }

        public virtual string AmountValue { get; set; }
        public virtual bool IsPaymented { get; set; }

        public virtual string AreaExecuteAzmoon { get; set; }
        public virtual string DetailPlaceKhedmat { get; set; }

        public virtual ICollection<AzmoonDevotionValentear> AzmoonDevotionValentearas { get; set; }
        public virtual ICollection<AzmoonQoutasValentear> AzmoonQoutasValentears { get; set; }

    }
}
