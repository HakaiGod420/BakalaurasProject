using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.DTO;
using ModelLayer.Enum;
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

        public async Task<bool> UsernameExist(string username)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName.Equals(username));
        }

        public async Task<bool> UpdateInvitationNotification(int userId, bool state)
        {
            var user = await _dbContext.Users.Where(x=>x.UserId == userId).FirstOrDefaultAsync();

            if(user != null)
            {
                user.EnableInvitationNotifications = state;
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<UserSettings> GetUserSettings(int userId)
        {
            var result = await _dbContext.Users
                .Where(x => x.UserId == userId)
                .Select(x => new UserSettings
                {
                    Address = x.Address != null ? new UserAddressDto
                    {
                        City = x.Address.City,
                        Country = x.Address.Country,
                        Province = x.Address.Province,
                        StreetName = x.Address.StreetName,
                        Map_X_Coords = x.Address.Map_X_Coords,
                        Map_Y_Coords = x.Address.Map_Y_Coords,
                    } : null,
                    EnabledInvitationSettings = x.EnableInvitationNotifications
                })
                .SingleAsync();

            return result;
        }

        public async Task UpdateUserAddress(int addressId,int userId)
        {
            _dbContext.Users.Single(x => x.UserId == userId).AddressId = addressId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<int>> GetCloseUserIds(int creatorId, AddressEntity address)
        {
            var result = await _dbContext.Users.Where(x =>
            x.EnableInvitationNotifications == true
            && x.UserId != creatorId
            && x.Address != null
            && x.Address.Country == address.Country
            && x.Address.City == address.City
            && x.Address.StreetName == address.StreetName)
            .Select(x => x.UserId).ToListAsync();

            return result;
        }

        public async Task<UserInformationDTO?> GetUserInformation(string userName)
        {
            var result = await  _dbContext.Users.Where(x => x.UserName == userName).Select(j=> new UserInformationDTO
            {
                UserId = j.UserId,
                UserName = j.UserName,
                Role = j.Role.Name,
                InvititationsCreated = j.ActiveGamesCreators.Count,
                TableTopGamesCreated = _dbContext.BoardGames.Where(k => k.UserId == j.UserId).ToList().Count,
                State = j.UserState.Name,
                RegisteredOn = j.RegistrationTime,
                LastLogin = j.LastTimeConnection,

            }).FirstOrDefaultAsync();

            if (result == null)
            {
                return null;
            }

            return result;
        }

        public async Task UpdateLastTimeConnection(int id)
        {
            var result = await _dbContext.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();

            if (result == null)
            {
                return;
            }

            result.LastTimeConnection = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return;
        }
    }
}
