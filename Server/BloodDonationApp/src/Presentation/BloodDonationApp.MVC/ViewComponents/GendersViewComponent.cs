using BloodDonationApp.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class GendersViewComponent : ViewComponent
{
    private readonly IGenderService _genderService;

    public GendersViewComponent(IGenderService genderService)
    {
        _genderService = genderService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var genders = await _genderService.GetGendersAsync();

        return View(genders);
    }
}
