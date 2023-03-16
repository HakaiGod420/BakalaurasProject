using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class PostInvatationDto
    {
        public int ActiveGameId { get; set; }
        public int PlayersNeed { get; set; }
        public float Map_X_Cords { get; set; }
        public float Map_Y_Cords { get; set; }
        public AddressCreateDto Address { get; set; }
    }
}
