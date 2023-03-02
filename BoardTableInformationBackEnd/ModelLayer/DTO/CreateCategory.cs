using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CreateCategory
    {
        [MaxLength(100)]
        [Required]
        public string CategoryName { get; set; }
    }
}
