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
        CreateMap<Account, AccountDTO>();
        //Transactions
        CreateMap<Transaction, TransactionDTO>();
        CreateMap<Transaction, TransactionDetailsDTO>()
            //.ForMember(dest => dest.User,
            //opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.AccountSource,
            opt => opt.MapFrom(src => $"{src.Account.User.FirstName} {src.Account.User.LastName}"))
            .ForMember(dest => dest.AccountDestination,
            opt => opt.MapFrom(src => $"{src.ToAccount.User.FirstName} {src.ToAccount.User.LastName}"));
    }
}
