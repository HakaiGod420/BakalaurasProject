using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class ChangeUserInformationDTO
    {
        public int? UserId { get; set; }
        public string Email { get; set; }
        public bool PasswordChanged { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set;}
    }
}
