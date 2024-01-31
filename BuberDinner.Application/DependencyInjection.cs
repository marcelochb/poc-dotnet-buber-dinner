using Microsoft.Extensions.DependencyInjection;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using ErrorOr;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Common.Interfaces.Behaviors;
using FluentValidation;
using System.Reflection;

namespace BuberDinner.Application;


public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    services.AddScoped<
      IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>, 
      ValidationRegisterCommandBehavior>();
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    return services;

  }
}