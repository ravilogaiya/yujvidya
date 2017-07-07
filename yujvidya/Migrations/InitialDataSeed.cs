using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace yujvidya.Migrations
{
    public static class InitialDataSeed
    {
        public static void Update(PersonContext context)
        {
            if (!context.BatchSchedules.Any())
            {
                context.AddRange(
                        new BatchSchedule() { StartTime = Utilities.GetTtime(5, 45), EndTime = Utilities.GetTtime(6, 45), Type = BatchType.Adults, Days = Utilities.GetWeekDays() },
                        new BatchSchedule() { StartTime = Utilities.GetTtime(7, 0), EndTime = Utilities.GetTtime(8, 0), Type = BatchType.Adults, Days = Utilities.GetWeekDays() },
                        new BatchSchedule() { StartTime = Utilities.GetTtime(8, 15), EndTime = Utilities.GetTtime(9, 15), Type = BatchType.Adults, Days = Utilities.GetWeekDays() },
                        new BatchSchedule() { StartTime = Utilities.GetTtime(9, 30), EndTime = Utilities.GetTtime(10, 30), Type = BatchType.Women, Days = Utilities.GetWeekDays() },
                        new BatchSchedule() { StartTime = Utilities.GetTtime(17, 0), EndTime = Utilities.GetTtime(18, 30), Type = BatchType.Kid, Days = Utilities.GetWeekDays() },
                        new BatchSchedule() { StartTime = Utilities.GetTtime(19, 0), EndTime = Utilities.GetTtime(20, 0), Type = BatchType.Adults, Days = Utilities.GetWeekDays() }
                    );
            }

            if (!context.EnrollmentTypes.Any())
            {
                context.AddRange(
                        new EnrollmentType() { Name = "Monthly", Amount = 2000, FromDate = new DateTime(2017, 4, 1), Duration = 1, DurationType = DurationType.Months },
                        new EnrollmentType() { Name = "Quarterly", Amount = 4500, FromDate = new DateTime(2017, 4, 1), Duration = 3, DurationType = DurationType.Months },
                        new EnrollmentType() { Name = "Half-yearly", Amount = 9000, FromDate = new DateTime(2017, 4, 1), Duration = 6, DurationType = DurationType.Months },
                        new EnrollmentType() { Name = "Annually", Amount = 15000, FromDate = new DateTime(2017, 4, 1), Duration = 1, DurationType = DurationType.Years }
                    );
            }

            context.SaveChanges();
        }
    }
}
