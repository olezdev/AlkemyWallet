using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AlkemyWallet.Core.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace AlkemyWallet.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<string> Login(string email, string password)
    {
        //Validading credentials
        var user = await _unitOfWork.UserRepository.ExpressionGetAsync(
            u => u.Email == email && u.Password == password, 
            null, 
            "Role");

        //HelperJWT Instance
        var jwt = new HelperJWT(_configuration);

        if (user is not null)
        {
            var token = jwt.CreateToken(user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        else
        {
            return null;
        }
    }
}
