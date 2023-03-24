using DataLayer.Models;
using DataLayer.Repositories.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<UserViewModel?> GetUser(int id)
        {
            var user = await _repository.GetUser(id);

            if (user != null)
            {
                return new UserViewModel(user.UserId, 
                    user.UserName, 
                    user.Email,
                    user.RegistrationTime,
                    user.LastTimeConnection,
                    user.ProfileImage,
                    user.RoleId.ToString(),
                    user.UserStateId.ToString());
            }

            return null;
        }

        public async Task<UserViewModel?> RegisterUser(UserRegisterModel user)
        {
            var userNameExist = await _repository.UsernameExist(user.UserName);

            if (userNameExist)
            {
                return null;
            }

            var enityUser = new UserEntity();

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            enityUser.UserName = user.UserName;
            enityUser.Email = user.Email;
            enityUser.Password = passwordHash;
            enityUser.PasswordSalt = passwordSalt;
            enityUser.UserStateId = ModelLayer.Enum.UserStates.Active;
            enityUser.RoleId = ModelLayer.Enum.Roles.User;
            enityUser.RegistrationTime = DateTime.Now;
            enityUser.LastTimeConnection = DateTime.Now;
            enityUser.ProfileImage = null;

            var returnedObject = await _repository.RegisterUser(enityUser);

            return new UserViewModel(
                    returnedObject.UserId,
                    returnedObject.UserName,
                    returnedObject.Email,
                    returnedObject.RegistrationTime,
                    returnedObject.LastTimeConnection,
                    returnedObject.ProfileImage,
                    returnedObject.RoleId.ToString(),
                    returnedObject.UserStateId.ToString());

        }

        public async Task<string?> Login(LoginUserModel loginUser)
        {
            var user = await _repository.GetPasswords(loginUser.UserName);

            if (user != null)
            {
                var validation = VerifyPasswodHash(loginUser.Password, user.Password, user.PasswordSalt);
                if (validation)
                {
                    return CreateToken(user);
                }

                return null;
            }
            return null;
        }

        private void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] passwordSalt )
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswodHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
               new Claim("UserId", user.UserId.ToString()),
               new Claim("Username", user.UserName),
               new Claim("Email", user.Email),
               new Claim("Role", user.RoleId.ToString()),
               new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                audience: _configuration.GetSection("Jwt:Audience").Value,
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<bool> UpdateNotifications(int id,NotificationsListDto notificationsListDto)
        {
           foreach(var notification in notificationsListDto.Notifications)
            {
               if(notification.Title == "Invitation")
                {
                    return await _repository.UpdateInvitationNotification(id,    notification.IsActive);
                }
            }

            return false;
        }

        public async Task<UserSettings> GetUserSettings(int id)
        {
            return await _repository.GetUserSettings(id);
        }


    }
}
