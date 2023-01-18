using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AccountsDTO>> GetAllAsync()
    {
        var accounts = await _unitOfWork.AccountRepository.GetAllAsync();
        return _mapper.Map<List<AccountsDTO>>(accounts);
    }

    public async Task<PagedResponse<AccountsDTO>> GetPaginated(int page, int pageSize)
    {
        try
        {
            var accounts = await _unitOfWork.AccountRepository.GetPagedAsync(page, pageSize);
            var accountsDTO = _mapper.Map<List<AccountsDTO>>(accounts);

            PagedResponse<AccountsDTO>? pageResponse;

            if (page > accounts.TotalPages)
            {
                return null;
            }

            var url = "/accounts";
            pageResponse = new PagedResponse<AccountsDTO>
            {
                nextPage = accounts.HasNextPage ?
                                $"{url}?page={page + 1}"
                                : "",
                previousPage = (accounts.Count > 0 && accounts.HasPreviousPage) ?
                                    $"{url}?page={page - 1}"
                                    : "",
                pageIndex = accounts.PageIndex,
                totalPages = accounts.TotalPages,
                data = accountsDTO
            };
            return pageResponse;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AccountDetailsDTO> GetByIdAsync(int id)
    {
        try
        {
            var account = await _unitOfWork.AccountRepository.ExpressionGetAsync(
            a => a.Id == id, null, "User");

            AccountDetailsDTO accountDetailsDTO = new AccountDetailsDTO();
            return _mapper.Map(account, accountDetailsDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AccountCreatedDTO> CreateAsync(int userId)
    {
        var account = new Account
        {
            CreationDate = DateTime.Now,
            Money = 0,
            UserId = userId,
            IsBlocked = false
        };
        try
        {
            var accountCreated = await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AccountCreatedDTO>(accountCreated);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AccountUpdatedDTO> UpdateBlockedAsync(int id, AccountToUpdateDTO accountDTO)
    {
        var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (account is null)
            return null;

        _mapper.Map(accountDTO, account);

        try
        {
            var accountUpdate = await _unitOfWork.AccountRepository.UpdateAsync(account);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AccountUpdatedDTO>(accountUpdate);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<bool> DeleteById(int id)
    {
        var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
        if (account is null)
            return false;

        try
        {
            _unitOfWork.AccountRepository.Delete(account);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
