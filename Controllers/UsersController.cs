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
    }
}
