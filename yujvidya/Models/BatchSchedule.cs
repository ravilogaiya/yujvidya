using System;
using System.Collections;
using System.Collections.Generic;

namespace yujvidya
{
    public class BatchSchedule : Interfaces.IBatchSchedule
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public WeekDay Days { get; set; }

        public BatchType Type { get; set; }

        public bool Deleted { get; set; }
    }
}