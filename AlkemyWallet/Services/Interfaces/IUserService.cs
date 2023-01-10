using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IUserService
{
    Task<List<UsersDTO>> GetAllAsync();
    Task<UserDetailsDTO> GetByIdAsync(int id);
}
