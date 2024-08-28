using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Application.Resorces;
using Presentation.Shared.Domain.Entities;
using Presentation.Shared.Extensions;

namespace Presentation.Features.Security.Query;

public record LoginUserQuery(string Username, string Password) : IRequest<LoginUserResponse>;
public class LoginUserQueryHandler(
    IHttpContextAccessor httpContextAccessor,
    IValidator<LoginUserQuery> validator,
    IMapper mapper,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<LoginUserQuery, LoginUserResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        LoginUserResponse  loginUserResponse ;
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
       
        if (validationResult.IsValid == false && validationResult.Errors.Count > 0)
        {
            loginUserResponse=validationResult.ExtractValidationerrors<LoginUserResponse>();
           
            return loginUserResponse;
        }

        var user=await _unitOfWork
            .UserRepository()
            .GetByUserNameAndPasswordAsync(request.Username, request.Password);

        if (user == null)
        {
            loginUserResponse = new LoginUserResponse(Messages.FailedValidation, false);

            return loginUserResponse;
        }
        else
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal);

            loginUserResponse = new LoginUserResponse(Messages.Ok, true)
            {
                User = user
            };

            return loginUserResponse;
        }
    }
}