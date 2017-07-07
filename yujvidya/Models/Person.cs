using System;
using System.ComponentModel.DataAnnotations.Schema;
using yujvidya.Interfaces;

namespace yujvidya
{
    public class Person : IPerson
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string MobileNumber { get; set; }

        public bool Inactive { get; set; }
    }

    public class PersonCareTaker : IPersonCareTaker
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public CareTakerType Type { get; set; }

        public string MobileNumber { get; set; }
    }

    public class PersonDetails : IPersonDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PersonId { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }
    }

    public class Student : IStudent
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime EnrolledUpto { get; set; }

        public int EnrollmentId { get; set; }

        public string MobileNumber { get; set; }
    }
}