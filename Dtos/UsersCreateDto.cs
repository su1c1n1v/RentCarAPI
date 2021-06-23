using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class UsersCreateDto
    {
        [Required(ErrorMessage = "User Name is required")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public String Email { get; set; }

    }
}
