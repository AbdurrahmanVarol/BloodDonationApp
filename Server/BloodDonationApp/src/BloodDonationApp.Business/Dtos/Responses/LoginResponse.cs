namespace BloodDonationApp.Business.Dtos.Responses;
public class LoginResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string UserName { get; set; }
    public DateTime Expire { get; set; }
    public int BloodGroup { get; set; }
    public int City { get; set; }
    public int UserRole { get; set; }
}
