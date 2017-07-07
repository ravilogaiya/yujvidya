using System;
using System.ComponentModel.DataAnnotations;

namespace yujvidya.Models
{
    public class DueDateNotification
    {
        [Key]
        public int Id
        {
            get;
            set;
        }

        public int PersonId
        {
            get;
            set;
        }

        public int EnrollmentId
        {
            get;
            set;
        }

        public DueDateNotificationLevel Level
        {
            get;
            set;
        }

        public int SmsDetailId
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }
    }
}
