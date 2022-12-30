using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDTO>> GetAllAsync();
}
