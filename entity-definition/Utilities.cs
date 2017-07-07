
using System;

namespace yujvidya
{
    public static class Utilities
    {
        public static WeekDay GetWeekDays()
        {
            return WeekDay.Monday | WeekDay.Tuesday | WeekDay.Wednesday | WeekDay.Thursday | WeekDay.Friday;
        }

        public static DateTime GetTtime(int hour, int minute)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
        }
    }
}