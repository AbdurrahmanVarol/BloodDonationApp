using BloodDonationApp.Business.Dtos.Requests;
using BloodDonationApp.Business.Dtos.Responses;
using BloodDonationApp.Business.Services;
using BloodDonationApp.Entities.Enums;
using BloodDonationApp.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BloodDonationApp.MVC.Controllers;
[Authorize(Roles = "Admin,Staff")]
public class RequestController : Controller
{
    private readonly IRequestService _requestService;
    private Guid UserId => Guid.Parse(HttpContext.User.Claims.First(p => p.Type.Equals(ClaimTypes.NameIdentifier)).Value);
    private Roles UserRole => Enum.Parse<Roles>(HttpContext.User.Claims.First(p => p.Type.Equals(ClaimTypes.Role)).Value);


    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        //TODO: hastane bilgileri eklenecek
        IEnumerable<RequestDisplayResponse> requests = await _requestService.GetRequestsByUserId(UserId);
        var model = new RequestsViewModel
        {
            Requests = requests,
            HospitalDetail = null

        };
        return View(model);
    }

    [HttpGet]
    public IActionResult CreateRequest()
    {
        ViewBag.UserRole = UserRole;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateRequest(CreateRequestRequest createRequestRequest)
    {
        ViewBag.UserRole = UserRole;
        try
        {

            createRequestRequest.UserId = UserId;
            var id = await _requestService.AddAsync(createRequestRequest);
            TempData["Message"] = "Talep oluşturuldu";
            return View();
        }
        catch (Exception)
        {
            TempData["Message"] = "Talep oluştutulamadı";
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRequest([FromBody] Guid id)
    {
        await _requestService.DeleteAsync(id);
        TempData["Message"] = "Talep silindi";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> UpdateRequest(Guid id)
    {
        var request = await _requestService.GetRequestForUpdateByIdAsync(id);
        return View(request);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateRequest(UpdateRequestRequest updateRequestRequest)
    {
        try
        {
            await _requestService.UpdateAsync(updateRequestRequest);
            TempData["Message"] = "Talep güncellendi";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            TempData["Message"] = "Talep oluştutulamadı";
            return RedirectToAction(nameof(Index));
        }
    }
}
