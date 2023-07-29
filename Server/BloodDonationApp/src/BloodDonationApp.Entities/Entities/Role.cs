namespace BloodDonationApp.Entities.Entities;

public class Role : IEntity
{
    public int Id { get; set; } = default;
    public required string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}
