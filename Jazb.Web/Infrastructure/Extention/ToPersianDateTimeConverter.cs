using AutoMapper;
using Jazb.Utilities.DateAndTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jazb.Web.Infrastructure.Extention
{
    public class ToPersianDateTimeConverter : ITypeConverter<DateTime, string>
    {
        private readonly bool _fullDateTime;

        public ToPersianDateTimeConverter(bool fullDateTime = true)
        {
            _fullDateTime = fullDateTime;
        }

        public string Convert(ResolutionContext context)
        {
            var dateTime = context.SourceValue;
            if (dateTime == null) return string.Empty;
            var persianDateTime = DateAndTime.ConvertToPersian((DateTime)dateTime);
            var persianFullDateTime = DateAndTime.ConvertToPersian((DateTime)dateTime, "dddd d MMMM yyyy ساعت hh:mm:ss tt");

            return _fullDateTime
                ? string.Format("{0}", persianFullDateTime)
                : string.Format("{0},", persianDateTime);
        }


    }
}