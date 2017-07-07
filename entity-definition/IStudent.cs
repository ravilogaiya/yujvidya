using System;

namespace yujvidya.Interfaces
{
    public interface IStudent
    {
        int Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        DateTime EnrolledUpto { get; set; }
    }
}