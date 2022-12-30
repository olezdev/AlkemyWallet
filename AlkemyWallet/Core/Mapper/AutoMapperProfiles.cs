﻿using AlkemyWallet.Entities;
using AlkemyWallet.Core.Models.DTO;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Role, RoleDTO>();
        CreateMap<RoleDTO, User>();
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
    }
}
