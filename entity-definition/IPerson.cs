using System;

namespace yujvidya.Interfaces
{
    public interface IPerson
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        Gender Gender { get; set; }

        DateTime BirthDate { get; set; }

        string MobileNumber { get; set; }
    }
}