using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentCarAPI.Data;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentCarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(UserManager<IdentityUser> userManager, IUsersRepo repository,
            IMapper mapper, IOptions<AppSettings> appSettings, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _roleManager = roleManager;
        }

        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<UsersReadDto>> GetAllUsers()
        {
            var usersItems = _repository.GetAllUsers();
            var usersDto = _mapper.Map<IEnumerable<UsersReadDto>>(usersItems);
            return Ok(usersDto);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersReadDto>> GetUsersById(String Id)
        {
            var usersItems = await _repository.GetUsersById(Id);
            if (usersItems != null)
            {
                var usersDto = _mapper.Map<UsersReadDto>(usersItems);
                return Ok(usersDto);
            }
            return NotFound();
        }

        [Route("Register/User")]
        public async Task<ActionResult> RegisterNewUser(UsersCreateDto usersCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(Temp => Temp.Errors));

            var usersModel = _mapper.Map<IdentityUser>(usersCreateDto);
            usersModel.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(usersModel,usersCreateDto.Password);
            //_repository.CreateUsers(usersModel);//Nothing
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(usersModel, UserRoles.User);

            return Ok(new { Status = "Success", Message = "User created successfully!", });
        }

        [Authorize]
        //[Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [Route("Register/Admin")]
        public async Task<IActionResult> RegisterNewAdmin(UsersCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(Temp => Temp.Errors));

            var usersModel = _mapper.Map<IdentityUser>(userCreateDto);
            usersModel.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(usersModel, userCreateDto.Password);
            if (!result.Succeeded)
                return BadRequest(new { Status = "Error", Message = result.Errors });

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(usersModel, UserRoles.Admin);
            }

            return Ok(new { Status = "Success", Message = "Admin created successfully!" });
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsersLoginDto usersLoginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(Temp => Temp.Errors));

            var usersModel = await _userManager.FindByNameAsync(usersLoginDto.UserName);
            if (await _userManager.CheckPasswordAsync(usersModel, usersLoginDto.Password))
            {
                var token = _repository.Login(usersModel, _appSettings).Result;
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo });
            }
            return BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsers(String id)
        {
            var usersFromRepo = await _userManager.FindByIdAsync(id);
            if (usersFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteUsers(usersFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUsers(String id, UsersUpdateDto usersUpdateDto)
        {
            var usersFromRepo = await _userManager.FindByIdAsync(id);
            if (usersFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(usersUpdateDto, usersFromRepo);
            _repository.UpdateUsers(usersFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialUsersUpdate(String id, JsonPatchDocument<UsersUpdateDto> patchDocument)
        {
            var usersFromRepo = await _userManager.FindByIdAsync(id);
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
