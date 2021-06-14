using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Models
{
    public class Cars
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Brand { get; set; }

        [Required]
        public bool Available { get; set; }

        [Required]
        public String Color { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
