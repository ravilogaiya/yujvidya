namespace yujvidya.Interfaces
{
    public interface IPersonCareTaker
    {
        int Id { get; set; }

        int PersonId { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        CareTakerType Type { get; set; }

        string MobileNumber { get; set; }
    }
}