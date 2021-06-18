using Microsoft.EntityFrameworkCore;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public class SqlRegistesRepo : IRegistersRepo
    {
        private readonly ApplicationContext _context;

        public SqlRegistesRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateRegisters(Registers rgt)
        {
            if(rgt == null)
            {
                throw new ArgumentNullException(nameof(Registers));
            }
            rgt.StartDate = DateTime.Now;
            _context.Registers.Add(rgt);
        }

        public void DeleteRegisters(Registers rgt)
        {
            if (rgt == null)
            {
                throw new ArgumentNullException(nameof(Registers));
            }
            _context.Registers.Remove(rgt);
        }

        public IEnumerable<Registers> GetAllRegisters()
        {
            return _context.Registers
                .Include(Temp => Temp.Car)
                .Include(Temp => Temp.User)
                .ToList();
        }

        public Registers GetRegistersById(int Id)
        {
            return _context.Registers.
                Include(Temp => Temp.Car)
                .Include(Temp => Temp.User)
                .FirstOrDefault(Temp => Temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateRegisters(Registers rgt)
        {
            //Nothing
        }
    }
}
