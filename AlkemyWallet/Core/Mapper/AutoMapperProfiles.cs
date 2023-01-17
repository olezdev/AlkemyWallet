using AlkemyWallet.Core.Helper;
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
        CreateMap<User, AuthMeDTO>()
            .ForMember(dest => dest.Role,
            opt => opt.MapFrom(src => src.Role.Name));

        // Accounts
        CreateMap<Account, AccountsDTO>();
        CreateMap<Account, AccountDetailsDTO>()
            .ForMember(dest => dest.User,
            opt => opt.MapFrom(src => $"{src.User.LastName} {src.User.FirstName}"))
            .ForMember(dest => dest.Email,
            opt => opt.MapFrom(src => src.User.Email));
        CreateMap<Account, AccountCreatedDTO>();
        CreateMap<AccountToUpdateDTO, Account>();
        CreateMap<Account, AccountUpdatedDTO>();

        //Transactions
        CreateMap<Transaction, TransactionsDTO>();
        CreateMap<Transaction, TransactionDetailsDTO>()
            .ForMember(dest => dest.User,
            opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(dest => dest.ToAccountUserId,
            opt => opt.MapFrom(src => src.ToAccount.UserId));
        CreateMap<TransactionToCreateDTO, Transaction>();
        CreateMap<Transaction, TransactionCreatedDTO>();
        CreateMap<TransactionToUpdateDTO, Transaction>();
        CreateMap<Transaction, TransactionUpdatedDTO>();
    }
}
