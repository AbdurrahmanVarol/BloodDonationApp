using BloodDonationApp.Business.Dtos.Responses;

namespace BloodDonationApp.MVC.Models;

public class RequestsViewModel
{
    public HospitalDisplayResponse? HospitalDetail { get; set; }

    public IEnumerable<RequestDisplayResponse> Requests { get; set; } = Enumerable.Empty<RequestDisplayResponse>();
}
