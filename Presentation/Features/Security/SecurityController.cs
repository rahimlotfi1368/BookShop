using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Features.Security.Command;
using Presentation.Features.Security.Query;

namespace Presentation.Features.Security;

public class SecurityController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Login(LoginUserQuery query)
    {
        var response=await _mediator.Send(query);

        if (response.Success == true)
            return RedirectToAction("Login");
        
        return View(response);
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var response=await _mediator.Send(command);

        if (response.Success == true)
            return RedirectToAction("Login");
        
        return View(response);
    }
}