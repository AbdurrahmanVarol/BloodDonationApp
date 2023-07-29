using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Entities.Entities;

namespace BloodDonationApp.Business.Services;
public interface IApiAuthService : IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    string GenerateToken(User user);
}
