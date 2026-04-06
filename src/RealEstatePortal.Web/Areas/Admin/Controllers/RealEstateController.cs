using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.Services.Core.Contracts;
using static RealEstatePortal.GCommon.OutputMessages.Admin;

namespace RealEstatePortal.Web.Areas.Admin.Controllers;

public class RealEstateController : BaseAdminController
{
    private readonly IRealEstateService realEstateService;

    public RealEstateController(IRealEstateService realEstateService)
    {
        this.realEstateService = realEstateService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await realEstateService
            .GetAllForAdminAsync();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        if (!await realEstateService.ExistsByIdAsync(id))
        {
            return NotFound();
        }

        await realEstateService
            .DeleteRealEstateAsync(id);

        TempData["SuccessMessage"] = RealEstateDeletedSuccessfullyByAdmin;
        return RedirectToAction(nameof(Index));
    }
}
