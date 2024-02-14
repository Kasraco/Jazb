using System;
using System.Web.Mvc;
using Jazb.Utilities.DateAndTime;
using Persia;
using System.Collections.Specialized;
using System.Text;
using System.Web.Mvc.Ajax;
using System.Web;
using System.Web.Routing;
using Jazb.Datalayer.Context;
using System.Linq;

namespace Jazb.Web
{
    public static class HtmlHelpers
    {
        public static string GetJobTitle(this HtmlHelper htmlHelper, long value)
        {
            JazbDbContext jdbcon = new JazbDbContext();
            return jdbcon.Jobs.Where(x => x.JobCode == value).Select(x => x.JobTitle).First();
        }

        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }

      

    }
}
namespace Jazb.Web.Helpers
{
    public static class ConverterToPersian
    {


        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }

        public static MvcHtmlString ConvertToPersianString(this HtmlHelper htmlHelper, int digit)
        {
            return MvcHtmlString.Create(PersianWord.ToPersianString(digit));
        }

        public static MvcHtmlString ConvertToPersianString(this HtmlHelper htmlHelper, string str)
        {
            return MvcHtmlString.Create(PersianWord.ToPersianString(str == null ? "": str));
        }

        public static MvcHtmlString ConvertToPersianDateTime(this HtmlHelper htmlHelper, DateTime dateTime,
            string mode = "")
        {
            return dateTime.Year == 1 ? null : MvcHtmlString.Create(DateAndTime.ConvertToPersian(dateTime, mode));
        }

        public static string ConvertBooleanToPersian(this HtmlHelper htmlHelper, bool? value)
        {
            return !Convert.ToBoolean(value) ? "آزاد" : "مسدود";
        }

        public static string ConvertBooleanToPersian(this HtmlHelper htmlHelper, bool value)
        {
            return !Convert.ToBoolean(value) ? "آزاد" : "مسدود";
        }

        public static string ConvertBooleanToPersian2(this HtmlHelper htmlHelper, bool value)
        {
            return !Convert.ToBoolean(value) ? "خیر" : "بلی";
        }




    }

    public static class HttpHelper
    {
        #region PreparePOSTForm
        /// <summary>
        /// کار این متد این است که یک فرم ایجاد کرده و اطلاعات پرداخت را به درگاه بانک ارسال می کنده
        /// This method prepares an Html form which holds all data in hidden field in the addetion to form submitting script.
        /// </summary>
        /// <param name="url">The destination Url to which the post and redirection will occur, the Url can be in the same App or ouside the App.</param>
        /// <param name="data">A collection of data that will be posted to the destination Url.</param>
        /// <returns>Returns a string representation of the Posting form.</returns>
        public static String PreparePOSTForm(string url, NameValueCollection data)
        {
            //Set a name for the form
            string formID = "PostForm";

            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
            foreach (string key in data)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
            }
            strForm.Append("</form>");

            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." + formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");

            //Return the form and the script concatenated. (The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        #endregion
    }
}