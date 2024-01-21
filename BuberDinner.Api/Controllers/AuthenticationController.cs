using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;
[Route("auth")]
public class AuthenticationController : ApiController
{

  private readonly IAuthenticationCommandService _authenticationCommandService;
  private readonly IAuthenticationQueryService _authenticationQueryService;
  private readonly IMapper _mapper;

    public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService, IMapper mapper)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
        _mapper = mapper;
    }

    [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationCommandService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return authResult.Match(
          authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
          errors => Problem(errors)
        );
    }

    [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationQueryService.Login(request.Email, request.Password);
    return authResult.Match(
      authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
      errors => Problem(errors)
    );
  }
}