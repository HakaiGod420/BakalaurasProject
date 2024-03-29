﻿using ModelLayer.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class ActiveGameEntity
    {
        public ActiveGameEntity()
        {
            Users = new HashSet<UserEntity>();
            SentInvitations = new HashSet<SentInvitationEntity>();
        }

        [Key]
        public int ActiveGameId { get; set; }
        public int PlayersNeed { get; set; }
        public int RegisteredPlayerCount { get; set; }
        public ActiveGameState InvitationStateId { get; set; }
        public float Map_X_Coords { get; set; }
        public float Map_Y_Coords { get; set; }

        public virtual ICollection<UserEntity> Users { get; set; }

        public int CreatorId { get; set; }
        public virtual UserEntity Creator { get; set; }

        public int AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }
        public DateTime MeetDate { get; set; }
        public int BoardGameId { get; set; }
        public virtual BoardGameEntity BoardGame { get; set; }
        public virtual ICollection<SentInvitationEntity> SentInvitations { get; set; }
    }
}
