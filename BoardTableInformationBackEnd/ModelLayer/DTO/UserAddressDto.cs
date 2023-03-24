using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UserAddressDto
    {

        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public double? Map_X_Coords { get; set; }
        public double? Map_Y_Coords { get; set; }

    }
}
