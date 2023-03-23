using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class NotificationSettingsDto
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
