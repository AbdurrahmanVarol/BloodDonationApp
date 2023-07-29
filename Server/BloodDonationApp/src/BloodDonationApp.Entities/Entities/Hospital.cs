namespace BloodDonationApp.Entities.Entities;

public class Hospital : IEntity
{
    public Guid Id { get; set; } = default;
    public required string Name { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public required int CityId { get; set; }
    public City? City { get; set; }

    public ICollection<Request> Requests { get; set; } = new List<Request>();

    public ICollection<User> Employees { get; set; } = new List<User>();
}
