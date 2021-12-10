using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace ChatApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        void DeleteUser(int UserID);
        User GetUserByID(int id);
        IEnumerable<User> GetUsers();
        void InsertUser(User User);
        void Save();
        void UpdateUser(User User);
    }
}
