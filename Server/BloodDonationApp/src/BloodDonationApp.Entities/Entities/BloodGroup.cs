namespace BloodDonationApp.Entities.Entities;

public class BloodGroup : IEntity
{
    public int Id { get; set; } = default;
    public required string Name { get; set; }
    public string? Symbol { get; set; }


    public ICollection<Request> Requests { get; set; } = new List<Request>();

    public ICollection<User> Users { get; set; } = new List<User>();
}
