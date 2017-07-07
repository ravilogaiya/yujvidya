using System;

namespace yujvidya.Interfaces
{
    public interface IPersonDetails
    {
        int Id { get; set; }

        int PersonId { get; set; }

        DateTime Date { get; set; }

        string Comments { get; set; }
    }
}