using Microsoft.EntityFrameworkCore;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt): base(opt)
        {

        }


        public DbSet<Cars> Cars { get; set; }
        //public DbSet<Registers> Registers { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
