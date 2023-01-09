using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AutoMapper;

namespace AlkemyWallet.Services;

public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<RoleDTO>> GetAllAsync()
    {
        try
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return _mapper.Map<List<RoleDTO>>(roles);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task<RoleDTO> GetByIdAsync(int id)
    {
        try
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleDTO>(role);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
