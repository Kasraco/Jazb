using System.Data.Entity.Migrations;
using Jazb.Datalayer.Context;
using Jazb.DomainClasses;
using Jazb.DomainClasses.Entities;
using Jazb.Utilities.DateAndTime;
using Jazb.Utilities.Security;
using System;
using System.Data.Entity.Validation;

namespace Jazb.Datalayer.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<JazbDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(JazbDbContext context)
        {
            //context.Roles.AddOrUpdate(x => new { x.Name, x.Description },
            //    new Role { Name = "admin", Description = "مدیرکل" },
            //    new Role { Name = "moderator", Description = "مدیر" },
            //    new Role { Name = "writer", Description = "نویسنده" },
            //    new Role { Name = "editor", Description = "ویرایشگر" },
            //    new Role { Name = "valentearUser", Description = "داوطلب" },
            //    new Role { Name = "mostafa", Description = "mostafa" },
            //    new Role { Name = "user", Description = "کاربر" });

       

            //context.Options.AddOrUpdate(op => new { op.Name, op.Value }, new Option { Name = "SiteUrl", Value = "http://Jazb.ir/" },
            //   new Option { Name = "SiteName", Value = "دانشگاه علوم پزشکی و خدمات بهداشتی درمانی" },
            //   new Option { Name = "SiteKeywords", Value = "دانشگاه علوم پزشکی و خدمات بهداشتی درمانی" },
            //   new Option { Name = "CompanyLogo", Value = "~/Images/Logo.png" },
            //   new Option { Name = "CompanyAddress", Value = "آدرس: دانشگاه علوم پزشکی و خدمات بهداشتی درمانی" },
            //   new Option { Name = "SiteDescription", Value = "" },
            //   new Option { Name = "UsersCanRegister", Value = "true" },
            //   new Option { Name = "AdminEmail", Value = "" },
            //   new Option { Name = "CommentsNotify", Value = "true" },
            //   new Option { Name = "MailServerUrl", Value = "smtp.gmail.com" },
            //   new Option { Name = "MailServerLogin", Value = "Admin@gmail.com" },
            //   new Option { Name = "MailServerPass", Value = "123" },
            //   new Option { Name = "MailServerPort", Value = "587" });



         //   context.Religions.AddOrUpdate(x => new { x.ReligionTitle, x.ReligionCode },
         //        new Religion { ReligionTitle = "مسلمان	", ReligionCode = 1 },
         //        new Religion { ReligionTitle = "مسیحی", ReligionCode = 2 },
         //        new Religion { ReligionTitle = "کلیمی", ReligionCode = 3 },
         //        new Religion { ReligionTitle = "زرتشتی	", ReligionCode = 4 });

         //   context.Faiths.AddOrUpdate(x => new { x.FaithTitle, x.FaithCode },
         //       new Faith { FaithTitle = "شیعه", FaithCode = 11 },
         //       new Faith { FaithTitle = "سنی", FaithCode = 12 }

         //       );


         //   context.degrees.AddOrUpdate(x => new { x.degreeTitle, x.degreeCode },
         //        new degree { degreeTitle = "دیپلم بهیاری", degreeCode = 120 },
         //        new degree { degreeTitle = "کاردانی", degreeCode = 121 },
         //        new degree { degreeTitle = "کارشناسی", degreeCode = 122 },
         //        new degree { degreeTitle = "کارشناسی ارشد", degreeCode = 123 },
         //        new degree { degreeTitle = "دکترای حرفه‌ای (دندانپزشک، داروساز، علوم آزمایشگاه و دامپزشک)", degreeCode = 124 },
         //        new degree { degreeTitle = "دکترای پزشکی", degreeCode = 125 },
         //        new degree { degreeTitle = "Ph.Dدکترای", degreeCode = 126 },
         //        new degree { degreeTitle = "دکترای تخصصی", degreeCode = 127 },
         //        new degree { degreeTitle = "فوق تخصص", degreeCode = 128 }

         //       );


         //   context.DevotionTypes.AddOrUpdate(x => new { x.DevotionTypeTitle, x.Grade },
         //    new DevotionType { DevotionTypeTitle = "ندارد", Grade = 0 },
         //    new DevotionType { DevotionTypeTitle = "جانباز", Grade = 4103 },
         //    new DevotionType { DevotionTypeTitle = "آزاده", Grade = 4102 },
         //    new DevotionType { DevotionTypeTitle = "رزمنده (حداقل 6 ماه حضور داوطلبانه در جبهه)", Grade = 4101 },
         //    new DevotionType { DevotionTypeTitle = "فرزند شهید", Grade = 4096 },
         //    new DevotionType { DevotionTypeTitle = "همسر شهید", Grade = 2048 },
         //    new DevotionType { DevotionTypeTitle = "همسر آزاده (بالای یک سال سابقه اسارت)", Grade = 1024 },
         //    new DevotionType { DevotionTypeTitle = "فرزند آزاده (بالای یک سال سابقه اسارت)", Grade = 512 },
         //    new DevotionType { DevotionTypeTitle = "همسر جانباز (25درصد و بالاتر)", Grade = 256 },
         //    new DevotionType { DevotionTypeTitle = "فرزند جانباز (25درصد و بالاتر)", Grade = 128 },
         //    new DevotionType { DevotionTypeTitle = "همسر رزمنده (حداقل 6 ماه حضور داوطلبانه در جبهه)", Grade = 16 },
         //    new DevotionType { DevotionTypeTitle = "فرزند رزمنده (حداقل 6 ماه حضور داوطلبانه در جبهه)", Grade = 8 },
         //    new DevotionType { DevotionTypeTitle = "فرزند جانباز (زیر 25درصد)", Grade = 4 },
         //    new DevotionType { DevotionTypeTitle = "فرزند آزاده (زیر یک سال سابقه اسارت)", Grade = 2 },
         //    new DevotionType { DevotionTypeTitle = "خواهر شهید", Grade = 64 },
         //    new DevotionType { DevotionTypeTitle = "برادر شهید", Grade = 32 }

         //    );


         //   context.QoutaTypes.AddOrUpdate(x => new { x.QoutaTypeTitle, x.Grade },
         //    new QoutaType { QoutaTypeTitle = "سهمیه آزاد", Grade = 0 },
         //    new QoutaType { QoutaTypeTitle = "معلولین عادی", Grade = 32 },
         //    new QoutaType { QoutaTypeTitle = "بومی محل مورد تقاضا", Grade = 16 },
         //    new QoutaType { QoutaTypeTitle = "بومی استان", Grade = 8 },
         //    new QoutaType { QoutaTypeTitle = "کارکنان قراردادی", Grade = 4 },
         //    new QoutaType { QoutaTypeTitle = "مشمولین خدمت پزشکان و پیراپزشکان", Grade = 2 }
         //    );


         //   context.ConscriptStatuses.AddOrUpdate(x => new { x.ConscriptStatusTitle, x.ConscriptStatusCode },
         //     new ConscriptStatus { ConscriptStatusTitle = "دارای کارت پایان خدمت", ConscriptStatusCode = 1100 },
         //     new ConscriptStatus { ConscriptStatusTitle = "دارای معافیت قانونی دائم", ConscriptStatusCode = 2200 },
         //     new ConscriptStatus { ConscriptStatusTitle = "ندارد(مخصوص خواهران)", ConscriptStatusCode = 0 }
         //    );

         //   context.Jobs.AddOrUpdate(x => new { x.JobTitle, x.JobCode },
         //   new Job { JobTitle = "کارشناس امور روانی", JobCode = 40101 },
         //   new Job { JobTitle = "کارشناس بینایی سنجی", JobCode = 40102 },
         //   new Job { JobTitle = "کارشناس شنوایی سنجی", JobCode = 40103 },
         //   new Job { JobTitle = "بهداشت کار دهان و دندان", JobCode = 40202 },
         //   new Job { JobTitle = "پرستار", JobCode = 40401 },
         //   new Job { JobTitle = "ماما", JobCode = 40402 },
         //   new Job { JobTitle = "بهیار", JobCode = 40403 },
         //   new Job { JobTitle = "کارشناس اتاق عمل", JobCode = 40405 },
         //   new Job { JobTitle = "کاردان اتاق عمل", JobCode = 40406 },
         //   new Job { JobTitle = "کارشناس هوشبری", JobCode = 40407 },
         //   new Job { JobTitle = "کاردان هوشبری", JobCode = 40408 },
         //   new Job { JobTitle = "مسئول امور فوریت های پزشکی", JobCode = 40409 },
         //   new Job { JobTitle = "کارشناس امور توانبخشی", JobCode = 40501 },
         //   new Job { JobTitle = "کارشناس بهداشت محیط", JobCode = 40601 },
         //   new Job { JobTitle = "کاردان بهداشت محیط", JobCode = 40602 },
         //   new Job { JobTitle = "کارشناس حشره شناسی و مبارزه با ناقلین", JobCode = 40603 },
         //   new Job { JobTitle = "کارشناس مبارزه با بیماری ها", JobCode = 40604 },
         //   new Job { JobTitle = "کاردان مبارزه با بیماری ها", JobCode = 40605 },
         //   new Job { JobTitle = "بهورز", JobCode = 40606 },
         //   new Job { JobTitle = "کارشناس بهداشت حرفه ای", JobCode = 40607 },
         //   new Job { JobTitle = "کاردان بهداشت حرفه ای", JobCode = 40608 },
         //   new Job { JobTitle = "کارشناس امور دارویی", JobCode = 40701 },
         //   new Job { JobTitle = "کارشناس تغذیه و رژیم های غذایی", JobCode = 40801 },
         //   new Job { JobTitle = "کارشناس مواد خوردنی، آشامیدنی، آرایشی و بهداشتی", JobCode = 40802 },
         //   new Job { JobTitle = "کارشناس رادیولوژی", JobCode = 40901 },
         //   new Job { JobTitle = "کاردان رادیولوژی", JobCode = 40902 },
         //   new Job { JobTitle = "کارشناس آزمایشگاه", JobCode = 41001 },
         //   new Job { JobTitle = "کاردان آزمایشگاه", JobCode = 41002 },
         //   new Job { JobTitle = "کارشناس امور بیمارستان ها", JobCode = 41101 },
         //   new Job { JobTitle = "مددکار بهداشتی و درمانی", JobCode = 41201 },
         //   new Job { JobTitle = "مسئول پذیرش و مدارک پزشکی", JobCode = 41202 },
         //   new Job { JobTitle = "کارشناس بهداشت خانواده", JobCode = 41301 },
         //   new Job { JobTitle = "کاردان بهداشت خانواده", JobCode = 41302 },
         //   new Job { JobTitle = "پزشک ", JobCode = 41601 },
         //   new Job { JobTitle = "دندانپزشک", JobCode = 41603 });



         //   context.educatedSkills.AddOrUpdate(x => new { x.educatedSkillTitle, x.educatedSkillCode },
         //    new educatedSkill { educatedSkillTitle = "آموزش بهداشت", educatedSkillCode = 1001 },
         //    new educatedSkill { educatedSkillTitle = "آموزش پرستاری", educatedSkillCode = 1002 },
         //    new educatedSkill { educatedSkillTitle = "اتاق عمل", educatedSkillCode = 1003 },
         //    new educatedSkill { educatedSkillTitle = "اعضای مصنوعی و وسایل کمکی", educatedSkillCode = 1004 },
         //    new educatedSkill { educatedSkillTitle = "انگل شناسی", educatedSkillCode = 1005 },
         //    new educatedSkill { educatedSkillTitle = "ایمنی شناسی", educatedSkillCode = 1006 },
         //    new educatedSkill { educatedSkillTitle = "بافت شناسی", educatedSkillCode = 1007 },
         //    new educatedSkill { educatedSkillTitle = "باکتری شناسی", educatedSkillCode = 1008 },
         //    new educatedSkill { educatedSkillTitle = "بهداشت حرفه ای", educatedSkillCode = 1009 },
         //    new educatedSkill { educatedSkillTitle = "بهداشت عمومی", educatedSkillCode = 1010 },
         //    new educatedSkill { educatedSkillTitle = "بهداشت کار دهان و دندان", educatedSkillCode = 1011 },
         //    new educatedSkill { educatedSkillTitle = "بهداشت محیط", educatedSkillCode = 1012 },
         //    new educatedSkill { educatedSkillTitle = "بینایی سنجی", educatedSkillCode = 1013 },
         //    new educatedSkill { educatedSkillTitle = "بیوتکنولوژی", educatedSkillCode = 1014 },
         //    new educatedSkill { educatedSkillTitle = "بیوتکنولوژی پزشکی", educatedSkillCode = 1015 },
         //    new educatedSkill { educatedSkillTitle = "پرستاری", educatedSkillCode = 1016 },
         //    new educatedSkill { educatedSkillTitle = "پرستاری دندانپزشکی", educatedSkillCode = 1017 },
         //    new educatedSkill { educatedSkillTitle = "پزشکی", educatedSkillCode = 1018 },
         //    new educatedSkill { educatedSkillTitle = "تغذیه", educatedSkillCode = 1019 },
         //    new educatedSkill { educatedSkillTitle = "تکنولوژی پرتودرمانی(رادیوتراپی)", educatedSkillCode = 1020 },
         //    new educatedSkill { educatedSkillTitle = "تکنولوژی پرتوشناسی(رادیولوژی)", educatedSkillCode = 1021 },
         //    new educatedSkill { educatedSkillTitle = "حشره شناسی پزشکی و مبارزه با ناقلین", educatedSkillCode = 1022 },
         //    new educatedSkill { educatedSkillTitle = "داروسازی تخصصی", educatedSkillCode = 1023 },
         //    new educatedSkill { educatedSkillTitle = "داروسازی عمومی", educatedSkillCode = 1024 },
         //    new educatedSkill { educatedSkillTitle = "دامپزشکی", educatedSkillCode = 1025 },
         //    new educatedSkill { educatedSkillTitle = "دیپلم بهیاری", educatedSkillCode = 1026 },
         //    new educatedSkill { educatedSkillTitle = "روان شناسی بالینی", educatedSkillCode = 1027 },
         //    new educatedSkill { educatedSkillTitle = "روان شناسی عمومی", educatedSkillCode = 1028 },
         //    new educatedSkill { educatedSkillTitle = "روانشناسی تربیتی", educatedSkillCode = 1029 },
         //    new educatedSkill { educatedSkillTitle = "زیست شناسی", educatedSkillCode = 1030 },
         //    new educatedSkill { educatedSkillTitle = "ژنتیک انسانی", educatedSkillCode = 1031 },
         //    new educatedSkill { educatedSkillTitle = "سم شناسی", educatedSkillCode = 1032 },
         //    new educatedSkill { educatedSkillTitle = "شنجش و اندازه گیری روان سنجی", educatedSkillCode = 1033 },
         //    new educatedSkill { educatedSkillTitle = "شنوایی سنجی", educatedSkillCode = 1034 },
         //    new educatedSkill { educatedSkillTitle = "شنوایی شناسی", educatedSkillCode = 1035 },
         //    new educatedSkill { educatedSkillTitle = "شیمی", educatedSkillCode = 1036 },
         //    new educatedSkill { educatedSkillTitle = "شیمی علوم آزمایشگاهی", educatedSkillCode = 1037 },
         //    new educatedSkill { educatedSkillTitle = "شیمی کاربردی", educatedSkillCode = 1038 },
         //    new educatedSkill { educatedSkillTitle = "علوم آزمایشگاهی", educatedSkillCode = 1039 },
         //    new educatedSkill { educatedSkillTitle = "علوم آزمایشگاهی دامپزشکی", educatedSkillCode = 1040 },
         //    new educatedSkill { educatedSkillTitle = "علوم اجتماعی(خدمات اجتماعی)", educatedSkillCode = 1041 },
         //    new educatedSkill { educatedSkillTitle = "علوم بهداشتی در تغذیه", educatedSkillCode = 1042 },
         //    new educatedSkill { educatedSkillTitle = "علوم تربیتی(آموزش و پرورش کودکان عقب مانده ذهنی)", educatedSkillCode = 1043 },
         //    new educatedSkill { educatedSkillTitle = "علوم تغذیه", educatedSkillCode = 1044 },
         //    new educatedSkill { educatedSkillTitle = "علوم و صنایع غذایی", educatedSkillCode = 1045 },
         //    new educatedSkill { educatedSkillTitle = "فارماکولوژی", educatedSkillCode = 1046 },
         //    new educatedSkill { educatedSkillTitle = "فوریت های پزشکی", educatedSkillCode = 1047 },
         //    new educatedSkill { educatedSkillTitle = "فیزیک پزشکی", educatedSkillCode = 1048 },
         //    new educatedSkill { educatedSkillTitle = "فیزیوتراپی", educatedSkillCode = 1049 },
         //    new educatedSkill { educatedSkillTitle = "فیزیولوژی ژنتیک", educatedSkillCode = 1050 },
         //    new educatedSkill { educatedSkillTitle = "قارچ شناسی", educatedSkillCode = 1051 },
         //    new educatedSkill { educatedSkillTitle = "قارچ شناسی پزشکی", educatedSkillCode = 1052 },
         //    new educatedSkill { educatedSkillTitle = "کاردرمانی(جسمانی و روانی)", educatedSkillCode = 1053 },
         //    new educatedSkill { educatedSkillTitle = "کارشناس مطالعات خانواده", educatedSkillCode = 1054 },
         //    new educatedSkill { educatedSkillTitle = "کودکان استثنایی(عقب مانده ذهنی)", educatedSkillCode = 1055 },
         //    new educatedSkill { educatedSkillTitle = "گفتاردرمانی", educatedSkillCode = 1056 },
         //    new educatedSkill { educatedSkillTitle = "مامایی", educatedSkillCode = 1057 },
         //    new educatedSkill { educatedSkillTitle = "مدارک پزشکی", educatedSkillCode = 1058 },
         //    new educatedSkill { educatedSkillTitle = "مددکار اجتماعی", educatedSkillCode = 1059 },
         //    new educatedSkill { educatedSkillTitle = "مدیریت توانبخشی", educatedSkillCode = 1060 },
         //    new educatedSkill { educatedSkillTitle = "مدیریت خدمات بهداشتی درمانی", educatedSkillCode = 1061 },
         //    new educatedSkill { educatedSkillTitle = "مدیریت خدمات پرستاری", educatedSkillCode = 1062 },
         //    new educatedSkill { educatedSkillTitle = "مشاوره و راهنمایی(راهنمای مشاور)", educatedSkillCode = 1063 },
         //    new educatedSkill { educatedSkillTitle = "مهندسی ایمنی و بازرسی فنی(ایمنی و حفاظت)", educatedSkillCode = 1064 },
         //    new educatedSkill { educatedSkillTitle = "مهندسی پزشکی(گرایش بیوالکتریک)", educatedSkillCode = 1065 },
         //    new educatedSkill { educatedSkillTitle = "مهندسی پلیمر", educatedSkillCode = 1066 },
         //    new educatedSkill { educatedSkillTitle = "مهندسی شیمی", educatedSkillCode = 1067 },
         //    new educatedSkill { educatedSkillTitle = "مهندسی کشاورزی(علوم و صنایع غذایی)", educatedSkillCode = 1068 },
         //    new educatedSkill { educatedSkillTitle = "میکروب شناسی", educatedSkillCode = 1069 },
         //    new educatedSkill { educatedSkillTitle = "ویروس شناسی", educatedSkillCode = 1070 },
         //    new educatedSkill { educatedSkillTitle = "هماتولوژی آزمایشگاهی و بانک خون", educatedSkillCode = 1071 },
         //    new educatedSkill { educatedSkillTitle = "هوشبری", educatedSkillCode = 1072 },
         //    new educatedSkill { educatedSkillTitle = "آسیب شناسی", educatedSkillCode = 5001 },
         //    new educatedSkill { educatedSkillTitle = "ارتوپدی", educatedSkillCode = 5002 },
         //    new educatedSkill { educatedSkillTitle = "اطفال", educatedSkillCode = 5003 },
         //    new educatedSkill { educatedSkillTitle = "اورولوژی", educatedSkillCode = 5004 },
         //    new educatedSkill { educatedSkillTitle = "بیماریهای داخلی", educatedSkillCode = 5005 },
         //    new educatedSkill { educatedSkillTitle = "بیماریهای عفونی و گرمسیری", educatedSkillCode = 5006 },
         //    new educatedSkill { educatedSkillTitle = "پرتو درمانی", educatedSkillCode = 5007 },
         //    new educatedSkill { educatedSkillTitle = "پزشکی اجتماعی", educatedSkillCode = 5008 },
         //    new educatedSkill { educatedSkillTitle = "پزشکی قانونی", educatedSkillCode = 5009 },
         //    new educatedSkill { educatedSkillTitle = "پزشکی هسته ای", educatedSkillCode = 5010 },
         //    new educatedSkill { educatedSkillTitle = "پوست", educatedSkillCode = 5011 },
         //    new educatedSkill { educatedSkillTitle = "جراحی عمومی", educatedSkillCode = 5012 },
         //    new educatedSkill { educatedSkillTitle = "جراحی مغز و اعصاب", educatedSkillCode = 5013 },
         //    new educatedSkill { educatedSkillTitle = "رادیولوژی", educatedSkillCode = 5014 },
         //    new educatedSkill { educatedSkillTitle = "روانپزشکی", educatedSkillCode = 5015 },
         //    new educatedSkill { educatedSkillTitle = "زنان و زایمان", educatedSkillCode = 5016 },
         //    new educatedSkill { educatedSkillTitle = "طب اورژانس", educatedSkillCode = 5017 },
         //    new educatedSkill { educatedSkillTitle = "طب فیزیکی و توانبخشی", educatedSkillCode = 5018 },
         //    new educatedSkill { educatedSkillTitle = "طب کار", educatedSkillCode = 5019 },
         //    new educatedSkill { educatedSkillTitle = "طب ورزشی", educatedSkillCode = 5020 },
         //    new educatedSkill { educatedSkillTitle = "طب هوافضا", educatedSkillCode = 5021 },
         //    new educatedSkill { educatedSkillTitle = "گوش و حلق و بینی", educatedSkillCode = 5022 },
         //    new educatedSkill { educatedSkillTitle = "متخصص بیهوشی", educatedSkillCode = 5023 });

         //   context.Genders.AddOrUpdate(x => new { x.GenderTitle, x.GenderCode },
         //   new Gender { GenderTitle = "زن", GenderCode = 1 },
         //   new Gender { GenderTitle = "مرد", GenderCode = 2 }
         //   );

         //   context.marriageStatuses.AddOrUpdate(x => new { x.marriageStatusTitle, x.marriageStatusCode },
         //   new marriageStatus { marriageStatusTitle = "مجرد", marriageStatusCode = 100 },
         //   new marriageStatus { marriageStatusTitle = "متاهل", marriageStatusCode = 200 }
         //   );

         //   context.PlaneStatuses.AddOrUpdate(x => new { x.PlaneStatusTitle, x.PlaneStatusCode },
         //       new PlaneStatus { PlaneStatusTitle = "ندارد", PlaneStatusCode = 0 },
         //       new PlaneStatus { PlaneStatusTitle = "همکار قراردادی نمی باشد", PlaneStatusCode = 44 },
         //       new PlaneStatus { PlaneStatusTitle = "همکار قراردادی می باشد", PlaneStatusCode = 66 }
         // );

         //   context.cooperationHistories.AddOrUpdate(x => new { x.cooperationHistoryTitle, x.cooperationHistoryCode },
         //       new cooperationHistory { cooperationHistoryTitle="ندارد", cooperationHistoryCode=0 }
         //);




          try
          {
              context.SaveChanges();
          }
          catch (DbEntityValidationException e)
          {
              foreach (var eve in e.EntityValidationErrors)
              {
                  Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                      eve.Entry.Entity.GetType().Name, eve.Entry.State);
                  foreach (var ve in eve.ValidationErrors)
                  {
                      Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                          ve.PropertyName, ve.ErrorMessage);
                  }
              }
              throw;
          }

            context.Users.AddOrUpdate(u => u.UserName, new User
            {
                CreatedDate = DateAndTime.GetDateTime(),
                Email = "Admin@Gmail.com",
                IsBaned = false,
                Password = Encryption.EncryptingPassword("123456"),
                Role = context.Roles.Find(1),
                UserName = "Admin",
                UserMetaData = new UserMetaData()
            });
            context.SaveChanges();
        }
    }
}