using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public SqlUsersRepo(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ApplicationContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> CreateJWT(string Email)
        {
            return "Nothing";
        }

        public void CreateUsers(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
        }

        public void DeleteUsers(IdentityUser usr)
        {
            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }
            var registers = _context.Registers.Where(Temp => Temp.User.Id == usr.Id).ToList();
            foreach (Registers rgs in registers)
            {
                _context.Registers.Remove(rgs);
            }
            _context.Users.Remove(usr);
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetUsersById(String Id)
        {
            return _context.Users.FirstOrDefault(Temp => Temp.Id == Id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUsers(IdentityUser usr)
        {
            //nothing
        }

        public bool UserExist(IdentityUser usr)
        {
            if (usr == null)
            {
                return true;
            }
            return false;
        }
    }
}
