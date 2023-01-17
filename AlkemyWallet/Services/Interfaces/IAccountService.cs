using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IAccountService
{
    Task<List<AccountsDTO>> GetAllAsync();
    Task<AccountDetailsDTO> GetByIdAsync(int id);
    Task<AccountCreatedDTO> CreateAsync(int userId);
    Task<AccountUpdatedDTO> UpdateBlockedAsync(int id, AccountToUpdateDTO accountDTO);
}
