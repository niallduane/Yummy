using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using System.Web;

namespace Yummy.Common.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Converts the .net supported date format current culture 
        /// format into JQuery Datepicker format.
        /// </summary>
        /// <param name="html">HtmlHelper object.</param>
        /// <returns>Format string that supported in JQuery Datepicker.</returns>
        public static string ConvertDateFormat(this HtmlHelper html)
        {
            return ConvertDateFormat(html, Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Converts the .net supported date format current culture 
        /// format into JQuery Datepicker format.
        /// </summary>
        /// <param name="html">HtmlHelper object.</param>
        /// <param name="format">Date format supported by .NET.</param>
        /// <returns>Format string that supported in JQuery Datepicker.</returns>
        public static string ConvertDateFormat(this HtmlHelper html, string format)
        {
            /*
             *  Date used in this comment : 5th - Nov - 2009 (Thursday)
             *
             *  .NET    JQueryUI        Output      Comment
             *  --------------------------------------------------------------
             *  d       d               5           day of month(No leading zero)
             *  dd      dd              05          day of month(two digit)
             *  ddd     D               Thu         day short name
             *  dddd    DD              Thursday    day long name
             *  M       m               11          month of year(No leading zero)
             *  MM      mm              11          month of year(two digit)
             *  MMM     M               Nov         month name short
             *  MMMM    MM              November    month name long.
             *  yy      y               09          Year(two digit)
             *  yyyy    yy              2009        Year(four digit)             *
             */

            string currentFormat = format;

            // Convert the date
            currentFormat = currentFormat.Replace("dddd", "DD");
            currentFormat = currentFormat.Replace("ddd", "D");

            // Convert month
            if (currentFormat.Contains("MMMM"))
            {
                currentFormat = currentFormat.Replace("MMMM", "MM");
            }
            else if (currentFormat.Contains("MMM"))
            {
                currentFormat = currentFormat.Replace("MMM", "M");
            }
            else if (currentFormat.Contains("MM"))
            {
                currentFormat = currentFormat.Replace("MM", "mm");
            }
            else
            {
                currentFormat = currentFormat.Replace("M", "m");
            }

            // Convert year
            currentFormat = currentFormat.Contains("yyyy") ? currentFormat.Replace("yyyy", "yy") : currentFormat.Replace("yy", "y");

            return currentFormat;
        }

        /// <summary>
        /// Adds an ordinal to a number i.e. 1st.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string MakeOrdinal(this HtmlHelper helper, int number)
        {
            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number.ToString() + "th";
            }

            switch (number % 10)
            {
                case 1:
                    return number.ToString() + "st";
                case 2:
                    return number.ToString() + "nd";
                case 3:
                    return number.ToString() + "rd";
                default:
                    return number.ToString() + "th";
            }
        }
        /// <summary>
        /// Adds content to a html area similar to a section but can be used in partial views.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="scriptContent"></param>
        /// <param name="pageLocation"></param>
        /// <returns></returns>
        public static IHtmlString HtmlArea(this HtmlHelper htmlHelper, Func<object, object> scriptContent, string pageLocation)
        {
            if (htmlHelper.ViewContext.HttpContext.Items[pageLocation] != null) ((List<Func<object, object>>)htmlHelper.ViewContext.HttpContext.Items[pageLocation]).Add(scriptContent);
            else htmlHelper.ViewContext.HttpContext.Items[pageLocation] = new List<Func<object, object>>() { scriptContent };

            return new HtmlString(String.Empty);
        }

        /// <summary>
        /// Renders a Html Area similar to a section but can be used in partial views
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="pageLocation"></param>
        /// <returns></returns>
        public static IHtmlString RenderArea(this HtmlHelper htmlHelper, string pageLocation)
        {
            if (htmlHelper.ViewContext.HttpContext.Items[pageLocation] != null)
            {
                var resources = (List<Func<object, object>>)htmlHelper.ViewContext.HttpContext.Items[pageLocation];

                foreach (var resource in resources.Where(resource => resource != null))
                {
                    htmlHelper.ViewContext.Writer.Write(resource(null).ToString());
                }
            }
            return new HtmlString(String.Empty);
        }
    }
}
