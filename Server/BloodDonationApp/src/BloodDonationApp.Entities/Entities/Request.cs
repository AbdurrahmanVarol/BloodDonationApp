namespace BloodDonationApp.Entities.Entities;

public class Request : IEntity
{
    public Guid Id { get; set; } = default;
    public required int Quantity { get; set; }

    public required int BloodGroupId { get; set; }
    public BloodGroup? BloodGroup { get; set; }

    public required Guid HospitalId { get; set; }
    public Hospital? Hospital { get; set; }

}
