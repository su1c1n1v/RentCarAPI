using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class RegistersUpdateDto
    {
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int? UserId { get; set; }

        [Required]
        public int? CarId { get; set; }
    }
}
