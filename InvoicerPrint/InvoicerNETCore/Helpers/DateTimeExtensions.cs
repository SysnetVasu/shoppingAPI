using System;
using System.Globalization;

namespace InvoicePrintFormat.Helpers
{
    public static class DateTimeExtensions
    {
        public static string GetShortYear(this DateTime date)
        {
            return date.Year.ToString().Substring(2);
        }

        public static int GetWeekNumber(this DateTime date)
        {
            return date.GetWeekNumber(CultureInfo.CurrentCulture.Name);
        }

        public static int GetWeekNumber(this DateTime date, string culture)
        {
            CultureInfo info = new CultureInfo(culture);
            return info.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
