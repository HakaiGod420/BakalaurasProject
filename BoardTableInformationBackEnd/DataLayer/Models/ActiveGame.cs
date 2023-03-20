using ModelLayer.Enum;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class ActiveGameEntity
    {
        public ActiveGameEntity()
        {
            Users = new HashSet<UserEntity>();
        }

        [Key]
        public int ActiveGameId { get; set; }
        public int PlayersNeed { get; set; }
        public int RegistredPlayerCount { get; set; }
        public ActiveGameState InvitationStateId { get; set; }
        public float Map_X_Cords { get; set; }
        public float Map_Y_Cords { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }

        public int CreatorId { get; set; }
        public virtual UserEntity Creator { get; set; }

        public int AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }

        public int BoardGameId { get; set; }
        public virtual BoardGameEntity BoardGame { get; set; }
    }
}
