using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BuberDinner.Application.Common.Interfaces.Behaviors;

public class ValidationRegisterCommandBehavior :
    IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IValidator<RegisterCommand> _validator;

    public ValidationRegisterCommandBehavior(IValidator<RegisterCommand> validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request,
                                                      RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
                                                      CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }
        var errors = validationResult.Errors
            .ConvertAll(validationFailure => 
            Error.Validation(
                validationFailure.PropertyName, 
                validationFailure.ErrorMessage));
        return errors;
    }
}