using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(string email, string password);
    Task<AuthMeDTO> GetProfile(int id);
}
