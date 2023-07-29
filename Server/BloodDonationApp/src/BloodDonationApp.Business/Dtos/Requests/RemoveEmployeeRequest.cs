namespace BloodDonationApp.Business.Dtos.Requests;
public class RemoveEmployeeRequest
{
    public Guid HospitalId { get; set; }
    public Guid EmployeeId { get; set; }
}
