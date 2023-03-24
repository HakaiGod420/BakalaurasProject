using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UpdateUserAddress
    {
        public UserAddressDto Address { get; set; }
        public bool EnabledInvitationSettings { get; set; }
    }
}
