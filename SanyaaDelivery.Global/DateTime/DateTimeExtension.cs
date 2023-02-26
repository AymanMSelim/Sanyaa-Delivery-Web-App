using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.DateTimeHelper
{
    public static class DateTimeExtension
    {
        public static DateTime ToEgyptTime(this DateTime time)
        {
            DateTimeHelper.DateTimeWithZone zoneTime = new DateTimeWithZone(time);
            return zoneTime.CairoTime;
        }

        public static DateTime EgyptTimeNow(this DateTime time)
        {
            DateTimeHelper.DateTimeWithZone zoneTime = new DateTimeWithZone(DateTime.Now);
            return zoneTime.CairoTime;
        }
    }
}
