using BuberDinner.Application.Common.Interfaces;
using BuberDinner.Application.Common.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuberDinner.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    #region Fields :
    private IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    #endregion

    #region CTORS :
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }
    #endregion

    #region Operations :
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName,firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    #endregion
}
