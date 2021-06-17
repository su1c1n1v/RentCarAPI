using Microsoft.AspNetCore.Mvc;
using RentCarAPI.Data;
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

        public UsersController(IUsersRepo repository)
        {
            _repository = repository; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetAllCars()
        {
            return Ok(_repository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Users>> GetUsersById(int Id)
        {
            var user = _repository.GetUsersById(Id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Users> CreateUsers(Users user)
        {
            _repository.CreateUsers(user);
            _repository.SaveChanges();
            return Ok(user);
        }
    }
}
