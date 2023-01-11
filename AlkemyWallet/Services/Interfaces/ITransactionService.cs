using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface ITransactionService
{
    Task<List<TransactionDTO>> GetAllAsync();
    Task<TransactionDetailsDTO> GetById(int id, int userId);
}
