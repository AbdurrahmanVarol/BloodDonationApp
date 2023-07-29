using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Caching;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class BloodGroupViewComponent : ViewComponent
{
    private readonly ICache _cache;
    private readonly IBloodGroupService _bloodGroupService;


    public BloodGroupViewComponent(ICache cache, IBloodGroupService bloodGroupService)
    {
        _cache = cache;
        _bloodGroupService = bloodGroupService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedId)
    {
        var cachedBloodGroups = _cache.Get<IEnumerable<BloodGroupDisplayResponse>>("bloodgroups");
        if (cachedBloodGroups is null)
        {
            var bloodGroups = await _bloodGroupService.GetBloodGroupsAsync();
            _cache.Set("bloodgroups", bloodGroups, TimeSpan.FromDays(1));
            cachedBloodGroups = bloodGroups;
        }
        var model = new BloodGroupViewModel
        {
            SelectedId = selectedId,
            BloodGroups = cachedBloodGroups
        };
        return View(model);
    }
}
