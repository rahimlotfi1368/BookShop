using AutoMapper;
using FluentValidation;
using MediatR;
using Presentation.Shared.Application.Contracts;
using Presentation.Shared.Application.Resorces;
using Presentation.Shared.Domain.Entities;
using Presentation.Shared.Extensions;
using Presentation.Shared.Infrastracture.Persistence;

namespace Presentation.Features.Security.Command;

public record RegisterUserCommand(string Username, string Password, string Role) : IRequest<RegisterUserResponse>;

public class RegisterUserCommandHandler(
    IValidator<RegisterUserCommand> registerUserCommandValidator,
    IUnitOfWork  unitOfWork,
    IMapper mapper)
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUnitOfWork  _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    { 
        RegisterUserResponse registerUserResponse ;
        
       var validationResult = await registerUserCommandValidator.ValidateAsync(request, cancellationToken);
       
       if (validationResult.IsValid == false && validationResult.Errors.Count > 0)
       {
           registerUserResponse=validationResult.ExtractValidationerrors<RegisterUserResponse>();
           
           return registerUserResponse;
       }

       var user = _mapper.Map<User>(request);

         await _unitOfWork
           .Repository<User>()
           .AddAsync(user);

       await _unitOfWork.CompleteAsync();

       registerUserResponse = new RegisterUserResponse(Messages.Ok, true)
       {
            UserId = user.Id
       };

       return registerUserResponse;

    }
}
