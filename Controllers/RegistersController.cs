﻿using Microsoft.AspNetCore.Mvc;
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
    public class RegistersController : ControllerBase
    {
        private readonly IRegistersRepo _repository;

        public RegistersController(IRegistersRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Registers>> GetRegistersById(int Id)
        {
            return Ok(_repository.GetRegisterById(Id));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Registers>> GetAllRegisters()
        {
            return Ok(_repository.GetAllRegisters());
        }
    }
}
