using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class CitiesViewComponent : ViewComponent
{
    private readonly ICityService _cityService;

    public CitiesViewComponent(ICityService cityService)
    {
        _cityService = cityService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedId)
    {
        var cities = await _cityService.GetCitiesAsync();

        var model = new CitiesViewModel
        {
            SelectedId = selectedId,
            Cities = cities
        };
        return View(model);
    }
}
