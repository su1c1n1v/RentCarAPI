﻿using System;
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

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        public String ConfirmPassword { get; set; }

    }
}
