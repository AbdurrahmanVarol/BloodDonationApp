namespace BloodDonationApp.Business.Dtos.Responses;
public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string UserName { get; set; }
}
