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
                "Email@Email.com", "", DateTime.Now, DateTime.Now, 
                0, false, false);
            return user;
        }
        private static User NewUser(int Id, string FirstName, string LastName, string UserName, 
            string Email, string PasswordHash, DateTime Created, DateTime LastUpdated, 
            int RoleId, bool isBlocked, bool RequiresVerification)
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                RoleId = RoleId,
                Created = Created,
                isBlocked = isBlocked,
                LastUpdated = LastUpdated,
                PasswordHash = PasswordHash,
                RequiresVerification = RequiresVerification,
                UserName = UserName
            };
        }
    }
}
