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
        private readonly RoleManager<IdentityRole> _roleManager;

        public SqlUsersRepo(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager, ApplicationContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public SecurityToken GenerateToken(AppSettings appSettings, List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret));

            return new JwtSecurityToken(
                issuer: appSettings.ValidIn,
                audience: appSettings.ValidIn,
                expires: DateTime.UtcNow.AddHours(appSettings.Expiration),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }

        public void CreateUsers(IdentityUser user)
        {

        }

        public async Task<SecurityToken> Login(IdentityUser usr, AppSettings appSettings)
        {

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (usr == null)
            {
                throw new ArgumentNullException(nameof(usr));
            }

            var userRoles = await _userManager.GetRolesAsync(usr);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,usr.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            foreach (var item in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            return GenerateToken(appSettings, authClaims);
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
