using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentCarAPI.Dtos;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public SqlUsersRepo(UserManager<IdentityUser> userManager, ApplicationContext context,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
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

        public IEnumerable<UsersReadDto> GetAllUsers()
        {
            var users = _context.Users.ToList();
            var list = new List<UsersReadDto>();
            foreach (var item in users)
            {
                var userMap = _mapper.Map<UsersReadDto>(item);
                if((_userManager.IsInRoleAsync(item, UserRoles.Admin).Result))
                {
                    userMap.Role = UserRoles.Admin;
                }
                else
                {
                    userMap.Role = UserRoles.User;
                }
                list.Add(userMap);
            }
            return list;
        }

        public async Task<UsersReadDto> GetUsersById(String Id)
        {
            var usr = await _userManager.FindByIdAsync(Id);
            var userMap = _mapper.Map<UsersReadDto>(usr);
            if ((_userManager.IsInRoleAsync(usr, UserRoles.Admin).Result))
            {
                userMap.Role = UserRoles.Admin;
            }
            else
            {
                userMap.Role = UserRoles.User;
            }
            return userMap;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateUsers(IdentityUser usr)
        {
            //nothing
        }

    }
}
