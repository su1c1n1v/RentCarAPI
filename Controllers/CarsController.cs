using AutoMapper;
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
            var carsModel = _mapper.Map<Cars>(carsCreateDto);
            _repository.CreateCar(carsModel);
            _repository.SaveChanges();
            var carReadDto = _mapper.Map<CarsReadDto>(carsModel);
            //return CreatedAtRoute(nameof(GetCarById), new { Id = carReadDto.Id, carReadDto });
            return Ok(carReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarsReadDto>> GetAllCars()
        {
            var carsItens = _repository.GetAllCars();
            var carsReadDto = _mapper.Map<IEnumerable<CarsReadDto>>(carsItens);
            return Ok(carsReadDto);
        }

        [HttpGet("{id}", Name = "GetCarById")]
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

        [HttpPut("{id}")]
        public ActionResult UpdateCars(int id, CarsUpdateDto carsUpdateDto)
        {
            var carsModelFromRepo = _repository.GetCarsById(id);
            if (carsModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(carsUpdateDto, carsModelFromRepo);
            _repository.UpdateCars(carsModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCarsUpdate(int id, JsonPatchDocument<CarsUpdateDto> patchDocument)
        {
            var carsModelFromRepo = _repository.GetCarsById(id);
            if (carsModelFromRepo == null)
            {
                return NotFound();
            }

            var carsToPatch = _mapper.Map<CarsUpdateDto>(carsModelFromRepo);
            patchDocument.ApplyTo(carsToPatch, ModelState);
            if (!TryValidateModel(carsToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(carsToPatch, carsModelFromRepo);
            _repository.UpdateCars(carsModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCars(int id)
        {
            var carsModelFromRepo = _repository.GetCarsById(id);
            if (carsModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCars(carsModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
