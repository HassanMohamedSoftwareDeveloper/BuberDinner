using BuberDinner.Application.Common.Interfaces;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    #region Fields :
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    #endregion

    #region CTORS :
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    #endregion

    #region Service Operations :
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(Guid.NewGuid(), "FirstName", "LastName", email, "Token");
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        //Check if user already exists
        //Create user (generate unique ID)
        var userId = Guid.NewGuid();
        //Create JWT
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return new AuthenticationResult(userId, firstName, lastName, email, token);
    }
    #endregion
}
