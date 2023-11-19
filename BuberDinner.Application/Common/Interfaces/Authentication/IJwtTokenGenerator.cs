using System;
using BuberDinner.Domain.Entities;
namespace BuberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
  string GeneratorToken(User user);
}