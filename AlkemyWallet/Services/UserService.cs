using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UsersDTO>> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<List<UsersDTO>>(users);
    }

    public async Task<UserDetailsDTO> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return _mapper.Map<UserDetailsDTO>(user);
    }
}
