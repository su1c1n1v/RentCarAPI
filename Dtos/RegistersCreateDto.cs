using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class RegistersCreateDto
    {
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? CarId { get; set; }
    }
}
