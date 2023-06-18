using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.DateTimeHelper
{
    public class DateTimeHelperService
    {
        public DateTime GetStartDateOfMonth()
        {
            return GetStartDateOfMonthS();
        }
        
        public DateTime GetEndDateOfMonth()
        {
            return GetEndDateOfMonthS();
        }
        public static DateTime GetStartDateOfMonthS(DateTime? date = null)
        {
            if(date == null)
            {
                date = DateTime.Now;
            }
           return new DateTime(date.Value.Year, date.Value.Month, 1, 0, 0, 0);
        }

        public static DateTime GetStartTimeOfDayS(DateTime? date = null)
        {
            if (date == null)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            }
            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 0, 0, 0);
        }

        public static DateTime GetStartWorkingDayTimeS(DateTime? date = null, int startHour = 9)
        {
            if (date == null)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, startHour, 0, 0);
            }
            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, startHour, 0, 0);
        }

        public static DateTime GetEndTimeOfDayS(DateTime? date = null)
        {
            if (date == null)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 57);
            }
            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 23, 59, 57);
        }

        public static DateTime GetEndWorkingDayTimeS(DateTime? date = null)
        {
            if (date == null)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 00, 00);
            }
            return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 21, 00, 00);
        }

        public static DateTime GetEndDateOfMonthS(DateTime? date = null)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            return new DateTime(date.Value.Year, date.Value.Month, DateTime.DaysInMonth(date.Value.Year, date.Value.Month), 23, 59, 57);
        }
    }
}
