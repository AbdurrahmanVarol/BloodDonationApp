namespace BloodDonationApp.Business.Dtos.Requests;
public class AddEmployeeRequest
{
    public Guid HospitalId { get; set; }
    public Guid EmployeeId { get; set; }
}
