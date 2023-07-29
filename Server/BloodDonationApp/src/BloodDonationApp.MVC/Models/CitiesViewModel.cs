using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.MVC.Models;

public class CitiesViewModel
{
    public int? SelectedId { get; set; }
    public IEnumerable<CityResponse> Cities { get; set; } = new List<CityResponse>();
}
