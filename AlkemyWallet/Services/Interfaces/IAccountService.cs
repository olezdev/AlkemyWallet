using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IAccountService
{
    Task<List<AccountDTO>> GetAllAsync();
}
