using AlkemyWallet.Core.Helper;
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

    public async Task<List<TransactionsDTO>> GetAllAsync()
    {
        var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();
        return _mapper.Map<List<TransactionsDTO>>(transactions);
    }

    public async Task<PagedResponse<TransactionsDTO>> GetPaginated(int page, int pageSize)
    {
        var transactions = await _unitOfWork.TransactionRepository.GetPagedAsync(page, pageSize);

        var transactionsDTO = _mapper.Map<List<TransactionsDTO>>(transactions);
        
        PagedResponse<TransactionsDTO>? pagedResponse;

        if (page > transactions.TotalPages)
        {
            return null;
        }
        else
        {
            var url = "/transactions";

            pagedResponse = new PagedResponse<TransactionsDTO>
            {
                nextPage = transactions.HasNextPage ?
                                $"{url}?page={page + 1}"
                                : "",
                previousPage = (transactions.Count > 0 && transactions.HasPreviousPage) ?
                                    $"{url}?page={page - 1}" :
                                    "",
                pageIndex = transactions.PageIndex,
                totalPages = transactions.TotalPages,
                data = transactionsDTO
            };
        }

        return pagedResponse;
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

    public async Task<bool> DeleteAsync(int id)
    {
        var transactionToDelete = await _unitOfWork.TransactionRepository.GetByIdAsync(id);
        if (transactionToDelete is null)
            return false;
        try
        {
            _unitOfWork.TransactionRepository.Delete(transactionToDelete);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
