using Microsoft.AspNetCore.Identity;
using RentCarAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Dtos
{
    public class RegistersReadDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String UserId { get; set; }

        public int? CarId { get; set; }

        [ForeignKey("CarId")]
        public virtual Cars Car { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
    }
}
