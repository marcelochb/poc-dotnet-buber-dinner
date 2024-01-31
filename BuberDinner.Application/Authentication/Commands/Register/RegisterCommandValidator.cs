using FluentValidation;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty();

        RuleFor(v => v.LastName)
            .NotEmpty();

        RuleFor(v => v.Email)
            .NotEmpty();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}
