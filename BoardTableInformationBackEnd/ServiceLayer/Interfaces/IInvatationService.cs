using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IInvatationService
    {
        public PostInvatationDto PostInvatation(PostInvatationDto data, int id);
    }
}
