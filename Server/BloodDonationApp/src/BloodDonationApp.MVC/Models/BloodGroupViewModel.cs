using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.MVC.Models;

public class BloodGroupViewModel
{
    public int? SelectedId { get; set; }
    public IEnumerable<BloodGroupDisplayResponse> BloodGroups { get; set; } = new List<BloodGroupDisplayResponse>();
}
