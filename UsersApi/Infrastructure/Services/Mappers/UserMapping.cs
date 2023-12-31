﻿using AutoMapper;
using Entities.DTOs.UserDTO;
using Entities.Models;

namespace Infrastructure.Services.Mappers
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUser, User>();
            CreateMap<User, UserView>();
            CreateMap<UpdateUser, User>();
        }
    }
}
