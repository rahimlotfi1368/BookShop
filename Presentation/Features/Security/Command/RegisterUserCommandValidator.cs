using FluentValidation;

namespace Presentation.Features.Security.Command;

public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(command => command.Role)
            .NotEmpty().WithMessage("Role is required.");
    }
}