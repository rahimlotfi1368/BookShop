using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Features.Security.Command;

namespace Presentation.Features.Security;

public class SecurityController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var response=await _mediator.Send(command);

        if (response.Success == true)
            return RedirectToAction("Login");
        
        return View(response);
    }
}