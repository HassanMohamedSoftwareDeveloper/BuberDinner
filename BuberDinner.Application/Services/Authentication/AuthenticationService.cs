using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;
using FluentResults;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    #region Fields :
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    #endregion

    #region CTORS :
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    #endregion

    #region Service Operations :
    public AuthenticationResult Login(string email, string password)
    {
        //1.Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
            throw new Exception("User with given email does not exist.");
        //2.Validate the password is correct
        if (user.Password != password)
            throw new Exception("Invalid password.");
        //3.Create Jwt
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }

    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        //1.Validate the user doesn;t exist
        if (_userRepository.GetUserByEmail(email) is not null)
            return Result.Fail<AuthenticationResult>(new DuplicateEmailError());
        //2.Create user (generate unique ID)
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.AddUser(user);
        //Create JWT
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
    #endregion
}
