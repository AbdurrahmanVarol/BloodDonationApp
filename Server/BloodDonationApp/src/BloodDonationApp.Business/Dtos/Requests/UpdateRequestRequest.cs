namespace BloodDonationApp.Business.Dtos.Requests;
public class UpdateRequestRequest
{
    public Guid Id { get; set; } = default;
    public required int Quantity { get; set; }
    public required int BloodGroupId { get; set; }
    public required Guid HospitalId { get; set; }
}
