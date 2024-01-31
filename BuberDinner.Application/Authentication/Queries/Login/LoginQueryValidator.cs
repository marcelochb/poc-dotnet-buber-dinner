using FluentValidation;

namespace BuberDinner.Application.Authentication.Queries.Login;

public class LoginQueryValidaton : AbstractValidator<LoginQuery>
{
    public LoginQueryValidaton()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}