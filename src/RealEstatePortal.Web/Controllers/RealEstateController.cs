namespace RealEstatePortal.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class RealEstateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
