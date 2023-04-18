using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class ChangeUserInformationDTO
    {
        public int? UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool PasswordChanged { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set;}
    }
}
