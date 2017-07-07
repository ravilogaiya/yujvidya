using System;
using System.ComponentModel.DataAnnotations;
using yujvidya.Interfaces;

namespace yujvidya
{
    public class EnrollmentType : IEnrollmentType
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime FromDate { get; set; }

        public bool Deleted { get; set; }

        public int Duration { get; set; }

        public DurationType DurationType { get; set; }
    }
}