using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class CarsCreateDto
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public String Brand { get; set; }

        [Required]
        public String Color { get; set; }
    }
}
