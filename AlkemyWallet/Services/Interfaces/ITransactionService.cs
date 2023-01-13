using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionsDTO>> GetAllAsync();
    Task<TransactionDetailsDTO> GetByIdAsync(int id, int userId);
    Task<TransactionCreatedDTO> CreateAsync(TransactionToCreateDTO transactionDTO);
    Task<TransactionUpdatedDTO> UpdateAsync(int id, int userId, TransactionToUpdateDTO transactionDTO);
    Task<bool> DeleteAsync(int id);
    Task<PagedResponse> GetPaginated(int page, int pageSize);

}
