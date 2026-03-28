namespace RealEstatePortal.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.GCommon.Exceptions;
using RealEstatePortal.Services.Core.Contracts;
using RealEstatePortal.Web.ViewModels.Agent;
using static GCommon.ApplicationConstants;
using static GCommon.OutputMessages.Agent;

public class AgentController : BaseController
{
    private readonly IAgentService agentService;
    private readonly ILogger<AgentController> logger;

    public AgentController(IAgentService agentService, ILogger<AgentController> logger)
    {
        this.agentService = agentService;
        this.logger = logger;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        IEnumerable<AllAgentsViewModel> allAgents = await agentService
            .GetAllAgentsAsync();

        return View(allAgents);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AgentFormModel formModel)
    {
        string userId = GetCurrentUserId()!;

        if (await agentService.ExistsByIdAsync(userId))
        {
            TempData["ErrorMessage"] = AgentAlreadyExistsMessage;
            return RedirectToAction("Index", "Agent");
        }

        if (await agentService.UserWithPhoneNumberExistsAsync(formModel.PhoneNumber))
        {
            ModelState.AddModelError(nameof(formModel.PhoneNumber), AgentPhoneNumberExistsMessage);
        }

        if (!ModelState.IsValid)
        {
            return View(formModel);
        }

        try
        {
            await agentService.CreateAgentAsync(userId, formModel);
            TempData["SuccessMessage"] = AgentCreatedSuccessfullyMessage;
        }
        catch (DbEntityCreateFailureException e)
        {
            logger.LogError(e, CreateAgentFailureMessage);
            ModelState.AddModelError(string.Empty, CreateAgentFailureMessage);

            return View(formModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);

            return View(formModel);
        }

        return RedirectToAction(nameof(Details), new { id = userId });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Details(string id)
    {
        AgentDetailsViewModel? agentDetailsModel = await agentService
            .GetAgentDetailsByIdAsync(id);

        if (agentDetailsModel == null)
        {
            return NotFound();
        }

        return View(agentDetailsModel);
    }
}
