using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace yujvidya
{
    public enum NotifyInteralType
    {
        Invalid = 0,
        Daily = 1,
    }

    public static class Extension
    {
        public static string ToCronExpression(this NotifyInteralType type, TimeSpan time)
        {
            return Cron.Daily(time.Hours, time.Minutes);
        }
    }
}
