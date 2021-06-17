using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RentCarAPI.Data;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IRegistersRepo _repository;
        private readonly IMapper _mapper;

        public RegistersController(IRegistersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<RegistersReadDto> GetRegistersById(int Id)
        {
            var registerItem = _repository.GetRegisterById(Id);
            var registerReadDto = _mapper.Map<RegistersReadDto>(registerItem);
            return Ok(registerReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegistersReadDto>> GetAllRegisters()
        {
            var registersItens = _repository.GetAllRegisters();
            var registersReadDto = _mapper.Map<IEnumerable<RegistersReadDto>>(registersItens);
            return Ok(registersReadDto);
        }

        [HttpPost]
        public ActionResult<RegistersReadDto> CreateRegister(RegistersCreateDto registersCreateDto)
        {
            var registerModel = _mapper.Map<Registers>(registersCreateDto);
            _repository.CreateRegister(registerModel);
            _repository.SaveChanges();
            var registersCreated = _repository.GetRegisterById(registerModel.Id);
            return Ok(_mapper.Map<RegistersReadDto>(registersCreated));
        }
    }
}
