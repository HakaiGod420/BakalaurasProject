using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class AddressDto
    {
        public string City { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress { get; set; }
    }
}
