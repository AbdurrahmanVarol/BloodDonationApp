namespace BloodDonationApp.Business.Dtos.Responses;
public class RequestDisplayResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public int BloodGroupId { get; set; }
    public string BloodGroup { get; set; } = string.Empty;
    public int CityId { get; set; }
    public string City { get; set; } = string.Empty;
    public Guid HospitalId { get; set; }
    public string Hospital { get; set; } = string.Empty;
}
