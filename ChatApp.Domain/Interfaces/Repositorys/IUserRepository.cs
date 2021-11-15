using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
