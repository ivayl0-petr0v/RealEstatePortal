namespace RealEstatePortal.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePortal.Services.Core.Contracts;
using RealEstatePortal.Web.ViewModels.Home;
using System.Diagnostics;
using ViewModels;

[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly IRealEstateService realEstateService;
    private readonly IAgentService agentService;

    public HomeController(IRealEstateService realEstateService, IAgentService agentService)
    {
        this.realEstateService = realEstateService;
        this.agentService = agentService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            RealEstates = await realEstateService.GetTopThreeRealEstatesAsync(),
            Agents = await agentService.GetTopFourAgentsAsync()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("Home/Error/{statusCode}")]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404)
        {
            return View("Error404");
        }

        return View("Error500");
    }
}
