using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    interface IUsersRepo
    {
        //Get all Users in the application
        IEnumerable<Users> GetAllUsers();

        //Get a user by it ID
        Users GetUserById(int Id);

        //Create a new user to the DB
        void CreateUser(Users pkm);

        //Save the thinks added in the DB
        bool SaveChanges();
    }
}
