using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IAccountService
{
    Task<List<AccountsDTO>> GetAllAsync();
    Task<AccountDetailsDTO> GetByIdAsync(int id);
}
