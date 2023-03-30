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
        public Task<bool> UpdateInvitationNotification(int userId,bool state);
        public Task<UserSettings> GetUserSettings(int userId);
        public Task UpdateUserAddress(int addressId, int userId);
        public Task<List<int>> GetCloseUserIds(int creatorId, AddressEntity address);
        public Task<UserInformationDTO?> GetUserInformation(string userName);
        public Task UpdateLastTimeConnection(int id);
        public Task<int?> GetUserIdByUsername(string username);
    }
}
