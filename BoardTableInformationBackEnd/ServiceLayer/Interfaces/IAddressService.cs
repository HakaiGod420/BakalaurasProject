using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAddressService
    {
        public Task<bool> AddNewAddress(AddressCreateDto addressCreateDto);
        public Task<bool> UpdateUserAddress(int id, UpdateUserAddress addressDto);
    }
}
