using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace ChatApp.Domain.Interfaces.Repositorys
{
    public interface IGroupUserRepository
    {
        IEnumerable<GroupUser> GetGroupUsers();
        void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup);
        void Save();
        void DeleteGroupUser(Group group);
        void DeleteUserFromGroupUsers(User user, Group group);
        GroupUser GetGroupUser(User user, Group group);
    }
}
