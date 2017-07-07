using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yujvidya.Managers;
using yujvidya.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace yujvidya.Schedulers
{
    public class DueDateNotifyScheduler
    {
        public string Name => "due-date-checking";

        public NotifyInteralType NotifyType => NotifyInteralType.Daily;

        public TimeSpan Time => new TimeSpan(9, 0, 0);

        public void Callback()
        {
            // First notification - before 2 days
            // Second notification - On due date
            // Third notification - One day after due date
            // Fourth notification - After 7 days
            // Fifth notification - Make inactive after 10 days

            Debug.WriteLine($"Hangfire callback at {DateTime.Now}");

            var context = new PersonContext(new Microsoft.EntityFrameworkCore.DbContextOptions<PersonContext>());

            var personController = new PersonController(context);
            var activeStudents = personController.GetStudents(string.Empty, 0, string.Empty, 0, 0, false, DateTime.Now.AddDays((int)DueDateNotificationLevel.Fifth));

            var studentsToMakeInactive = new List<int>();
            var smsDetails = new List<SmsDetail>();
            var dueDateNotifications = new List<DueDateNotification>();

            foreach (var activeStudent in activeStudents)
            {
                var dueDateDifference = DateTime.Now.Subtract(activeStudent.EnrolledUpto);

                if (dueDateDifference.Days >= (int)DueDateNotificationLevel.Fifth)
                {
                    // Make student as inactive
                    studentsToMakeInactive.Add(activeStudent.Id);
                    continue;
                }

                // Getting latest due date notification
                var lastestDueDateNotification = context.DueDateNotifications.Where(x => x.PersonId == activeStudent.Id).OrderByDescending(x => x.Level).FirstOrDefault();
                var latestNotification = lastestDueDateNotification?.Level;

                var currentNotification = default(DueDateNotificationLevel?);
                var messageTemplate = default(MessageTemplate);

                if (dueDateDifference.Days > (int)DueDateNotificationLevel.Second)
                {
                    currentNotification = latestNotification == null || latestNotification < DueDateNotificationLevel.Third ?
                                                            DueDateNotificationLevel.Third :
                                                            DueDateNotificationLevel.Fourth;

                    // Send over due notification
                    messageTemplate = dueDateDifference.Days == 1 ?
                                                        MessageTemplate.OneDayOverDueDateTemplate :
                                                        MessageTemplate.OverDueDateTemplate;

                }
                else if (dueDateDifference.Days == (int)DueDateNotificationLevel.Second &&
                         (latestNotification == null || latestNotification < DueDateNotificationLevel.Second))
                {
                    currentNotification = DueDateNotificationLevel.Second;

                    // Send expiry notification
                    messageTemplate = MessageTemplate.OnDueDateTemplate;
                }
                else if (dueDateDifference.Days >= (int)DueDateNotificationLevel.First &&
                         (latestNotification == null || latestNotification < DueDateNotificationLevel.First))
                {
                    currentNotification = DueDateNotificationLevel.First;

                    // Send prior expiry notification
                    messageTemplate = dueDateDifference.Days == -1 ?
                                                        MessageTemplate.OneDayPriorDueDateTemplate :
                                                        MessageTemplate.PriorDueDateTemplate;


                }

                if (currentNotification.HasValue)
                {
                    var smsDetail = NotificationMessageManager.SendSms(activeStudent.MobileNumber, messageTemplate,
                                                                       activeStudent.FirstName,
                                                                       activeStudent.EnrolledUpto.ToString("yy-MMM-yyyy")).Result;
                    smsDetail.PersonId = activeStudent.Id;

                    var dueDateNotification = new DueDateNotification()
                    {
                        PersonId = activeStudent.Id,
                        EnrollmentId = activeStudent.EnrollmentId,
                        Date = DateTime.Now,
                        Level = currentNotification.Value,
                        SmsDetailId = smsDetail.Id
                    };

                    smsDetails.Add(smsDetail);
                    dueDateNotifications.Add(dueDateNotification);
                }
            }

            // Making students inactive
            context.Persons.Where(x => studentsToMakeInactive.Contains(x.Id)).ForEach(x => x.Inactive = true);

            context.AddRange(smsDetails);
            context.AddRange(dueDateNotifications);

            context.SaveChangesAsync().Wait();

            Debug.WriteLine("Hangefire job");
            Debug.WriteLine(JsonConvert.SerializeObject(new { smsDetails, dueDateNotifications, studentsToMakeInactive }));
        }
    }
}
