using Microsoft.AspNetCore.Mvc;

namespace AmazingJourney.Api.Controllers
{
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

    }
}
