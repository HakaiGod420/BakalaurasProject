using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class SentInvitationEntity
    {
        [Key]
        public int SentInvitationId { get; set; }
        public int SelectedActiveGameId { get; set; }
        public virtual ActiveGameEntity SelectedActiveGame { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public int InvitationStateId { get; set; }
        public bool IsBlocked { get; set; } = false;
        public virtual InvitationStateEntity InvitationState { get; set; }
    }
}
