using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public class SqlCarsRepo : ICarsRepo
    {
        private readonly ApplicationContext _context;

        public SqlCarsRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateCar(Cars crs)
        {
            if (crs == null)
            {
                throw new ArgumentNullException(nameof(Cars));
            }
            crs.Date = DateTime.Now;
            crs.Available = true;
            _context.Cars.Add(crs);
        }

        public IEnumerable<Cars> GetAllCars()
        {
            return _context.Cars.ToList();
        }

        public Cars GetCarsById(int Id)
        {
            return _context.Cars.FirstOrDefault(Temp => Temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
