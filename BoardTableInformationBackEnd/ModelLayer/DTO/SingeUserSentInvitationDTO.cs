using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class SingeUserSentInvitationDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int ActiveInvitationId { get; set; }
    }
}
