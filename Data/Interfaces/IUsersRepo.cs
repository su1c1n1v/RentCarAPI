﻿using Microsoft.AspNetCore.Identity;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public interface IUsersRepo
    {
        //Get all Users in the application
        IEnumerable<IdentityUser> GetAllUsers();

        //Get a user by it ID
        IdentityUser GetUsersById(string Id);

        //Create a new user to the DB
        void CreateUsers(IdentityUser usr);

        //Save the thinks added in the DB
        bool SaveChanges();

        void UpdateUsers(IdentityUser usr);

        void DeleteUsers(IdentityUser usr);
        bool UserExist(IdentityUser usr);
    }
}
