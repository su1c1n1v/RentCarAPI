using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class CarsReadDto
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Brand { get; set; }

        public bool Available { get; set; }

        public String Color { get; set; }

        public DateTime Date { get; set; }
    }
}
