using Microsoft.AspNetCore.Mvc;

namespace Presentation.Features.Home.Landing;

public class LandingController : Controller
{
    // GET
    public IActionResult Index()
    {
        //this is new branch
        
        ViewData["data"] = "Hello";
        return View();
    }
}