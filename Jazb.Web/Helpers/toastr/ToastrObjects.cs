using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Jazb.Web
{
    public static class Notification
    {
        #region HELPERS
        private static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        private static string booToLowerString(this bool value)
        {
            return value.ToString().ToLower();
        }
        #endregion


        public static string Show(string message, string title = "",
             ToastType type = ToastType.Info,
            Position position = Position.TopRight,
            int timeOut = 2000,
            bool closeButton = true,
            bool progressBar = true,
            bool newestOnTop = true,
            string onclick = null)
        {
            var scriptOption = "<script>";
            scriptOption += "toastr.options = {";

            scriptOption += "'closeButton': '" + closeButton.booToLowerString() + "','debug': false,'newestOnTop': " + newestOnTop.booToLowerString() + ",'progressBar': " + progressBar.booToLowerString() + ",'positionClass': '" + stringValueOf(position) + "','preventDuplicates': false,'onclick': " + (onclick ?? "null") + ",'showDuration': '300','hideDuration': '1000','timeOut': '" + timeOut + "','extendedTimeOut': '1000','showEasing': 'swing','hideEasing': 'linear','showMethod': 'fadeIn','hideMethod': 'fadeOut'";
            scriptOption += "};";
            scriptOption += "toastr['" + stringValueOf(type) + "']('" + message + "', '" + title + "');</script>";

            return scriptOption;
        }
    }

    public enum ToastType
    {

        [Description("success")]
        Success,

        [Description("info")]
        Info,

        [Description("warning")]
        Warning,

        [Description("error")]
        Error,

    }
    public enum Position
    {
        [Description("toast-top-right")]
        TopRight,

        [Description("toast-bottom-right")]
        BottomRight,

        [Description("toast-bottom-left")]
        BottomLeft,

        [Description("toast-top-left")]
        TopLeft,

        [Description("toast-top-full-width")]
        TopFullWidth,

        [Description("toast-bottom-full-width")]
        BottomFullWidth,

        [Description("toast-top-center")]
        TopCenter,

        [Description("toast-bottom-center")]
        BottomCenter
    }
}