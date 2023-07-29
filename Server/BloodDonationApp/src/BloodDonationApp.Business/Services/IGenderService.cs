using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;
public interface IGenderService
{
    Task<IEnumerable<GenderResponse>> GetGendersAsync();
}
