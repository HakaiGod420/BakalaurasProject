using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ImageEntity
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string Location { get; set; }

        [MaxLength(100)]
        public string? Alias { get; set; }
        public virtual BoardGameEntity BoardGame { get; set; }
        public int? BoardGameId { get; set; }

    }
}
