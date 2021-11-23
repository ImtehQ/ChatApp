using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IGroupUserService
    {
        IResponse GetAllUsersByGroupType(User user, GroupTypeEnum groupType);
        List<Group> GetGroupsByUser(User user);
        void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup);
        IResponse Join(Group group, User user, AccountRoleEnum accountRole);
        void RemoveGroup(Group group);
        void RemoveUser(User user, Group group);
    }
}
