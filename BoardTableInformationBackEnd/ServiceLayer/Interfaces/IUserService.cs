﻿using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUserService
    {
        public Task<UserViewModel?> GetUser(int id);
        public Task<UserViewModel?> RegisterUser(UserRegisterModel user);
        public Task<string?> Login(LoginUserModel loginUser);
        public Task<bool> UpdateNotifications(int id, NotificationsListDto notificationsListDto);
        public Task<UserSettings> GetUserSettings(int id);
        public Task<UserInformationDTO?> GetUserInformation(string userName);
        public Task ChangeUserInformation(ChangeUserInformationDTO userInformationDTO);

    }
}
