using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global.ExtensionMethods
{
    public static class CommonExtension
    {
        public static bool HasItem<T>(this ICollection<T> list)
        {
            if(list == null)
            {
                return false;
            }
            if(list.Count == 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsEmpty<T>(this ICollection<T> list)
        {
            if (list == null)
            {
                return true;
            }
            if (list.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsNull<T>(this T obj)
        {
            return obj == null;
        }

        public static bool IsNotNull<T>(this T obj)
        {
            return obj != null;
        }

        public static string ToStartDate(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("yyyy-MM-dd 00:00:00.000");
            }
            return DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");
        }

        public static string ToEndDate(this DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("yyyy-MM-dd 23:59:59.999");
            }
            return DateTime.Now.ToString("yyyy-MM-dd 23:59:59.999");
        }
    }
}
