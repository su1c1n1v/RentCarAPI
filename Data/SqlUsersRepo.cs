using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public class SqlUsersRepo : IUsersRepo
    {
        private readonly ApplicationContext _context;

        public SqlUsersRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateUsers(Users user)
        {
            return;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public Users GetUsersById(int Id)
        {
            return _context.Users.FirstOrDefault(Temp => Temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
