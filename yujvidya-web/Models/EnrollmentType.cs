using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using yujvidya.Interfaces;

namespace yujvidya.Models
{
    public class EnrollmentType : IEnrollmentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Remote("IsEnrollmentTypeNameExists", "EnrollmentType", ErrorMessage = "Enrollment type name already exists.", HttpMethod = "GET", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public DateTime FromDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public DurationType DurationType { get; set; }

        public bool Deleted { get; set; }
    }
}