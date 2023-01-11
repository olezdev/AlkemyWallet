using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AlkemyWallet.Core.Helper;
using System.IdentityModel.Tokens.Jwt;
using AlkemyWallet.Core.Models.DTO;
using AutoMapper;

namespace AlkemyWallet.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
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

    public async Task<AuthMeDTO> GetProfile(int id)
    {
        var profile = await _unitOfWork.UserRepository
            .ExpressionGetAsync(u => u.Id == id, null, "Role");
        var profileDTO = new AuthMeDTO();
        profileDTO = _mapper.Map(profile, profileDTO);
        if (profileDTO == null)
            return null;

        return profileDTO;
    }
}
