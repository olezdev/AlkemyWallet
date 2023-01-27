using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Services.Interfaces;

public interface IAccountService
{
    Task<List<AccountsDTO>> GetAllAsync();
    Task<AccountDetailsDTO> GetByIdAsync(int id);
    Task<AccountCreatedDTO> CreateAsync(int userId);
    Task<AccountUpdatedDTO> UpdateBlockedAsync(int id, AccountToUpdateDTO accountDTO);
    Task<bool> DeleteById(int id);
    Task<PagedResponse<AccountsDTO>> GetPaginated(int page, int pageSize);
    Task<TransactionDTO> VerifyAccountAsync(int id, int userId, TransactionDTO transactionDTO);
    Task<TransactionDTO> DepositAsync(TransactionDTO transactionDTO, Account account);
}
