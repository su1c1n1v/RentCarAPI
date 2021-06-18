using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class CarsUpdateDto
    {
        public String Name { get; set; }

        public String Brand { get; set; }

        public String Color { get; set; }
    }
}
