using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    #region Fields :
    private readonly ISender _mediator;
    #endregion

    #region CTORS :
    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Endpoints :
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);
        return registerResult.Match(
              authResult => Ok(MapAuthResult(authResult)),
              errors => Problem(errors)
              );
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        ErrorOr<AuthenticationResult> loginResult = await _mediator.Send(query);
        if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials)
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: loginResult.FirstError.Description);

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