using Jazb.Utilities.DateAndTime;
using Persia;
using System;

namespace Jazb.Utilities
{
    public class ConvertToPersian
    {
        public static string ConvertToPersianString(object digit)
        {
            return PersianWord.ToPersianString(digit);
        }

        public static string ConvertToPersianDate(DateTime digit)
        {
            return digit.ToPersianDate();
        }
    }
}