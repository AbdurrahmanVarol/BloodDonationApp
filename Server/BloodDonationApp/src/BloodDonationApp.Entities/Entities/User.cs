namespace BloodDonationApp.Entities.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }

    public required int GenderId { get; set; }
    public Gender? Gender { get; set; }

    public required int CityId { get; set; }
    public City? City { get; set; }

    public required int BloodGroupId { get; set; }
    public BloodGroup? BloodGroup { get; set; }

    public Guid? HospitalId { get; set; }
    public Hospital? Hospital { get; set; }

    public required int RoleId { get; set; }
    public Role? Role { get; set; }
}
