using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class FilterDTO
    {
        public List<int>? Categories { get; set; }
        public List<int>? Types { get; set; }
        public DateTime? CreationDate { get; set; }
        [AllowNull]
        public string? Rating { get; set; } = null;
        [AllowNull]
        public string? Title { get; set; } = null;

    }
}
