using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using yujvidya.Interfaces;

namespace yujvidya
{
    public class Enrollment : IEnrollment
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int EnrollmentTypeId { get; set; }

        [ForeignKey("EnrollmentTypeId")]
        public EnrollmentType Type { get; set; }

        public double Amount { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime PaymentDate { get; set; }

        public int PreferredBatchScheduleId { get; set; }

        [ForeignKey("PreferredBatchScheduleId")]
        public BatchSchedule PreferredBatch { get; set; }

        public bool AcknowledgementSent { get; set; }
    }
}