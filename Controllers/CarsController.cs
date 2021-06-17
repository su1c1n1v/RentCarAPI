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
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepo _repository;
        private readonly IMapper _mapper;

        public CarsController(ICarsRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<CarsReadDto> CreateCars(CarsCreateDto carsCreateDto)
        {
            if (carsCreateDto == null)
            {
                throw new ArgumentNullException(nameof(Cars));
            }
            var carsModel = _mapper.Map<Cars>(carsCreateDto);
            _repository.CreateCar(carsModel);
            _repository.SaveChanges();
            return Ok(_mapper.Map<CarsReadDto>(carsModel));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarsReadDto>> GetAllCars()
        {
            var carsItens = _repository.GetAllCars();
            var carsReadDto = _mapper.Map<IEnumerable<CarsReadDto>>(carsItens);
            return Ok(carsReadDto);
        }

        [HttpGet("{id}")]
        public ActionResult<CarsReadDto> GetCarById(int Id)
        {
            var carItem = _repository.GetCarsById(Id);
            if (carItem != null)
            {
                var carsReadDto = _mapper.Map<CarsReadDto>(carItem);
                return Ok(carsReadDto);
            }
            return NotFound();
        }
    }
}
