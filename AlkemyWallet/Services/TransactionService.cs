using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
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

    public async Task<TransactionDetailsDTO> GetByIdAsync(int id, int userId)
    {
        var transaction = await _unitOfWork.TransactionRepository
            .ExpressionGetAsync(
                u => u.Id == id && u.UserId == userId,
                null,
                "User,Account,ToAccount");

        if (transaction is null)
            return null;

        var transactionDTO = new TransactionDetailsDTO();
        transactionDTO = _mapper.Map(transaction, transactionDTO);

        return transactionDTO;
    }

    public async Task<TransactionCreatedDTO> CreateAsync(TransactionToCreateDTO transactionDTO)
    {
        try
        {
            var transaction = new Transaction();
            _mapper.Map(transactionDTO, transaction);
            transaction.Date = DateTime.Now;

            var transactionToCreate = await _unitOfWork.TransactionRepository.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            var transactionCreated = new TransactionCreatedDTO();
            _mapper.Map(transactionToCreate, transactionCreated);

            return transactionCreated;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<TransactionUpdatedDTO> UpdateAsync(int id, int userId, TransactionToUpdateDTO transactionDTO)
    {
        try
        {
            var transactionToUpdate = await _unitOfWork.TransactionRepository
                .ExpressionGetAsync(
                    u => u.Id == id && u.UserId == userId,
                    null, "");

            if (transactionToUpdate is null)
                return null;


            _mapper.Map(transactionDTO, transactionToUpdate);

            _ = _unitOfWork.TransactionRepository.UpdateAsync(transactionToUpdate);
            await _unitOfWork.SaveChangesAsync();

            var transactionUpdatedDTO = new TransactionUpdatedDTO();
            _mapper.Map(transactionToUpdate, transactionUpdatedDTO);

            return transactionUpdatedDTO;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }



    }
}
