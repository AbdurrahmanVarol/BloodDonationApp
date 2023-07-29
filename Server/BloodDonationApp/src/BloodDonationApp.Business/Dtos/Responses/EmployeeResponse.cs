namespace BloodDonationApp.Business.Dtos.Responses;
public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string City { get; set; } = string.Empty;
    public Guid? HospitalId { get; set; }
}
