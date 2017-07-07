using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore;
using yujvidya.Schedulers;

namespace yujvidya.Managers
{
    public class NotificationScheduleManager
    {
        public static void StartSchecules()
        {
            var dueDateSchedule = new DueDateNotifyScheduler();

            RecurringJob.RemoveIfExists(dueDateSchedule.Name);

            RecurringJob.AddOrUpdate(dueDateSchedule.Name, () => dueDateSchedule.Callback(), dueDateSchedule.NotifyType.ToCronExpression(dueDateSchedule.Time), TimeZoneInfo.Local);
        }
    }
}
