using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IGroupUserService
    {
        IResponse AddGroupUser(User user, Group group, AccountRoleEnum accountRoleWithinGroup);
        IResponse GetAccountRoleByUser(User user, Group group);
        IResponse GetAllUsersByGroupType(User user, GroupTypeEnum groupType);
        IResponse GetGroupsByUser(User user);
        IResponse Join(Group group, User user, AccountRoleEnum accountRole);
        IResponse RemoveGroup(Group group);
        IResponse RemoveUser(User user, Group group);
    }
}
