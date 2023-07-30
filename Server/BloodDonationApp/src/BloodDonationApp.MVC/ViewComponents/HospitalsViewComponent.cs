using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class HospitalsViewComponent : ViewComponent
{
    private readonly IHospitalService _hospitalService;

    public HospitalsViewComponent(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var hospitals = await _hospitalService.GetHospitalsAsync();

        return View(hospitals);
    }
}
