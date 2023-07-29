using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;
public interface ICityService
{
    Task<IEnumerable<CityResponse>> GetCitiesAsync();
}
