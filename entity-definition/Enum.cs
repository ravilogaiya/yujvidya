using System;

namespace yujvidya
{
    public enum Gender : int
    {
        Male = 1,
        Female = 2
    }

    public enum BatchType : int
    {
        Kid = 1,

        Adults = 2,

        Women = 3
    }

    [Flags]
    public enum WeekDay
    {
        //
        // Summary:
        //     Indicates Sunday.
        Sunday = 1,
        //
        // Summary:
        //     Indicates Monday.
        Monday = 2,
        //
        // Summary:
        //     Indicates Tuesday.
        Tuesday = 4,
        //
        // Summary:
        //     Indicates Wednesday.
        Wednesday = 8,
        //
        // Summary:
        //     Indicates Thursday.
        Thursday = 16,
        //
        // Summary:
        //     Indicates Friday.
        Friday = 32,
        //
        // Summary:
        //     Indicates Saturday.
        Saturday = 64
    }

    public enum CareTakerType
    {
        Father = 1,
        Mother = 2,
        Uncle = 3,
        Aunty = 4,
        Friend = 5,
        Gaurdian = 6
    }

    public enum DurationType
    {
        Days = 1,
        Months = 2,
        Years = 3
    }
}