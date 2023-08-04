using System.ComponentModel.DataAnnotations;

namespace BloodDonationApp.Business.Dtos.Requests;

public class RegisterRequest
{
    //TODO:Düzenle
    [Required(ErrorMessage = "Ad boş geçilemez!")]
    public required string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Soyad boş geçilemez!")]
    public required string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email boş geçilemez!")]
    public required string Email { get; set; }

    public required int CityId { get; set; }
    public required int GenderId { get; set; }
    public required int BloodGroupId { get; set; }
    public Guid? HospitalId { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı boş geçilemez!")]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "Şifre boş geçilemez!")]
    public required string Password { get; set; }
    [Compare(otherProperty: "Password", ErrorMessage = "Şifre ve şifre tekrar aynı olmalı!")]
    [Required(ErrorMessage = "Şifre tekrar boş geçilemez!")]
    public required string PasswordConfirm { get; set; }
}
