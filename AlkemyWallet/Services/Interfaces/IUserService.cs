using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Services.Interfaces;

public interface IUserService
{
    Task<List<UsersDTO>> GetAllAsync();
    Task<UserDetailsDTO> GetByIdAsync(int id);
    Task<UserRegisteredDTO> Register(UserRegisterDTO userDTO);
    Task<User> UpdateAsync(int id, UserUpdateDTO userDTO);
    Task<bool> DeleteAsync(int id);
    Task<PagedResponse<UsersDTO>> GetPaginated(int page, int pageSize);
}
