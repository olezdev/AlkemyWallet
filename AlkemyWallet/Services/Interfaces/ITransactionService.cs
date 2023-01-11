using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Services.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionDTO>> GetAllAsync();
    Task<TransactionDetailsDTO> GetByIdAsync(int id, int userId);
    Task<TransactionCreatedDTO> CreateAsync(TransactionToCreateDTO transactionDTO);

}
