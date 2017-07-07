using System;
using System.ComponentModel.DataAnnotations;
using yujvidya.Interfaces;

namespace yujvidya.Models
{
    public class BatchSchedule : IBatchSchedule
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime EndTime { get; set; }

        public WeekDay Days { get; set; }

        [Required]
        public BatchType Type { get; set; }

        public bool Deleted { get; set; }

        public bool Sunday
        {
            get
            {
                return Days.HasFlag(WeekDay.Sunday);
            }
            set
            {
                SetDays(value, WeekDay.Sunday);
            }
        }

        public bool Monday
        {
            get
            {
                return Days.HasFlag(WeekDay.Monday);
            }
            set
            {
                SetDays(value, WeekDay.Monday);
            }
        }

        public bool Tuesday
        {
            get
            {
                return Days.HasFlag(WeekDay.Tuesday);
            }
            set
            {
                SetDays(value, WeekDay.Tuesday);
            }
        }

        public bool Wednesday
        {
            get
            {
                return Days.HasFlag(WeekDay.Wednesday);
            }
            set
            {
                SetDays(value, WeekDay.Wednesday);
            }
        }

        public bool Thursday
        {
            get
            {
                return Days.HasFlag(WeekDay.Thursday);
            }
            set
            {
                SetDays(value, WeekDay.Thursday);
            }
        }

        public bool Friday
        {
            get
            {
                return Days.HasFlag(WeekDay.Friday);
            }
            set
            {
                SetDays(value, WeekDay.Friday);
            }
        }

        public bool Saturday
        {
            get
            {
                return Days.HasFlag(WeekDay.Saturday);
            }
            set
            {
                SetDays(value, WeekDay.Saturday);
            }
        }

        private void SetDays(bool value, WeekDay weekDay)
        {
            if (value)
                Days |= weekDay;
            else
                Days &= ~weekDay;
        }
    }
}