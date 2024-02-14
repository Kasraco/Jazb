using System;
using Persia;

namespace Jazb.Utilities.DateAndTime
{
    public static class  DateAndTime
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static string ConvertToPersian(DateTime dateTime, string mod = "")
        {
            SolarDate solar = Calendar.ConvertToPersian(dateTime);
            return string.IsNullOrEmpty(mod) ? solar.ToString() : solar.ToString(mod);
        }


        public static string ToPersianDate(this DateTime dateTime, string mod = "")
        {
            SolarDate solar = Calendar.ConvertToPersian(dateTime);
            return string.IsNullOrEmpty(mod) ? solar.ToString() : solar.ToString(mod);
        }


        public static DateTime ToGetorgian(this string dateTime, string mod = "")
        {
            string[] strsp = { "/" };
            string[] parts = dateTime.Split(strsp, StringSplitOptions.None);
          
            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            DateTime solar = Calendar.ConvertToGregorian(year, month, day, DateType.Persian);
            return solar;
        }
    }
}