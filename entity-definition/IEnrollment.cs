using System;

namespace yujvidya.Interfaces
{
    public interface IEnrollment
    {
        int Id { get; set; }

        int PersonId { get; set; }

        int EnrollmentTypeId { get; set; }

        double Amount { get; set; }

        DateTime FromDate { get; set; }

        DateTime ToDate { get; set; }

        DateTime PaymentDate { get; set; }

        int PreferredBatchScheduleId { get; set; }

        bool AcknowledgementSent { get; set; }
    }
}