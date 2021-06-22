using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentCarAPI.Models
{
    public class AppSettings
    {
        public String Secret { get; set; }
        public int Expiration { get; set; }
        public String Creater { get; set; }
        public String ValidIn { get; set; }
    }
}
