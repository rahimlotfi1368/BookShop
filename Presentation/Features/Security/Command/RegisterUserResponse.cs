using Presentation.Shared.Application.Dtos.Response;

namespace Presentation.Features.Security.Command;

public class RegisterUserResponse:Response
{
    public int UserId { get; set; }
    public RegisterUserResponse(string message, bool success) : base(message, success)
    {
    }

    public RegisterUserResponse() : base()
    {
       
    }
}