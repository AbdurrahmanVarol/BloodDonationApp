using System.ComponentModel.DataAnnotations;

namespace BloodDonationApp.Business.Dtos.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Şifre adı boş geçilemez!")]
    public string Password { get; set; } = string.Empty;
    public bool IsKeepLoggedIn { get; set; }
}