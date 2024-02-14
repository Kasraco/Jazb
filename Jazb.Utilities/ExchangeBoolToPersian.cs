using System;

namespace Jazb.Utilities
{
    public static class ExchangeBoolToPersian
    {
        public static string BooleanToPersian(bool? value)
        {
            return !Convert.ToBoolean(value) ? "آزاد" : "مسدود";
        }
    }
}