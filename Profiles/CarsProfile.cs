using AutoMapper;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Profiles
{
    public class CarsProfile : Profile
    {
        public CarsProfile()
        {
            CreateMap<Cars, CarsReadDto>();
            CreateMap<CarsCreateDto, Cars>();
            CreateMap<CarsUpdateDto, Cars>();
            CreateMap<Cars, CarsUpdateDto>();
        }
    }
}
