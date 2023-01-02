using AlkemyWallet.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlkemyWallet.Core.Helper;

public class HelperJWT
{
    private readonly IConfiguration _configuration;

    public HelperJWT(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JwtSecurityToken? CreateToken(User user)
    {
        var authClaims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

        //security key
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

        var credentials = new SigningCredentials(
            authSigningKey,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(8),
            claims: authClaims,
            signingCredentials: credentials
        );

        return token;
    }
}
