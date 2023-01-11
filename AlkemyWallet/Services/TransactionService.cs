using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Services;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TransactionDTO>> GetAllAsync()
    {
        var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();
        return _mapper.Map<List<TransactionDTO>>(transactions);
    }

    public async Task<TransactionDetailsDTO> GetById(int id, int userId)
    {
        var transaction = await _unitOfWork.TransactionRepository
            .ExpressionGetAsync(
            u => u.Id == id && u.UserId == userId,
            null,
            "User,Account,ToAccount");
        if (transaction is null)
            return null;

        var transactionDTO = new TransactionDetailsDTO();
        transactionDTO = _mapper.Map(transaction,transactionDTO);
        
        return transactionDTO;
    }
}
