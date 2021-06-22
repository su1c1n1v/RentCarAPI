using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public async Task<string> CreateJWT(IdentityUser usr, AppSettings appSettings)
        {
            var user = await _userManager.FindByIdAsync(usr.Id);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = appSettings.Creater,
                Audience = appSettings.ValidIn,
                Expires = DateTime.UtcNow.AddHours(appSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        public void CreateUsers(IdentityUser user)
        {
           /*
             if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);*/
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

        public async Task<IdentityUser> GetUsersById(String Id)
        {
            return await _userManager.FindByIdAsync(Id);
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
        //Test
        public bool IsAuthenticate(IdentityUser usr)
        {
            if (usr == null)
            {
                return true;
            }
            return false;
        }
    }
}
