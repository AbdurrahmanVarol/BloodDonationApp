using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Services;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.MVC.Controllers;

[Authorize]
public class HospitalController : Controller
{
    private readonly IHospitalService _hospitalService;
    private readonly IUserService _userService;

    public HospitalController(IHospitalService hospitalService, IUserService userService)
    {
        _hospitalService = hospitalService;
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var hospitals = await _hospitalService.GetHospitalsAsync();
        return View(hospitals);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult CreateHospital()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateHospital(CreateHospitalRequest createHospitalRequest)
    {
        //TODO:TRYCATCH
        try
        {
            await _hospitalService.AddAsync(createHospitalRequest);
            TempData["Message"] = "Hastane oluşturuldu";
            return View();
        }
        catch
        {
            TempData["Message"] = "Hastane oluşturulamadı";
            return View();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> UpdateHospital(Guid id)
    {
        var hospital = await _hospitalService.GetHospitalForUpdateByIdAsync(id);
        if (hospital is null)
        {
            return RedirectToAction("NotFoundPage", "home");
        }
        return View(hospital);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateHospital(UpdateHospitalRequest updateHospitalRequest)
    {
        await _hospitalService.UpdateAsync(updateHospitalRequest);
        TempData["Message"] = "Hastane Güncellendi";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> DeleteHospital(Guid id)
    {
        await _hospitalService.DeleteAsync(id);
        TempData["Message"] = "Hastane Silindi";
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequest addEmployeeRequest)
    {
        try
        {
            await _hospitalService.AddEmployeeAsync(addEmployeeRequest);
            return Json(new { IsSuccess = true });
        }
        catch
        {
            return Json(new { IsSuccess = false });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> RemoveEmployee([FromBody] RemoveEmployeeRequest removeEmployeeRequest)
    {
        try
        {
            await _hospitalService.RemoveEmployeeAsync(removeEmployeeRequest);
            return Json(new { IsSuccess = true });
        }
        catch
        {
            return Json(new { IsSuccess = false });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IActionResult EmployeeManagement(Guid hospitalId)
    {
        var model = new EmployeeManagementViewModel
        {
            HospitalId = hospitalId
        };
        return View(model);
    }
}
