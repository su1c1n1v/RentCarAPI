using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<IdentityUser, UsersReadDto>();
            CreateMap<UsersCreateDto, IdentityUser>();
            CreateMap<UsersUpdateDto, IdentityUser>();
            CreateMap<IdentityUser, UsersUpdateDto>();
        }
    }
}
