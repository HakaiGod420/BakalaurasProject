using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class PostInvatationDto
    {
        [Required]
        public int ActiveGameId { get; set; }
        [Required]
        public int PlayersNeed { get; set; }
        [Required]
        public float Map_X_Cords { get; set; }
        [Required]
        public float Map_Y_Cords { get; set; }
        [Required]
        public int MinimalAge { get; set; }
        [Required]
        public int PlayersNeeded { get; set; }
        [Required]
        public string InvitationDate { get; set; }
        [Required]
        public AddressCreateDto Address { get; set; }
    }
}
