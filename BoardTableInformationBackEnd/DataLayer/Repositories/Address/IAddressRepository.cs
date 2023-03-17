using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Address
{
    public interface IAddressRepository
    {
        public Task<AddressEntity> AddAddress(AddressEntity address);
        public Task<AddressEntity?> CheckIfExistAddress(string fullAddress);
    }
}
