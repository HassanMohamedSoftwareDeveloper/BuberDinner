using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
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
        FluentResults.Result<AuthenticationResult> registerResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        if (registerResult.IsSuccess)
            return Ok(MapAuthResult(registerResult.Value));

        var firstError = registerResult.Errors.First();
        if (firstError is DuplicateEmailError)
            Problem(statusCode: StatusCodes.Status409Conflict, title: "Email already exists.");

        return Problem();
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
        return response;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
        return Ok(response);
    }
    #endregion
}