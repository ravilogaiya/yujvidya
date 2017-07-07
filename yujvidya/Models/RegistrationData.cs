using System;
using System.Collections.Generic;
using yujvidya.Interfaces;
using System.Linq;

namespace yujvidya
{
    public class RegistrationData : IRegistrationData
    {
        public Person Person { get; set; }

        public PersonDetails Details { get; set; }

        public PersonCareTaker CareTaker1 { get; set; }

        public PersonCareTaker CareTaker2 { get; set; }

        public Enrollment Enrollment { get; set; }

        IPerson IRegistrationData.Person { get => Person; set => Person = (Person)value; }
        IPersonDetails IRegistrationData.Details { get => Details; set => Details = (PersonDetails)value; }
        IPersonCareTaker IRegistrationData.CareTaker1 { get => CareTaker1; set => CareTaker1 = (PersonCareTaker)value; }
        IPersonCareTaker IRegistrationData.CareTaker2 { get => CareTaker2; set => CareTaker2 = (PersonCareTaker)value; }
        IEnrollment IRegistrationData.Enrollment { get => Enrollment; set => Enrollment = (Enrollment)value; }
    }

    public class StudentDetail : IStudentDetail
    {
        public Person Person { get; set; }
        public PersonDetails Details { get; set; }
        public List<PersonCareTaker> CareTakers { get; set; }
        public List<Enrollment> Enrollments { get; set; }

        IPerson IStudentDetail.Person { get => Person; set => Person = (Person)value; }
        IPersonDetails IStudentDetail.Details { get => Details; set => Details = (PersonDetails)value; }
        IEnumerable<IPersonCareTaker> IStudentDetail.CareTakers { get => CareTakers; set => CareTakers = value.OfType<PersonCareTaker>().ToList(); }
        IEnumerable<IEnrollment> IStudentDetail.Enrollments { get => Enrollments; set => Enrollments = value.OfType<Enrollment>().ToList(); }
    }
}