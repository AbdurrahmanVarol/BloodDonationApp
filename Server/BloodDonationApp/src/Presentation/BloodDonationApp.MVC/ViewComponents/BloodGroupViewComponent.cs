using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.ViewComponents;

public class BloodGroupViewComponent : ViewComponent
{
    private readonly IBloodGroupService _bloodGroupService;


    public BloodGroupViewComponent(IBloodGroupService bloodGroupService)
    {
        _bloodGroupService = bloodGroupService;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? selectedId)
    {
        var bloodGroups = await _bloodGroupService.GetBloodGroupsAsync();

        var model = new BloodGroupViewModel
        {
            SelectedId = selectedId,
            BloodGroups = bloodGroups
        };
        return View(model);
    }
}
