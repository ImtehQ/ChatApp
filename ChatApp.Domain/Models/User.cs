using ChatApp.Domain.Enums;
using System;
namespace ChatApp.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public AccountRoleEnum Role { get; set; }
        public bool isBlocked { get; set; }
    }
}
