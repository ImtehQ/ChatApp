using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapApp.Business.Domain.Extensions
{
    public static class UserExtensions
    {
        public static User New(this User user, int id)
        {
            user = NewUser(id, "FirstName", "LastName", "UserName", 
                "Email@Email.com", "", DateTime.Now, DateTime.Now, false);
            return user;
        }
        public static User New(int id)
        {
            return NewUser(id, "FirstName", "LastName", "UserName",
                "Email@Email.com", "", DateTime.Now, DateTime.Now, false);
        }
        private static User NewUser(int Id, string FirstName, string LastName, string UserName, 
            string Email, string PasswordHash, DateTime Created, DateTime LastUpdated, 
            bool isBlocked)
        {
            return new User
            {
                UserId = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Created = Created,
                isBlocked = isBlocked,
                LastUpdated = LastUpdated,
                PasswordHash = PasswordHash,
                UserName = UserName
            };
        }
    }
}
