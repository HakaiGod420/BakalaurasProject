using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ActiveGameStateEntity
    {
        [Key]
        public int StateId { get; set; }
        public string Name { get; set; }
    }
}
