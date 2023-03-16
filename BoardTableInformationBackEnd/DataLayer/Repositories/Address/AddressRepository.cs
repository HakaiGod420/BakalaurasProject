using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Address
{
    public class AddressRepository : IAddressRepository
    {
        public readonly DataBaseContext _dbContext;
        public AddressRepository(DataBaseContext dataBaseContext) {

            _dbContext = dataBaseContext;
        }

        public async Task<AddressEntity> AddAddress(AddressEntity address)
        {
            var result = _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public async Task<AddressEntity?> CheckIfExistAddress(AddressEntity address)
        {
            return await _dbContext.Addresses.Where(x => x.FullAddress == address.FullAddress).FirstOrDefaultAsync();
        }
    }
}
