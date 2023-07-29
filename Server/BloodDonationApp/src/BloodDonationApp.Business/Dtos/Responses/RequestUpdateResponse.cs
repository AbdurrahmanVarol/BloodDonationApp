using System.ComponentModel.DataAnnotations;

namespace BloodDonationApp.Business.Dtos.Responses;
public class RequestUpdateResponse
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Miktar boş geçilemez!")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Miktar 1'den büyük olmadı")]
    public required int Quantity { get; set; }
    [Required(ErrorMessage = "Kan grubu boş geçilemez!")]
    public required int BloodGroupId { get; set; }
    [Required(ErrorMessage = "Hastane boş geçilemez!")]
    public required Guid HospitalId { get; set; }
}
