using System;

namespace yujvidya.Interfaces
{
    public interface IBatchSchedule : INonDeletable
    {
        int Id { get; set; }

        DateTime StartTime { get; set; }

        DateTime EndTime { get; set; }

        WeekDay Days { get; set; }

        BatchType Type { get; set; }
    }
}