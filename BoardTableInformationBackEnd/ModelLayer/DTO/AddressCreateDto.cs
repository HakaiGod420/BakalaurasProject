using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class AddressCreateDto
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string Province { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public int HouseNumber { get; set; }
    }
}
