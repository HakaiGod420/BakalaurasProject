using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TableBoardStateEntity
    {
        public TableBoardStateEntity()
        {
            BoardGames = new HashSet<BoardGameEntity>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TableBoardState TableBoardStateId { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<BoardGameEntity> BoardGames { get; set; }
    }
}
