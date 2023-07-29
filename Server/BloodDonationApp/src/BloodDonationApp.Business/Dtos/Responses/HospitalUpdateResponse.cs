using System.ComponentModel.DataAnnotations;

namespace BloodDonationApp.Business.Dtos.Responses;
public class HospitalUpdateResponse
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Hastane adı boş geçilemez!")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Şehir boş geçilemez!")]

    public required int CityId { get; set; }
    [Required(ErrorMessage = "Telefon numarası boş geçilemez!")]
    [RegularExpression(pattern: @"\+90\(\d{3}\)\d{3}-\d{2}-\d{2}", ErrorMessage = "Lütfen doğru formatta telefon numarası giriniz")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Adress boş geçilemez!")]
    public string Address { get; set; } = string.Empty;
}
