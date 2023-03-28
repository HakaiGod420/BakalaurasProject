using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UserInformationDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public int InvititationsCreated { get; set; }
        public int TableTopGamesCreated { get; set; }
        public string State { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
