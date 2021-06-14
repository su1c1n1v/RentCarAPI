using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    interface IRegistersRepo
    {
        //Get all Registers in the application
        IEnumerable<Registers> GetAllRegisters();

        //Get a register by it ID
        Cars GetRegisterById(int Id);

        //Create a new register to the DB
        void CreateRegister(Registers rgt);

        //Save the thinks added in the DB
        bool SaveChanges();
    }
}
