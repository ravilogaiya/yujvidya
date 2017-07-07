using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using yujvidya.Interfaces;

namespace yujvidya.Models
{
    public class Student : IStudent
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        [Display(Name = "Upto")]
        public DateTime EnrolledUpto { get; set; }
    }
}
