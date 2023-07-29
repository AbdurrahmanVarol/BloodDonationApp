using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Caching;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class HospitalsViewComponent : ViewComponent
{
    private readonly IHospitalService _hospitalService;
    private readonly ICache _cache;

    public HospitalsViewComponent(IHospitalService hospitalService, ICache cache)
    {
        _hospitalService = hospitalService;
        _cache = cache;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cachedHospitals = _cache.Get<IEnumerable<HospitalDisplayResponse>>("hospitals");
        if (cachedHospitals is null)
        {
            var hospitals = await _hospitalService.GetHospitalsAsync();
            _cache.Set("hospitals", hospitals, TimeSpan.FromDays(1));
            cachedHospitals = hospitals;
        }
        return View(cachedHospitals);
    }
}
