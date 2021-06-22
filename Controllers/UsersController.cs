using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IUsersRepo repository, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
        public ActionResult<UsersReadDto> GetUsersById(String Id)
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
        public async Task<ActionResult> CreateNewUser(UsersCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(Temp => Temp.Errors));

            var usersModel = _mapper.Map<IdentityUser>(userCreateDto);
            usersModel.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(usersModel);

            await _signInManager.SignInAsync(usersModel, false);
            var usersReadDto = _mapper.Map<UsersReadDto>(usersModel);
            return Ok(usersReadDto);
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteUsers(String id)
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
        public ActionResult UpdateUsers(String id, UsersUpdateDto usersUpdateDto)
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
        public ActionResult PartialCarsUpdate(String id, JsonPatchDocument<UsersUpdateDto> patchDocument)
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
