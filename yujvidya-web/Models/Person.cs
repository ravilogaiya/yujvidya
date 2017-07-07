using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using yujvidya.Interfaces;

namespace yujvidya.Models
{
    public class Person : IPerson
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
    }

    public class PersonCareTaker : IPersonCareTaker
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public CareTakerType Type { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Wrong mobile")]
        public string MobileNumber { get; set; }
    }

    public class PersonDetails : IPersonDetails
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        public DateTime Date { get; set; }

        public string Comments { get; set; }
    }
}