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

        public void CreateRegister(Registers rgt)
        {
            if(rgt == null)
            {
                throw new ArgumentNullException(nameof(Registers));
            }
            _context.Registers.Add(rgt);
        }

        public IEnumerable<Registers> GetAllRegisters()
        {
            return _context.Registers.Include(Temp => Temp.Car).Include(Temp => Temp.User).ToList();
        }

        public Registers GetRegisterById(int Id)
        {
            return _context.Registers.FirstOrDefault(Temp => Temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
