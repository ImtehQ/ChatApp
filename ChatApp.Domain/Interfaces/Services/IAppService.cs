using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IAppService
    {
        //--------------
        IResponse AccountUpdateUser(int id, string Username, string Emailaddress, string Password);
        IResponse BlockUser(int userId);
        IResponse ListUsers(User user, int groupType);
        IResponse LoginUser(string Username, string Password);
        IResponse RegisterUser(string Name, string Username, string Emailaddress, string Password);
        //--------------
        IResponse InviteToGroup(User user, int InviteId);
        IResponse JoinGroup(User sender, int GroupId, int UserId, string Message);
        IResponse ListGroups(int GroupId, User user);
        IResponse RegisterGroup(User user, string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup);
        IResponse RemoveGroup(int GroupId);
        IResponse RemoveUserFromGroup(User user, int GroupId);
        //--------------
        IResponse PullMessages(int pageNr, int groupId);
        IResponse SendMessage(string Message, User Sender, int Type, int TypeId);
        //--------------
    }
}
