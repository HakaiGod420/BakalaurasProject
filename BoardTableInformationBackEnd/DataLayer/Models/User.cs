using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserEntity
    {
        public UserEntity()
        {
            ActiveGamesParticipants = new HashSet<ActiveGameEntity>();
            ActiveGamesCreators = new HashSet<ActiveGameEntity>();
            ReceivedInvitations = new HashSet<SentInvitationEntity>();
            Reviews = new HashSet<ReviewEntity>();
        }

        [Key]
        public int UserId { get; set; }

        public Roles RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }

        public UserStates UserStateId { get; set; }
        public virtual UserStateEntity UserState { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public DateTime RegistrationTime { get; set; }

        [Required]
        public DateTime LastTimeConnection { get; set; }


        public string? ProfileImage { get; set; }

        [Required]
        public string Email { get; set; }

        public bool EnableInvitationNotifications { get; set; } = false;

        public int? AddressId { get; set; } = null;
        public virtual UserAddressEntity Address { get; set; } = null;
        public virtual ICollection<ActiveGameEntity> ActiveGamesParticipants { get; set; }
        public virtual ICollection<ActiveGameEntity> ActiveGamesCreators { get; set; }
        public virtual ICollection<SentInvitationEntity> ReceivedInvitations { get; set;}
        public virtual ICollection<ReviewEntity> Reviews { get; set; }
    }
}
