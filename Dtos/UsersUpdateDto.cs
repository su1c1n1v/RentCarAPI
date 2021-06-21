using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class UsersUpdateDto
    {
        [Required]
        public String UserName { get; set; }

        [Required]
        public String Email { get; set; }
    }
}
