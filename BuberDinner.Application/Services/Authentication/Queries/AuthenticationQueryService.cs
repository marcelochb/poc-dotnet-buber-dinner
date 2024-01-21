using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

namespace BuberDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1 - Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredential;
        }

        // 2 - Validate the password is correct
        if (user.Password != password)
        {
            return new[] { Errors.Authentication.InvalidCredential };
        }
        // 3 - Generate JWT token
        var token = _jwtTokenGenerator.GeneratorToken(user);
        return new AuthenticationResult(user, token);
    }
}