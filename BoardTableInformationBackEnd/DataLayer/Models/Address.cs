using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AddressEntity
    {
        public AddressEntity()
        {
            ActiveGameInThisAddress = new HashSet<ActiveGameEntity>();
        }

        [Key]
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string FullAddress { get; set; }
        public virtual ICollection<ActiveGameEntity> ActiveGameInThisAddress { get; set; }

    }
}

