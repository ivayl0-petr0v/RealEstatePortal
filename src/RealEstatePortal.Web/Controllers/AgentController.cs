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

            return RedirectToAction(nameof(Details), new { id = userId });
        }
        catch (AgentCreateFailureException e)
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

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        string currentUserId = GetCurrentUserId()!;

        string? agentOwnedId = await agentService
            .GetAgentUserIdAsync(id);

        if (currentUserId != agentOwnedId)
        {
            TempData["ErrorMessage"] = EditAgentMissingPermission;
            return RedirectToAction("Index", "Home");
        }

        AgentFormModel? model = await agentService.GetAgentForEditAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, AgentFormModel model)
    {
        string currentUserId = GetCurrentUserId()!;

        string? agentOwnedId = await agentService
            .GetAgentUserIdAsync(id);

        if (currentUserId != agentOwnedId)
        {
            TempData["ErrorMessage"] = EditAgentMissingPermission;
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await agentService.EditAgentAsync(id, model);
            TempData["SuccessMessage"] = AgentEditedSuccessfullyMessage;
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (AgentEditFailureException e)
        {
            logger.LogError(e, EditAgentFailureMessage, id);
            ModelState.AddModelError(string.Empty, EditAgentFailureMessage);
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
        string currentUserId = GetCurrentUserId()!;

        string? agentOwnedId = await agentService
            .GetAgentUserIdAsync(id);

        if (currentUserId != agentOwnedId)
        {
            TempData["ErrorMessage"] = DeleteAgentMissingPermission;
            return RedirectToAction("Index", "Home");
        }

        var model = await agentService
            .GetAgentForDeleteByIdAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirm(string id)
    {
        string currentUserId = GetCurrentUserId()!;

        string? agentOwnedId = await agentService
            .GetAgentUserIdAsync(id);

        if (currentUserId != agentOwnedId)
        {
            TempData["ErrorMessage"] = DeleteAgentMissingPermission;
            return RedirectToAction("Index", "Home");
        }

        try
        {
            await agentService.DeleteAgentAsync(id);
            TempData["SuccessMessage"] = AgentDeletedSuccessfullyMessage;

            return RedirectToAction("Index", "Home");
        }
        catch (AgentDeleteFailureException e)
        {
            logger.LogError(e, DeleteAgentFailureMessage, id);
            ModelState.AddModelError(string.Empty, DeleteAgentFailureMessage);
            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, UnexpectedErrorMessage);
            ModelState.AddModelError(string.Empty, UnexpectedErrorMessage);
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
