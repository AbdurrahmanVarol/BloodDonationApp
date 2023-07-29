using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Caching;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class GendersViewComponent : ViewComponent
{
    private readonly IGenderService _genderService;
    private readonly ICache _cache;

    public GendersViewComponent(IGenderService genderService, ICache cache)
    {
        _genderService = genderService;
        _cache = cache;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cachedGenders = _cache.Get<IEnumerable<GenderResponse>>("genders");
        if (cachedGenders == null)
        {
            var genders = await _genderService.GetGendersAsync();
            _cache.Set("genders", genders, TimeSpan.FromDays(1));
            cachedGenders = genders;
        }
        return View(cachedGenders);
    }
}
