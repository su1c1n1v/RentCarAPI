using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public class MockCarsRepo : ICarsRepo
    {

        List<Cars> cars = new List<Cars>
        {
            new Cars{Id = 0, Available=false, Brand = "Fiat", Color= "Gray", Date=DateTime.Now, Name="Uno"},
            new Cars{Id = 1, Available=true, Brand = "Fiat", Color= "Gray", Date=DateTime.Now, Name="Palio"},
            new Cars{Id = 2, Available=true, Brand = "Fiat", Color= "Gray", Date=DateTime.Now, Name="Siena"},
            new Cars{Id = 3, Available=true, Brand = "Fiat", Color= "Gray", Date=DateTime.Now, Name="Strada"}
        };

        public void CreateCar(Cars car)
        {

        }

        public IEnumerable<Cars> GetAllCars()
        {
            return cars;
        }

        public Cars GetCarsById(int Id)
        {
            return cars.FirstOrDefault(temp => temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
