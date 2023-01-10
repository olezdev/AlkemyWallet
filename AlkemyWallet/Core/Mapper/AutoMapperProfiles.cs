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
        CreateMap<RoleDTO, User>();
        // Users
        CreateMap<User, UsersDTO>();
        //CreateMap<UsersDTO, User>();
        CreateMap<User, UserDetailsDTO>();
        // Accounts
        CreateMap<Account, AccountDTO>();
        CreateMap<AccountDTO, Account>();
        //Transactions
        CreateMap<Transaction, TransactionDTO>();
        CreateMap<TransactionDTO, Transaction>();
    }
}
