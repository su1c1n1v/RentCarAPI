using AutoMapper;
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
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsersReadDto>> GetAllUsers()
        {
            var usersItems = _repository.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UsersReadDto>>(usersItems);
            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        public ActionResult<UsersReadDto> GetUsersById(int Id)
        {
            var usersItems = _repository.GetUsersById(Id);
            if (usersItems != null)
            {
                var usersDto = _mapper.Map<UsersReadDto>(usersItems);
                return Ok(usersDto);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<UsersReadDto> CreateUsers(UsersCreateDto userCreateDto)
        {
            var usersModel = _mapper.Map<Users>(userCreateDto);
            _repository.CreateUsers(usersModel);
            _repository.SaveChanges();
            var usersReadDto = _mapper.Map<UsersReadDto>(usersModel);
            return Ok(usersReadDto);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUsers(int id)
        {
            var usersFromRepo = _repository.GetUsersById(id);
            if (usersFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteUsers(usersFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateUsers(int id, UsersUpdateDto usersUpdateDto)
        {
            var usersFromRepo = _repository.GetUsersById(id);
            if (usersFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(usersUpdateDto, usersFromRepo);
            _repository.UpdateUsers(usersFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id}")]
        public ActionResult PartialCarsUpdate(int id, JsonPatchDocument<UsersUpdateDto> patchDocument)
        {
            var usersFromRepo = _repository.GetUsersById(id);
            if (usersFromRepo == null)
            {
                return NotFound();
            }

            var usersToPatch = _mapper.Map<UsersUpdateDto>(usersFromRepo);
            patchDocument.ApplyTo(usersToPatch, ModelState);
            if (!TryValidateModel(usersToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(usersToPatch, usersFromRepo);
            _repository.UpdateUsers(usersFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
