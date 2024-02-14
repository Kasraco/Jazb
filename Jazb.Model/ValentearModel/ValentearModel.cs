using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.ValentearModel
{
    public class ValentearModel
    {
        public virtual int Id { get; set; }
        public virtual int AzmoonId { get; set; }
        public virtual string ChairCode { get; set; }
        public virtual int  ChairCodeint { get; set; }
        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage="نام داوطلب الزامی است")]
        public virtual string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگیداوطلب الزامیاست")]
        public virtual string LastName { get; set; }

        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "نام پدر الزامی است")]
        public virtual string FatherName { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت خود را مشخص کنید")]
        public virtual int GenderId { get; set; }

        [Display(Name = "دین")]
        [Required(ErrorMessage = "لطفا دین خود را مشخص کنید")]
        public virtual int ReligionId { get; set; }

        [Display(Name = "مذهب")]
        [Required(ErrorMessage = "لطفا مذهب خود را مشخص کنید")]
        public virtual int FaithId { get; set; }


        [Display(Name = "شماره شناسنامه")]
        [MaxLength(15, ErrorMessage = "تعداد ارقام شماره شناسنامه بیش از حد مجاز می باشد")]
        [Required(ErrorMessage = "شماره شناسنامه باید وارد شود")]
        public virtual string BirthCertificateNo { get; set; }

        [Display(Name = "کد ملی (بدون خط تیره)")]
        //////[NationalCode(ErrorMessage = "لطفا کد ملی را به صورت صحیح وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد ارقام کد ملی بیش از حد مجاز می باشد")]
        [System.Web.Mvc.Remote("IsNationalId", "Valentear", AdditionalFields="AzmoonId", HttpMethod = "POST", ErrorMessage = "شما قبلا در این آزمون شرکت کرده اید")]
        [Required(ErrorMessage = "کد ملی الزامی است")]
        public string NationalID { get; set; }

        [Display(Name = "وضعیت تاهل")]
        [Required(ErrorMessage = "لطفا وضعیت تاهل خود را مشخص کنید")]
        public virtual int MarriageStatusId { get; set; }

        [Display(Name = "شهرستان محل صدور ")]
        [Required(ErrorMessage = "شهرستان محل صدور شناسنامه را وارد کنید")]
        public virtual string IssuancePlace_City { get; set; }


        [Display(Name = "محل تولد")]
        [Required(ErrorMessage = "محل تولد الزامیاست")]
        public virtual string CityPart_BirthPlace { get; set; }



        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد باید ورد شود")]
        public virtual string BirthDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        [Required(ErrorMessage = "لطفا مقطع تحصیلی خود را انتخاب کنید")]
        public virtual int DegreeId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        [Required(ErrorMessage = "لطفا رشته تحصیلی خود را مشخص کنید")]
        public virtual int EducatedSkillId { get; set; }


        [Display(Name = "گرایش تحصیلی")]
        public virtual string EducatedField { get; set; }



        [Display(Name = "رشته شغلی")]
        [Required(ErrorMessage = "رشته شغلی مورد تقاضای خودر را مشخص کنید")]
        public virtual int JobId { get; set; }


        [Display(Name = "محل مورد تقاضا")]
        [Required(ErrorMessage = "محل مورد تقاضای خود را مشخص کنید")]
        public virtual long PlaceId { get; set; }



        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی")]
        [Required(ErrorMessage = "تاریخ اخذ آخرین مدرک تحصیلی الزامی است")]
        public virtual string LicenseDate { get; set; }

        [Display(Name = "استان محل تحصیل")]
        [Required(ErrorMessage = "لطفا استان محل تحصیل خود را وارد کنید")]
        public virtual string UniversityProvinceTitle { get; set; }

        [Display(Name = "نام دانشگاه محل تحصیل")]
        [Required(ErrorMessage = "نام دانشگاه محل تحصیل خود را وارد کنید")]
        public virtual string University_Name { get; set; }

        [Display(Name = "محل اخذ دیپلم")]
        [Required(ErrorMessage = "محل اخذ دیپلم الزامی است")]
        public virtual string SchoolName { get; set; }

        [Display(Name = "معدل")]
        [Required(ErrorMessage = "معدل الزامی است و باید وارد شود")]
        public virtual string Average { get; set; }

      
        //[Display(Name = "مشمولین خدمت پزشکان و پیراپزشکان")]
        //public virtual bool MashmoolKhedmat { get; set; }

        //[Display(Name = "روز ")]
        //public virtual int? Khedmat_Day { get; set; }

        //[Display(Name = "ماه ")]
        //public virtual int? Khedmat_Month { get; set; }

        //[Display(Name = "سال ")]
        //public virtual int? Khedmat_Year { get; set; }


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

        [Display(Name = "درصد جانبازی")]
        public virtual string WoundedPercent { get; set; }

        [Display(Name = "وضعیت نظام وظیفه")]
        [Required(ErrorMessage="لطفا وضعیت نظام وظیفه خود را مشخص کنید")]
        public virtual int ConscriptStatusId { get; set; }


        [Display(Name = "استان محل سکونت")]
        public virtual string Address_ProvinceName { get; set; }

        [Display(Name = "شهرستان محل سکونت")]
        public virtual string Address_CityName { get; set; }

        [Display(Name = "بخش محل سکونت")]
        public virtual string Address_ZoneName { get; set; }

        [Display(Name = "روستای محل سکونت")]
        public virtual string Address_VillageName { get; set; }

          [Display(Name = "آدرس")]
        public virtual string Adress_Adress { get; set; }

        

        [Required(ErrorMessage = "وارد کردن کد پستی الزامی است (1234567890) ")]
        [Display(Name = "کد پستی (1234567890) ")]
        [RegularExpression(@"^\d{5}(\d{5})?$", ErrorMessage = " لطفا کد پستی را به صورت صحیح وارد کنید(1234567890) ")]
        public virtual string PostalCode { get; set; }


        [Required(ErrorMessage="شماره تلفن الزامی است")]
        [Display(Name = "شماره تلفن ثابت ")]
        [RegularExpression(@"((\(\d{3,4}\) ?)|(\d{3,4}))?\d{8}", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Tel { get; set; }

          [Required(ErrorMessage = "شماره همراه الزامی است")]
        [Display(Name = "شماره تلفن همراه")]
        [RegularExpression(@"^09\d{2}\s*?\d{3}\s*?\d{4}$", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Mobile { get; set; }

        [Display(Name = "شماره تلفن برای تماس ضروری ")]
        public virtual string TellNeed { get; set; }

        [Display(Name = "وضعیت طرح")]
        [Required(ErrorMessage = "لطفا وضعیت طرح خود را مشخص کنید")]
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

        public virtual byte[] Pic_Tarth { get; set; }
        public virtual byte[] Pic_Conscript { get; set; }
        public virtual byte[] Pic5 { get; set; }
        public virtual byte[] Pic6 { get; set; }
        public virtual byte[] Pic7 { get; set; }
        public virtual byte[] Pic8 { get; set; }
        
        public virtual string FormTaahodName { get; set; }

        public virtual int Accept { get; set; }


        [Display(Name = "جنسیت")]
        public virtual string GenderTitle { get; set; }
        [Display(Name = "دین")]
        public virtual string ReligionTitle { get; set; }
        [Display(Name = "مذهب")]
        public virtual string FaithTitle { get; set; }
        [Display(Name = "وضعیت تاهل")]
        public virtual string MarriageStatusTitle { get; set; }
        [Display(Name = "مقطع تحصیلی")]
        public virtual string DegreeTitle { get; set; }
        [Display(Name = "رشته تحصیلی")]
        public virtual string EducatedSkillTitle { get; set; }
        [Display(Name = "رشته شغلی")]
        public virtual string JobTitle { get; set; }
        [Display(Name = "محل مورد تقاضا")]
        public virtual string PlaceTitle { get; set; }
        [Display(Name = "وضعیت طرح")]
        public virtual string PlanStatusTitle { get; set; }
           [Display(Name = "وضعیت نظام وظیفه")]
        public virtual string ConscriptStatusTitle { get; set; }

        public virtual string CooperationHistoryTitle { get; set; }

        public virtual string DetailPlaceKhedmat { get; set; }

        public virtual int RecordBlocked { get; set; }

        public virtual int RecordUnBlocked { get; set; }

        public virtual string Description { get; set; }
       
    }

    public class EditValentearModel
    {
        public virtual int Id { get; set; }
        public virtual int AzmoonId { get; set; }
        public virtual string ChairCode { get; set; }
        public virtual int ChairCodeint { get; set; }
        [Display(Name = "کد رهگیری")]
        public string TrackingCode { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "نام داوطلب الزامی است")]
        public virtual string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگیداوطلب الزامیاست")]
        public virtual string LastName { get; set; }

        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "نام پدر الزامی است")]
        public virtual string FatherName { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا جنسیت خود را مشخص کنید")]
        public virtual int GenderId { get; set; }

        [Display(Name = "دین")]
        [Required(ErrorMessage = "لطفا دین خود را مشخص کنید")]
        public virtual int ReligionId { get; set; }

        [Display(Name = "مذهب")]
        [Required(ErrorMessage = "لطفا مذهب خود را مشخص کنید")]
        public virtual int FaithId { get; set; }


        [Display(Name = "شماره شناسنامه")]
        [MaxLength(15, ErrorMessage = "تعداد ارقام شماره شناسنامه بیش از حد مجاز می باشد")]
        [Required(ErrorMessage = "شماره شناسنامه باید وارد شود")]
        public virtual string BirthCertificateNo { get; set; }

        [Display(Name = "کد ملی (بدون خط تیره)")]
        //////[NationalCode(ErrorMessage = "لطفا کد ملی را به صورت صحیح وارد کنید")]
        [MaxLength(10, ErrorMessage = "تعداد ارقام کد ملی بیش از حد مجاز می باشد")]
        //[System.Web.Mvc.Remote("IsNationalId", "Valentear", AdditionalFields = "AzmoonId", HttpMethod = "POST", ErrorMessage = "شما قبلا در این آزمون شرکت کرده اید")]
        [Required(ErrorMessage = "کد ملی الزامی است")]
        public string NationalID { get; set; }

        [Display(Name = "وضعیت تاهل")]
        [Required(ErrorMessage = "لطفا وضعیت تاهل خود را مشخص کنید")]
        public virtual int MarriageStatusId { get; set; }

        [Display(Name = "شهرستان محل صدور ")]
        [Required(ErrorMessage = "شهرستان محل صدور شناسنامه را وارد کنید")]
        public virtual string IssuancePlace_City { get; set; }


        [Display(Name = "محل تولد")]
        [Required(ErrorMessage = "محل تولد الزامیاست")]
        public virtual string CityPart_BirthPlace { get; set; }



        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "تاریخ تولد باید ورد شود")]
        public virtual string BirthDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        [Required(ErrorMessage = "لطفا مقطع تحصیلی خود را انتخاب کنید")]
        public virtual int DegreeId { get; set; }

        [Display(Name = "رشته تحصیلی")]
        [Required(ErrorMessage = "لطفا رشته تحصیلی خود را مشخص کنید")]
        public virtual int EducatedSkillId { get; set; }


        [Display(Name = "گرایش تحصیلی")]
        public virtual string EducatedField { get; set; }



        [Display(Name = "رشته شغلی")]
        [Required(ErrorMessage = "رشته شغلی مورد تقاضای خودر را مشخص کنید")]
        public virtual int JobId { get; set; }


        [Display(Name = "محل مورد تقاضا")]
        [Required(ErrorMessage = "محل مورد تقاضای خود را مشخص کنید")]
        public virtual long PlaceId { get; set; }



        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی")]
        [Required(ErrorMessage = "تاریخ اخذ آخرین مدرک تحصیلی الزامی است")]
        public virtual string LicenseDate { get; set; }

        [Display(Name = "استان محل تحصیل")]
        [Required(ErrorMessage = "لطفا استان محل تحصیل خود را وارد کنید")]
        public virtual string UniversityProvinceTitle { get; set; }

        [Display(Name = "نام دانشگاه محل تحصیل")]
        [Required(ErrorMessage = "نام دانشگاه محل تحصیل خود را وارد کنید")]
        public virtual string University_Name { get; set; }

        [Display(Name = "محل اخذ دیپلم")]
        [Required(ErrorMessage = "محل اخذ دیپلم الزامی است")]
        public virtual string SchoolName { get; set; }

        [Display(Name = "معدل")]
        [Required(ErrorMessage = "معدل الزامی است و باید وارد شود")]
        public virtual string Average { get; set; }


        //[Display(Name = "مشمولین خدمت پزشکان و پیراپزشکان")]
        //public virtual bool MashmoolKhedmat { get; set; }

        //[Display(Name = "روز ")]
        //public virtual int? Khedmat_Day { get; set; }

        //[Display(Name = "ماه ")]
        //public virtual int? Khedmat_Month { get; set; }

        //[Display(Name = "سال ")]
        //public virtual int? Khedmat_Year { get; set; }


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

        [Display(Name = "درصد جانبازی")]
        public virtual string WoundedPercent { get; set; }

        [Display(Name = "وضعیت نظام وظیفه")]
        [Required(ErrorMessage = "لطفا وضعیت نظام وظیفه خود را مشخص کنید")]
        public virtual int ConscriptStatusId { get; set; }


        [Display(Name = "استان محل سکونت")]
        public virtual string Address_ProvinceName { get; set; }

        [Display(Name = "شهرستان محل سکونت")]
        public virtual string Address_CityName { get; set; }

        [Display(Name = "بخش محل سکونت")]
        public virtual string Address_ZoneName { get; set; }

        [Display(Name = "روستای محل سکونت")]
        public virtual string Address_VillageName { get; set; }

        [Display(Name = "آدرس")]
        public virtual string Adress_Adress { get; set; }



        [Required(ErrorMessage = "وارد کردن کد پستی الزامی است (1234567890) ")]
        [Display(Name = "کد پستی (1234567890) ")]
        [RegularExpression(@"^\d{5}(\d{5})?$", ErrorMessage = " لطفا کد پستی را به صورت صحیح وارد کنید(1234567890) ")]
        public virtual string PostalCode { get; set; }


        [Required(ErrorMessage = "شماره تلفن الزامی است")]
        [Display(Name = "شماره تلفن ثابت ")]
        [RegularExpression(@"((\(\d{3,4}\) ?)|(\d{3,4}))?\d{8}", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Tel { get; set; }

        [Required(ErrorMessage = "شماره همراه الزامی است")]
        [Display(Name = "شماره تلفن همراه")]
        [RegularExpression(@"^09\d{2}\s*?\d{3}\s*?\d{4}$", ErrorMessage = "لطفا شماره تلفن را به صورت صحیح وارد کنید")]
        public virtual string Mobile { get; set; }

        [Display(Name = "شماره تلفن برای تماس ضروری ")]
        public virtual string TellNeed { get; set; }

        [Display(Name = "وضعیت طرح")]
        [Required(ErrorMessage = "لطفا وضعیت طرح خود را مشخص کنید")]
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

        public virtual byte[] Pic_Tarth { get; set; }
        public virtual byte[] Pic_Conscript { get; set; }

        public virtual byte[] Pic5 { get; set; }
        public virtual byte[] Pic6 { get; set; }
        public virtual byte[] Pic7 { get; set; }
        public virtual byte[] Pic8 { get; set; }

        public virtual string FormTaahodName { get; set; }

        public virtual int Accept { get; set; }


        [Display(Name = "جنسیت")]
        public virtual string GenderTitle { get; set; }
        [Display(Name = "دین")]
        public virtual string ReligionTitle { get; set; }
        [Display(Name = "مذهب")]
        public virtual string FaithTitle { get; set; }
        [Display(Name = "وضعیت تاهل")]
        public virtual string MarriageStatusTitle { get; set; }
        [Display(Name = "مقطع تحصیلی")]
        public virtual string DegreeTitle { get; set; }
        [Display(Name = "رشته تحصیلی")]
        public virtual string EducatedSkillTitle { get; set; }
        [Display(Name = "رشته شغلی")]
        public virtual string JobTitle { get; set; }
        [Display(Name = "محل مورد تقاضا")]
        public virtual string PlaceTitle { get; set; }
        [Display(Name = "وضعیت طرح")]
        public virtual string PlanStatusTitle { get; set; }
        [Display(Name = "وضعیت نظام وظیفه")]
        public virtual string ConscriptStatusTitle { get; set; }

        public virtual string CooperationHistoryTitle { get; set; }

        public virtual string DetailPlaceKhedmat { get; set; }

        public virtual int RecordBlocked { get; set; }

        public virtual int RecordUnBlocked { get; set; }

        public virtual string Description { get; set; }

    }
}
