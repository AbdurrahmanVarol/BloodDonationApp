namespace BloodDonationApp.Entities.Entities;

public class City : IEntity
{
    public int Id { get; set; } = default;
    public required string Name { get; set; }
    public required string Plate { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
