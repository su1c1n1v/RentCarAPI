using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCarAPI.Data;
using RentCarAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsersRepo _repository;
        private readonly IMapper _mapper;
        public AuthenticationController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, IUsersRepo repository, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewUser(UsersCreateDto userCreateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(Temp => Temp.Errors));

            var usersModel =  _mapper.Map<IdentityUser>(userCreateDto);
            usersModel.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(usersModel);

            await _signInManager.SignInAsync(usersModel,false);
            var usersReadDto = _mapper.Map<UsersReadDto>(usersModel);
            return Ok(usersReadDto);
        }
    }
}
