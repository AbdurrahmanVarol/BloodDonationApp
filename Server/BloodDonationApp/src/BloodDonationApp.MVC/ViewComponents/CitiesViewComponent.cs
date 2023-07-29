using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Caching;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class CitiesViewComponent : ViewComponent
{
    private readonly ICityService _cityService;
    private readonly ICache _cache;

    public CitiesViewComponent(ICityService cityService, ICache cache)
    {
        _cityService = cityService;
        _cache = cache;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedId)
    {
        var cachedCities = _cache.Get<IEnumerable<CityResponse>>("cities");
        if (cachedCities is null)
        {
            var cities = await _cityService.GetCitiesAsync();
            _cache.Set("cities", cities, TimeSpan.FromDays(1));
            cachedCities = cities;
        }
        var model = new CitiesViewModel
        {
            SelectedId = selectedId,
            Cities = cachedCities
        };
        return View(model);
    }
}
