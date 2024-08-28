using FluentValidation;

namespace Presentation.Features.Security.Query;

public class LoginUserValidator:AbstractValidator<LoginUserQuery>
{
    public LoginUserValidator()
    {
        RuleFor(query => query.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(query => query.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        
    }
}