using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public interface ICarsRepo
    {
        //Get all cars in the application
        IEnumerable<Cars> GetAllCars();

        //Get a car by it ID
        Cars GetCarsById(int Id);

        //Create a new car to the DB
        void CreateCar(Cars crs);

        //Save the thinks added in the DB
        bool SaveChanges();
    }
}
