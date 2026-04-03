namespace RealEstatePortal.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.GCommon.Exceptions;
using RealEstatePortal.Web.ViewModels.Agent;
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

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AllRealEstatesViewModel> allRealEstates = await realEstateService
            .GetAllRealEstatesAsync();

        return View(allRealEstates);
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
            formModel.Categories = await realEstateService.GetAllCategoriesAsync();
            formModel.Cities = await realEstateService.GetAllCitiesAsync();
            formModel.Features = await realEstateService.GetAllFeaturesAsync();

            return View(formModel);
        }

        try
        {
            string imageFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", "real-estates");
            string newRealEstateId = await realEstateService.CreateRealEstateAsync(formModel, agentId, imageFolderPath);
            TempData["SuccessError"] = RealEstateCreatedSuccessfullyMessage;
            return RedirectToAction("Details", new { id = newRealEstateId });
        }
        catch (RealEstateCreateFailureException e)
        {
            logger.LogError(e, CreateRealEstateFailureMessage);
            ModelState.AddModelError(string.Empty, CreateRealEstateFailureMessage);

            formModel.Categories = await realEstateService.GetAllCategoriesAsync();
            formModel.Cities = await realEstateService.GetAllCitiesAsync();
            formModel.Features = await realEstateService.GetAllFeaturesAsync();

            return View(formModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

            formModel.Categories = await realEstateService.GetAllCategoriesAsync();
            formModel.Cities = await realEstateService.GetAllCitiesAsync();
            formModel.Features = await realEstateService.GetAllFeaturesAsync();

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
}
