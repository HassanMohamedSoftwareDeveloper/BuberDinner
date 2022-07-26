using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    #region Fields :
    private IAuthenticationService _authenticationService;
    #endregion

    #region CTORS :
    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    #endregion

    #region Endpoints :
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        return registerResult.Match(
              authResult => Ok(MapAuthResult(authResult)),
              errors => Problem(errors)
              );
    }
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> loginResult = _authenticationService.Login(request.Email, request.Password);
        return loginResult.Match(
              authResult => Ok(MapAuthResult(authResult)),
              errors => Problem(errors)
              );
    }
    #endregion

    #region Helpers :
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
        return response;
    }
    #endregion
}