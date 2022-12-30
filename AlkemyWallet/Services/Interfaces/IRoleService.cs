using AlkemyWallet.Core.Models.DTO;

namespace AlkemyWallet.Services.Interfaces;

public interface IRoleService
{
    Task<List<RoleDTO>> GetAllAsync();
}
