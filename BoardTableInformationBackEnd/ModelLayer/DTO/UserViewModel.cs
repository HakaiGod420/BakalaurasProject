using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UserViewModel
    { 
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationTime { get; set; }
        public DateTime LastTimeConnection { get; set; }
        public string? ProfileImmage { get; set; }
        public string Role { get; set; }
        public string State { get; set; }

        public UserViewModel() { }

        public UserViewModel(int id,
            string username, 
            string email,
            DateTime registrationTime,
            DateTime lastTimeConnection,
            string? profileImmage,
            string role,
            string state)
        {
            Id = id;
            Username = username;
            Email = email;
            RegistrationTime = registrationTime;
            LastTimeConnection = lastTimeConnection;
            ProfileImmage = profileImmage;
            Role = role;
            State = state;
        }
    }
}
