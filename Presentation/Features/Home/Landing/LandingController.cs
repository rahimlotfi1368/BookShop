using Microsoft.AspNetCore.Mvc;

namespace Presentation.Features.Home.Landing;

public class LandingController : Controller
{
    // GET
    public IActionResult Index()
    {
        ViewData["data"] = "Hello";
        return View();
    }
}