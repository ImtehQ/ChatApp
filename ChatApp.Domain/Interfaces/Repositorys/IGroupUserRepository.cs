using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces.Repositorys
{
    public interface IGroupUserRepository
    {
        List<GroupUser> GetGroupUsersByUserId(int userId);
        void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup);
        void Save();
    }
}
