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
        IResponse GetGroupsByUser(User user);
        IResponse Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup);
        IResponse Join(Group group, User user, AccountRoleEnum accountRole);
        IResponse RemoveGroup(Group group);
        IResponse RemoveUser(User user, Group group);
        IResponse GetAccountRoleByUser(User user, Group group);
    }
}
