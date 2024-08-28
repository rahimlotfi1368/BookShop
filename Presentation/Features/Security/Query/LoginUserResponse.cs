using Presentation.Shared.Application.Dtos.Response;
using Presentation.Shared.Domain.Entities;

namespace Presentation.Features.Security.Query;

public class LoginUserResponse:Response
{
    public User User { get; set; }
    public LoginUserResponse(string message, bool success) : base(message, success)
    {
    }

    public LoginUserResponse() : base()
    {
       
    }
}