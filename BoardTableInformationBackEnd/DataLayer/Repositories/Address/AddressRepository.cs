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
            if (address == null)
            {
                throw new ArgumentNullException(nameof(AddressEntity));
            }

            var result = _dbContext.Addresses.Add(address);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserAddressEntity> AddAddressToUser(UserAddressEntity address)
        {
            if (address == null)
            {
                throw new ArgumentNullException(nameof(UserAddressEntity));
            }

            _dbContext.UserAddress.Add(address);
            await _dbContext.SaveChangesAsync();
            return address;
        }

        public async Task<AddressEntity?> CheckIfExistAddress(string fullAddress)
        {
            return await _dbContext.Addresses.Where(x => x.FullAddress == fullAddress).FirstOrDefaultAsync();
        }

        public async Task<int?> CheckIfUserHasAddress(int userId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null && user.AddressId.HasValue)
            {
                return user.AddressId.Value;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserAddressEntity> UpdateUserAddress(UserAddressEntity address)
        {
            if(address == null)
            {
                throw new ArgumentNullException(nameof(Address));
            }
            _dbContext.UserAddress.Update(address);
            await _dbContext.SaveChangesAsync();
            return address;
        }
    }
}
