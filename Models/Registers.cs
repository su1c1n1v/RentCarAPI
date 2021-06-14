using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Models
{
    public class Registers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; }

        public int? UserId { get; set; }

        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Cars Car { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
