using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AditionalFileEntity
    {
        [Key]
        public int AditionalFilesId { get; set; }

        [MaxLength(100)]
        public string FileName { get; set; }

        public string FileLocation { get; set; }

        public int BoardGameId { get; set; }

        public virtual BoardGameEntity BoardGame { get; set; }
    }
}
