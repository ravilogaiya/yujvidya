using System;

namespace yujvidya.Interfaces
{
    public interface IEnrollmentType : INonDeletable
    {
        double Amount { get; set; }
        DateTime FromDate { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        int Duration { get; set; }
        DurationType DurationType { get; set; }
    }
}