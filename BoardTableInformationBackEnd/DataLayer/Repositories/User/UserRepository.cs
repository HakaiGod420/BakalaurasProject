using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _dbContext;

        public UserRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> GetUser(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<UserEntity?> RegisterUser(UserEntity user)
        {
            await _dbContext.Users.AddAsync(user);
            _dbContext.SaveChanges();
            return user;
        }

        public async Task<UserEntity?> GetPasswords(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName.Equals(username));

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
