using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UserManagementAPI.Services;

public class EmailJwtProvider(IOptions<EmailJwtOptions> jwtOptions) : IEmailJwtProvider
{
    private readonly EmailJwtOptions _jwtOptions = jwtOptions.Value;
    
    public string GenerateToken(string userId)
    {
        Claim[] claims = [new("userId", userId)];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }

    public string? GetUserId(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        try
        {
            handler.ValidateToken(token, validations, out var validatedToken);
            var jwtToken = handler.ReadJwtToken(token);
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId");
            return userIdClaim?.Value;
        }
        catch
        {
            return null;
        }
    }
    
    
}