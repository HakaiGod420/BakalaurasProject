using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class BoardTypeEntity
    {
        public BoardTypeEntity()
        {
            Boards = new HashSet<BoardGameEntity>();
        }

        [Key]
        public int BoardTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BoardTypeName { get; set; }

        public virtual ICollection<BoardGameEntity> Boards { get; set; }
    }
}
