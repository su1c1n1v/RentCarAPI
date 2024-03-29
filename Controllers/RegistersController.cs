﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
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
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly IRegistersRepo _registersRepository;
        private readonly ICarsRepo _carsRepository;
        private readonly IUsersRepo _usersRepository;
        private readonly IMapper _mapper;

        public RegistersController(ICarsRepo carsRepository, IUsersRepo usersRepository, IRegistersRepo registersRepository, IMapper mapper)
        {
            _registersRepository = registersRepository;
            _carsRepository = carsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<RegistersReadDto> GetRegistersById(int Id)
        {
            var registerItem = _registersRepository.GetRegistersById(Id);
            var registerReadDto = _mapper.Map<RegistersReadDto>(registerItem);
            return Ok(registerReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegistersReadDto>> GetAllRegisters()
        {
            var registersItens = _registersRepository.GetAllRegisters();
            var registersReadDto = _mapper.Map<IEnumerable<RegistersReadDto>>(registersItens);
            return Ok(registersReadDto);
        }

        [HttpPost]
        public ActionResult<RegistersReadDto> CreateRegister(RegistersCreateDto registersCreateDto)
        {
            var registerModel = _mapper.Map<Registers>(registersCreateDto);
            var userModel = _usersRepository.GetUsersById(registerModel.UserId).Result;
            var carsModel = _carsRepository.GetCarsById(registerModel.CarId.Value);

            if (_carsRepository.IsCarAvailable(carsModel) || userModel == null)
                return BadRequest(new {Status = "Failed", Error = "Car it is not available!"});          
            if (userModel == null) 
                return BadRequest(new { Status = "Failed", Error = "User ID is invalid!" });
            
            _registersRepository.CreateRegisters(registerModel);
            _registersRepository.SaveChanges();
            var registersCreated = _registersRepository.GetRegistersById(registerModel.Id);
            return Ok(_mapper.Map<RegistersReadDto>(registersCreated));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRegisters(int id)
        {
            var RegistersFromRepo = _registersRepository.GetRegistersById(id);
            if (RegistersFromRepo == null)
            {
                return NotFound();
            }
            _registersRepository.DeleteRegisters(RegistersFromRepo);
            _registersRepository.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateRegister(int id, RegistersUpdateDto registersUpdateDto)
        {
            var registersFromRepo = _registersRepository.GetRegistersById(id);
            if (registersFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(registersUpdateDto, registersFromRepo);
            _registersRepository.UpdateRegisters(registersFromRepo);
            _registersRepository.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialRegistersUpdate(int id, JsonPatchDocument<RegistersUpdateDto> patchDocument)
        {
            var registersFromRepo = _registersRepository.GetRegistersById(id);
            if (registersFromRepo == null)
            {
                return NotFound();
            }

            var registersToPatch = _mapper.Map<RegistersUpdateDto>(registersFromRepo);
            patchDocument.ApplyTo(registersToPatch, ModelState);
            if (!TryValidateModel(registersToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(registersToPatch, registersFromRepo);
            _registersRepository.UpdateRegisters(registersFromRepo);
            _registersRepository.SaveChanges();
            return NoContent();
        }
    }
}
