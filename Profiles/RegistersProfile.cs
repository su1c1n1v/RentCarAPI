using AutoMapper;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Profiles
{
    public class RegistersProfile : Profile
    {
        public RegistersProfile()
        {
            CreateMap<Registers, RegistersReadDto>();
            CreateMap<RegistersCreateDto, Registers>();
            CreateMap<RegistersUpdateDto, Registers>();
            CreateMap<Registers, RegistersUpdateDto>();
        }
    }
}
