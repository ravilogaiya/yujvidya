using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using yujvidya.Interfaces;

namespace yujvidya.Models
{
    public class Enrollment : IEnrollment
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [Required]
        [Display(Name = "Fee Detail")]
        public int EnrollmentTypeId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        [Display(Name = "From")]
        public DateTime FromDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        [Display(Name = "To")]
        public DateTime ToDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Preferred Batch")]
        public int PreferredBatchScheduleId { get; set; }

        public bool AcknowledgementSent { get; set; }

        public EnrollmentType Type { get; set; }

        public BatchSchedule PreferredBatch { get; set; }

        public bool AllowEdit { get; set; }
    }
}