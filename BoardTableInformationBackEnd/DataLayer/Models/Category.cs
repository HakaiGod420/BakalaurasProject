using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class CategoryEntity
    {
        public CategoryEntity()
        {
            Boards = new HashSet<BoardGameEntity>();
        }
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        public virtual ICollection<BoardGameEntity> Boards { get; set; }
    }
}
