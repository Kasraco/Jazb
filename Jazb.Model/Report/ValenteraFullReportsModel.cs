using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jazb.Model.Report
{
    public class ValenteraFullReportsModel
    {
        [Display(Name = "عنوان آزمون")]
        public string AzmoonName { get; set; }

        [Display(Name = "شماره صندلی")]
        public int intChaireCode { get; set; }

        private string _ChaireCode;

        public string ChaireCode
        {
            get { return _ChaireCode; }
            set
            {
                _ChaireCode = value;
                intChaireCode = int.Parse(string.IsNullOrEmpty(value) ? "0" : value);
            }
        }


        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [Display(Name = "جنسیت")]
        public string GenderName { get; set; }

        [Display(Name = "دین")]
        public string ReligionTitle { get; set; }

        [Display(Name = "مذهب")]
        public string FaithTitle { get; set; }

        [Display(Name = "شماره شناسنامه")]
        public string BirthCertificateNo { get; set; }

        [Display(Name = "کد ملی ")]
        public string NationalID { get; set; }

        [Display(Name = "وضعیت نظام وظیفه")]
        public string ConscriptStatusTitle { get; set; }

        [Display(Name = "وضعیت تاهل")]
        public string MarriageStatusTitle { get; set; }

        [Display(Name = "شهرستان محل صدور ")]
        public string IssuancePlace_City { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string DegreeTitle { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string EducatedSkillTitle { get; set; }

        [Display(Name = "گرایش تحصیلی")]
        public string EducatedField { get; set; }

        [Display(Name = "رشته شغلی")]
        public string JobName { get; set; }

        [Display(Name = "محل مورد تقاضا")]
        public string PlaceName { get; set; }

        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی")]
        public string LicenseDate { get; set; }

        [Display(Name = "استان محل تحصیل")]
        public string UniversityProvinceTitle { get; set; }

        [Display(Name = "نام دانشگاه محل تحصیل")]
        public string University_Name { get; set; }

        [Display(Name = "محل اخذ دیپلم")]
        public string SchoolName { get; set; }

        [Display(Name = "معدل")]
        public string Average { get; set; }

        [Display(Name = "امتیاز ایثارگری")]
        public int MaxDevotionType { get; set; }

        [Display(Name = "جمع امتیازات ایتارگری")]
        public int SumDevotionType { get; set; }

        [Display(Name = "ایثارگری")]
        public string TextDevotionType { get; set; }

        [Display(Name = "امتیاز سهمیه")]
        public int MaxQoutaType { get; set; }

        [Display(Name = "جمع امتیاز سهمیه")]
        public int SumQoutaType { get; set; }

        [Display(Name = "سهمیه")]
        public string TextQoutaType { get; set; }
      

        [Display(Name = "استان محل سکونت")]
        public  string Address_ProvinceName { get; set; }

        [Display(Name = "شهرستان محل سکونت")]
        public string Address_CityName { get; set; }

        [Display(Name = "بخش محل سکونت")]
        public string Address_ZoneName { get; set; }

     
        [Display(Name = "آدرس محل سکونت")]
        public string Adress_Adress { get; set; }



      
        [Display(Name = "کد پستی  ")]     
        public string PostalCode { get; set; }



        [Display(Name = "شماره تلفن ثابت ")]
        public string Tel { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "شماره تلفن برای تماس ضروری ")]
        public string TellNeed { get; set; }

        [Display(Name = "وضعیت طرح")]
        public string PlanStatusTitle { get; set; }

        [Display(Name = "محل خدمت طرح")]
        public string PlaceKhedmat { get; set; }

        [Display(Name = "تعداد سال طرح")]
        public int? PlanYear { get; set; }

        [Display(Name = "تعداد ماه طرح")]
        public int? PlanMonth { get; set; }

        [Display(Name = "تعداد روز طرح")]
        public int? PlanDay { get; set; }

        [Display(Name = "وضعیت همکاری")]
        public string CooperationHistoryTitle { get; set; }

        [Display(Name = "تعداد سال همکاری قراردادی")]
        public  int CooperationYear { get; set; }

        [Display(Name = "تعداد ماه همکاری قراردادی")]
        public int CooperationMonth { get; set; }

        [Display(Name = "تعداد روز همکاری قراردادی")]
        public int CooperationDay { get; set; }

        [Display(Name = "تاریخ ثبت نام")] 
        public DateTime DateRegister { get; set; }


        [Display(Name = "وضعیت داوطلب")]
        public virtual int Accept { get; set; }

        [Display(Name = "توضیح")]
        public virtual string Description { get; set; }

       

        [Display(Name = "امتیاز")]
        public virtual int score { get; set; }

        [Display(Name = "رتبه")]
        public virtual int grade { get; set; }

        [Display(Name = "محل آزمون")]
        public virtual string AzmoonLocation { get; set; }

    }

    public class ValenteraFullWithNumberReportsModel
    {
        [Display(Name = "عنوان آزمون")]
        public string AzmoonName { get; set; }

        [Display(Name = "شماره صندلی")]
        public int intChaireCode { get; set; }

        private string _ChaireCode;

        public string ChaireCode
        {
            get { return _ChaireCode; }
            set
            {
                _ChaireCode = value;
                intChaireCode = int.Parse(string.IsNullOrEmpty(value) ? "0" : value);
            }
        }


        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [Display(Name = "جنسیت")]
        public string GenderName { get; set; }

        [Display(Name = "دین")]
        public string ReligionTitle { get; set; }

        [Display(Name = "مذهب")]
        public string FaithTitle { get; set; }

        [Display(Name = "شماره شناسنامه")]
        public string BirthCertificateNo { get; set; }

        [Display(Name = "کد ملی ")]
        public string NationalID { get; set; }

        [Display(Name = "وضعیت نظام وظیفه")]
        public string ConscriptStatusTitle { get; set; }

        [Display(Name = "وضعیت تاهل")]
        public string MarriageStatusTitle { get; set; }

        [Display(Name = "شهرستان محل صدور ")]
        public string IssuancePlace_City { get; set; }

        [Display(Name = "تاریخ تولد")]
        public string BirthDate { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string DegreeTitle { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string EducatedSkillTitle { get; set; }

        [Display(Name = "گرایش تحصیلی")]
        public string EducatedField { get; set; }

        [Display(Name = "رشته شغلی")]
        public string JobName { get; set; }

        [Display(Name = "محل مورد تقاضا")]
        public string PlaceName { get; set; }

        [Display(Name = "تاریخ اخذ آخرین مدرک تحصیلی")]
        public string LicenseDate { get; set; }

        [Display(Name = "استان محل تحصیل")]
        public string UniversityProvinceTitle { get; set; }

        [Display(Name = "نام دانشگاه محل تحصیل")]
        public string University_Name { get; set; }

        [Display(Name = "محل اخذ دیپلم")]
        public string SchoolName { get; set; }

        [Display(Name = "معدل")]
        public string Average { get; set; }

        [Display(Name = "امتیاز ایثارگری")]
        public int MaxDevotionType { get; set; }

        [Display(Name = "جمع امتیازات ایتارگری")]
        public int SumDevotionType { get; set; }

        [Display(Name = "ایثارگری")]
        public string TextDevotionType { get; set; }

        [Display(Name = "امتیاز سهمیه")]
        public int MaxQoutaType { get; set; }

        [Display(Name = "جمع امتیاز سهمیه")]
        public int SumQoutaType { get; set; }

        [Display(Name = "سهمیه")]
        public string TextQoutaType { get; set; }


        [Display(Name = "استان محل سکونت")]
        public string Address_ProvinceName { get; set; }

        [Display(Name = "شهرستان محل سکونت")]
        public string Address_CityName { get; set; }

        [Display(Name = "بخش محل سکونت")]
        public string Address_ZoneName { get; set; }


        [Display(Name = "آدرس محل سکونت")]
        public string Adress_Adress { get; set; }




        [Display(Name = "کد پستی  ")]
        public string PostalCode { get; set; }



        [Display(Name = "شماره تلفن ثابت ")]
        public string Tel { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "شماره تلفن برای تماس ضروری ")]
        public string TellNeed { get; set; }

        [Display(Name = "وضعیت طرح")]
        public string PlanStatusTitle { get; set; }

        [Display(Name = "محل خدمت طرح")]
        public string PlaceKhedmat { get; set; }

        [Display(Name = "تعداد سال طرح")]
        public int? PlanYear { get; set; }

        [Display(Name = "تعداد ماه طرح")]
        public int? PlanMonth { get; set; }

        [Display(Name = "تعداد روز طرح")]
        public int? PlanDay { get; set; }

        [Display(Name = "وضعیت همکاری")]
        public string CooperationHistoryTitle { get; set; }

        [Display(Name = "تعداد سال همکاری قراردادی")]
        public int CooperationYear { get; set; }

        [Display(Name = "تعداد ماه همکاری قراردادی")]
        public int CooperationMonth { get; set; }

        [Display(Name = "تعداد روز همکاری قراردادی")]
        public int CooperationDay { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime DateRegister { get; set; }


        [Display(Name = "وضعیت داوطلب")]
        public virtual int Accept { get; set; }

        [Display(Name = "توضیح")]
        public virtual string Description { get; set; }

        [Display(Name = "نمره عمومی")]
        public virtual string GeneralNumber { get; set; }

        [Display(Name = "نمره تخصصی")]
        public virtual string TechnicalNumber { get; set; }


        [Display(Name = "امتیاز")]
        public virtual int score { get; set; }

        [Display(Name = "رتبه")]
        public virtual int grade { get; set; }

    }

    public class ValenteraFullAcceptPasokhnamehReportsModel
    {

        [Display(Name = "کد ملی")]
        public string NatinalID { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "مدرسه")]
        public string AzmoonName { get; set; }

        [Display(Name = "مقطع تحصیلی")]
        public string DegreeTitle { get; set; }

        [Display(Name = "پایه")]
        public string BaseClass { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public string EducatedSkillTitle { get; set; }
        
        [Display(Name = "کلاس")]
        public string JobName { get; set; }

        [Display(Name = "شماره صندلی")]
        public int intChaireCode { get; set; }

        private string _ChaireCode;

        public string ChaireCode
        {
            get { return _ChaireCode; }
            set
            {
                _ChaireCode = value;
                intChaireCode = int.Parse(string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }


        [Display(Name = "تصویر")]
        public string PictureValen { get; set; }

        [Display(Name = "شماره همراه")]
        public string MobileNumber { get; set; }

        [Display(Name = "تصویردوم")]
        public byte[] PictureValentear { get; set; }









    }

    public class ValenteraFullAcceptPictureValentearModel
    {



        [Display(Name = "تصویر")]
        public string PictureValen { get; set; }

        [Display(Name = "تصویردوم")]
        public byte[] PictureValentear { get; set; }









    }


    public class ValenteraFullResultAnswerReportModel
    {
        [Display(Name = "شماره صندلی")]
        public int intChaireCode { get; set; }

        private string _ChaireCode;

        public string ChaireCode
        {
            get { return _ChaireCode; }
            set
            {
                _ChaireCode = value;
                intChaireCode = int.Parse(string.IsNullOrEmpty(value) ? "0" : value);
            }
        }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

      
        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [Display(Name = "کد ملی ")]
        public string NationalID { get; set; }

        [Display(Name = "رشته شغلی")]
        public string JobName { get; set; }

        [Display(Name = "محل مورد تقاضا")]
        public string PlaceName { get; set; }


        [Display(Name = "ظرفیت")]
        public int Capacity { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "ایثارگری")]
        public string TextDevotionType { get; set; }              

        [Display(Name = "سهمیه")]
        public string TextQoutaType { get; set; }
             
        [Display(Name = "نمره عمومی")]
        public virtual double GeneralFinalScore { get; set; }
        [Display(Name = "نمره تخصصی")]
        public virtual double TechnicalFinalScore { get; set; }
        [Display(Name = "نمره کل")]
        public virtual double FinalScore { get; set; }


        [Display(Name = "امتیاز")]
        public virtual int score { get; set; }

        [Display(Name = "رتبه")]
        public virtual int grade { get; set; }


        [Display(Name = "وضعیت آزمون داوطلب")]
        public virtual string StateResult { get; set; }








    }
}
