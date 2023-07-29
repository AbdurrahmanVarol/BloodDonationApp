namespace BloodDonationApp.Business.Dtos.Responses;
public class EmployeeManagementResponse
{
    public IEnumerable<EmployeeResponse> Employees { get; set; } = Enumerable.Empty<EmployeeResponse>();
    public IEnumerable<EmployeeResponse> UnEmployedUsers { get; set; } = Enumerable.Empty<EmployeeResponse>();
}
