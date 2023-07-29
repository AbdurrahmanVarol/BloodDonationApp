using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;
public interface IBloodGroupService
{
    Task<IEnumerable<BloodGroupDisplayResponse>> GetBloodGroupsAsync();
}
