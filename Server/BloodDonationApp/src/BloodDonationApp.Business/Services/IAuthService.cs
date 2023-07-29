using BloodDonationApp.Business.Dtos.Requests;

namespace BloodDonationApp.Business.Services;
public interface IAuthService
{
    Task RegisterAsync(RegisterRequest registerRequest);
    void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
    bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt);
}
