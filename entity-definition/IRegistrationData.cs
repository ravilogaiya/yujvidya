using System.Collections.Generic;

namespace yujvidya.Interfaces
{
    public interface IRegistrationData
    {
        IPerson Person { get; set; }

        IPersonDetails Details { get; set; }

        IPersonCareTaker CareTaker1 { get; set; }

        IPersonCareTaker CareTaker2 { get; set; }

        IEnrollment Enrollment { get; set; }
    }

    public interface IStudentDetail
    {
        IPerson Person { get; set; }

        IPersonDetails Details { get; set; }

        IEnumerable<IPersonCareTaker> CareTakers { get; set; }

        IEnumerable<IEnrollment> Enrollments { get; set; }
    }
}