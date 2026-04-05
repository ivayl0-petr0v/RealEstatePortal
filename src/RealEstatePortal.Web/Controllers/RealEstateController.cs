namespace RealEstatePortal.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.GCommon.Exceptions;
using Services.Core.Contracts;
using ViewModels.RealEstate;
using static GCommon.ApplicationConstants;
using static GCommon.OutputMessages.RealEstate;

public class RealEstateController : BaseController
{
    private readonly IRealEstateService realEstateService;
    private readonly IAgentService agentService;
    private readonly ILogger<RealEstateController> logger;
    private readonly IWebHostEnvironment webHostEnvironment;

    public RealEstateController(IRealEstateService realEstateService, IAgentService agentService, ILogger<RealEstateController> logger, IWebHostEnvironment webHostEnvironment)
    {
        this.realEstateService = realEstateService;
        this.agentService = agentService;
        this.logger = logger;
        this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index([FromQuery] AllRealEstatesQueryModel queryModel)
    {
        var model = await realEstateService
            .GetAllRealEstatesAsync(queryModel);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        string currentUserId = GetCurrentUserId()!;

        bool isAgent = await agentService.ExistsByIdAsync(currentUserId);

        if (!isAgent)
        {
            TempData["ErrorMessage"] = UserWithoutAgentProfile;
            return RedirectToAction("Create", "Agent");
        }

        var model = new RealEstateFormModel
        {
            Categories = await realEstateService.GetAllCategoriesAsync(),
            Cities = await realEstateService.GetAllCitiesAsync(),
            Features = await realEstateService.GetAllFeaturesAsync()
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RealEstateFormModel formModel)
    {
        string currentUserId = GetCurrentUserId()!;

        string? agentId = await agentService
            .GetAgentIdByUserIdAsync(currentUserId);

        if (agentId == null)
        {
            TempData["ErrorMessage"] = UserWithoutAgentProfile;
            return RedirectToAction("Create", "Agent");
        }

        if (!ModelState.IsValid)
        {
            await LoadDropdownDataAsync(formModel);
            return View(formModel);
        }

        try
        {
            string imageFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "real-estates");
            string newRealEstateId = await realEstateService.CreateRealEstateAsync(formModel, agentId, imageFolderPath);
            TempData["SuccessMessage"] = RealEstateCreatedSuccessfullyMessage;
            return RedirectToAction("Details", new { id = newRealEstateId });
        }
        catch (RealEstateCreateFailureException e)
        {
            logger.LogError(e, CreateRealEstateFailureMessage);
            ModelState.AddModelError(string.Empty, CreateRealEstateFailureMessage);

            await LoadDropdownDataAsync(formModel);
            return View(formModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

            await LoadDropdownDataAsync(formModel);
            return View(formModel);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        var model = await realEstateService
            .GetDetailsByIdAsync(id);

        if (model == null)
        {
            TempData["ErrorMessage"] = RealEstateNotFoundMessage;
            return RedirectToAction("Index", "RealEstate");
        }

        if (User.Identity?.IsAuthenticated == true)
        {
            string currentUserId = GetCurrentUserId()!;
            string? currentAgentId = await agentService
                .GetAgentIdByUserIdAsync(currentUserId);

            model.IsOwner = (currentAgentId == model.AgentId);
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        if (!await realEstateService.ExistsByIdAsync(id)) return NotFound();

        string? agendId = await agentService
            .GetAgentIdByUserIdAsync(GetCurrentUserId()!);
        if (agendId == null || !await realEstateService.IsAgentIdOwnerOfRealEstateIdAsync(id, agendId))
        {
            return Unauthorized();
        }

        var formModel = await realEstateService.GetRealEstateForEditByIdAsync(id);
        formModel!.Categories = await realEstateService.GetAllCategoriesAsync();
        formModel.Cities = await realEstateService.GetAllCitiesAsync();
        formModel.Features = await realEstateService.GetAllFeaturesAsync();

        return View(formModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, RealEstateFormModel model)
    {
        int countToBeDeleted = model.RemovedImagesUrls?.Count ?? 0;

        if (!ModelState.IsValid)
        {
            await LoadDropdownDataAsync(model);
            return View(model);
        }

        string? agentId = await agentService.GetAgentIdByUserIdAsync(GetCurrentUserId()!);
        if (agentId == null || !await realEstateService.IsAgentIdOwnerOfRealEstateIdAsync(id, agentId))
        {
            return Unauthorized();
        }

        try
        {
            string imageFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "real-estates");
            await realEstateService.EditRealEstateAsync(id, model, imageFolderPath);
            TempData["SuccessMessage"] = RealEstateUpdatedSuccessfullyMessage;

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (RealEstateEditFailureException e)
        {
            logger.LogError(e, EditRealEstateLogError, id);
            ModelState.AddModelError(string.Empty, EditRealEstateModelError);

            await LoadDropdownDataAsync(model);
            return View(model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        if (!await realEstateService.ExistsByIdAsync(id)) return NotFound();

        string currentUserId = GetCurrentUserId()!;
        string? agentId = await agentService.GetAgentIdByUserIdAsync(currentUserId);

        if (agentId == null || !await realEstateService.IsAgentIdOwnerOfRealEstateIdAsync(id, agentId))
        {
            return Unauthorized();
        }

        var model = await realEstateService.GetRealEstateForDeleteByIdAsync(id);
        if (model == null) return NotFound();

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (!await realEstateService.ExistsByIdAsync(id)) return NotFound();

        string currentUserId = GetCurrentUserId()!;
        string? agentId = await agentService.GetAgentIdByUserIdAsync(currentUserId);

        if (agentId == null || !await realEstateService.IsAgentIdOwnerOfRealEstateIdAsync(id, agentId))
        {
            return Unauthorized();
        }

        try
        {
            await realEstateService.DeleteRealEstateAsync(id);

            TempData["SuccessMessage"] = "Property deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (RealEstateDeleteFailureException e)
        {
            logger.LogError(e, DeleteRealEstateLogError, id);
            ModelState.AddModelError(string.Empty, DeleteRealEstateModelError);
            return RedirectToAction(nameof(Delete), new { id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            return RedirectToAction(nameof(Delete), new { id });
        }
    }

    private async Task LoadDropdownDataAsync(RealEstateFormModel model)
    {
        model.Categories = await realEstateService.GetAllCategoriesAsync();
        model.Cities = await realEstateService.GetAllCitiesAsync();
        model.Features = await realEstateService.GetAllFeaturesAsync();
    }
}
