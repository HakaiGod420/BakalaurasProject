using DataLayer.Models;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.User
{
    public interface IUserRepository
    {
        public Task<UserEntity?> GetUser(int id);
        public Task<UserEntity?> RegisterUser(UserEntity user);
        public Task<UserEntity?> GetPasswords(string username);
        public Task<bool> UsernameExist(string username);
    }
}
