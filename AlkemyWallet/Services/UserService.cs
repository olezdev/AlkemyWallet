using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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

    public async Task<PagedResponse<UsersDTO>> GetPaginated(int page, int pageSize)
    {
        var users = await _unitOfWork.UserRepository.GetPagedAsync(page, pageSize);

        var usersDTO = _mapper.Map<List<UsersDTO>>(users);

        PagedResponse<UsersDTO>? pagedResponse;

        if (page > users.TotalPages)
        {
            return null;
        }
        else
        {
            var url = "/users";

            pagedResponse = new PagedResponse<UsersDTO>
            {
                nextPage = users.HasNextPage ?
                                $"{url}?page={page + 1}"
                                : "",
                previousPage = (users.Count > 0 && users.HasPreviousPage) ?
                                    $"{url}?page={page - 1}" :
                                    "",
                pageIndex = users.PageIndex,
                totalPages = users.TotalPages,
                data = usersDTO
            };
        }

        return pagedResponse;
    }


    public async Task<UserDetailsDTO> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return _mapper.Map<UserDetailsDTO>(user);
    }

    public async Task<UserRegisteredDTO> Register(UserRegisterDTO userDTO)
    {
        var userExist = await _unitOfWork.UserRepository.ExpressionGetAsync(
            u => u.Email == userDTO.Email,
            null, "");

        if (userExist != null)
            return null;
        else
        {
            var newUser = new User
            {
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Password = userDTO.Password,
                Points = 1,
                RoleId = 2
            };

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserRegisteredDTO>(newUser);
        }
    }

    public async Task<User> UpdateAsync(int id, UserUpdateDTO userDTO)
    {
        try
        {
            var userToUpdate = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (userToUpdate != null)
            {
                userToUpdate = _mapper.Map(userDTO, userToUpdate);

                var userUpdated = await _unitOfWork.UserRepository.UpdateAsync(userToUpdate);
                await _unitOfWork.SaveChangesAsync();
                return userUpdated;
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userToDelete = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (userToDelete == null)
            return false;
        else
        {
            _unitOfWork.UserRepository.Delete(userToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        return true;
    }
}
