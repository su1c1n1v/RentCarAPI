using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class UsersCreateDto
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public String Email { get; set; }
    }
}
