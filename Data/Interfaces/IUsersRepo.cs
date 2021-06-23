using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RentCarAPI.Dtos;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public interface IUsersRepo
    {
        //Get all Users in the application
        IEnumerable<UsersReadDto> GetAllUsers();

        //Get a user by it ID
        Task<IdentityUser> GetUsersById(string Id);

        //Create a new user to the DB
        void CreateUsers(IdentityUser usr);

        //Save the thinks added in the DB
        bool SaveChanges();

        void UpdateUsers(IdentityUser usr);

        void DeleteUsers(IdentityUser usr);

        SecurityToken GenerateToken(AppSettings appSettings, List<Claim> authClaims);

        Task<SecurityToken> Login(IdentityUser usr, AppSettings appSettings);
    }
}
