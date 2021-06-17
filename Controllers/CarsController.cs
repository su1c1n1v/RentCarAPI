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
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepo _repository;

        public CarsController(ICarsRepo repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public ActionResult<Cars> CreateCars(Cars car)
        {
            _repository.CreateCar(car);
            _repository.SaveChanges();
            return Ok(car);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cars>> GetAllCars()
        {
            return Ok(_repository.GetAllCars());
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Cars>> GetCarById(int Id)
        {
            var car = _repository.GetCarsById(Id);
            if (car != null)
            {
                return Ok(car);
            }
            return NotFound();
        }
    }
}
