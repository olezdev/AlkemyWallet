using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Roles
        CreateMap<Role, RoleDTO>();
        // Users
        CreateMap<User, UsersDTO>();
        CreateMap<User, UserDetailsDTO>();
        CreateMap<User, UserRegisteredDTO>();
        CreateMap<UserUpdateDTO, User>();
        // Accounts
        CreateMap<Account, AccountDTO>();
        //Transactions
        CreateMap<Transaction, TransactionDTO>();
    }
}
