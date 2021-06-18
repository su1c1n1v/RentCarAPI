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
        IEnumerable<Users> GetAllUsers();

        //Get a user by it ID
        Users GetUsersById(int Id);

        //Create a new user to the DB
        void CreateUsers(Users usr);

        //Save the thinks added in the DB
        bool SaveChanges();

        void UpdateUsers(Users usr);

        void DeleteUsers(Users usr);
        bool UserExist(Users usr);
    }
}
