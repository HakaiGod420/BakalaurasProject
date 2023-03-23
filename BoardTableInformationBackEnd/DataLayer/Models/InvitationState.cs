using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class InvitationStateEntity
    {
        public InvitationStateEntity()
        {
            Invitations = new HashSet<SentInvitationEntity>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SentInvitationEntity> Invitations { get; set; }
    }
}
