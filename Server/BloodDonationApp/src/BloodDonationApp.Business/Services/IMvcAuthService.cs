using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.Business.Services;

public interface IMvcAuthService : IAuthService
{
    Task<UserResponse> LoginAsync(LoginRequest loginRequest);
}
