namespace BloodDonationApp.Business.Dtos.Responses;
public class HospitalDisplayResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string? City { get; set; }
}
