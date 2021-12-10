using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.AppServices
{
    //Group
    public partial class AppService : IAppService
    {
        public IResponse ListGroupsFromUser(User user)
        {
            return this.CreateResponse().Includes(_GroupUserService.GetGroupsByUser(user)).Successfull();
        }

        public IResponse ListGroups(int groupId, int userId)
        {
            IResponse response = this.CreateResponse();
            User user = response.Includes(_UserService.GetUserById(userId)).LastIncluded().Contents<User>();
            _GroupUserService.GetGroupsByUser(user);
            return response.Successfull();
        }

        public IResponse RegisterGroup(User user, string name, string password, int maxUsers = 0,
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();

            if (response.LastIncluded().Status() == false) return response.Failed();

            Group group = response.Includes(_GroupService.Create(name, password, maxUsers, Visibility, GroupType)).LastIncluded().Contents<Group>();

            if (response.LastIncluded().Status() == false) return response.Failed();

            return response.Includes(_GroupUserService.Insert(
                user, group, AccountRoleEnum.RoleAdmin)).Successfull();
        }


        public IResponse InviteToGroup(User user, int inviteId)
        {
            IResponse response = this.CreateResponse();

            Invite invite = response.Includes(_InviteService.GetInviteById(inviteId)).LastIncluded().Contents<Invite>();
            if (response.LastIncluded().Status() == false) return response.Failed();

            Group group = response.Includes(_GroupService.GetGroupById(invite.GroupId)).LastIncluded().Contents<Group>();
            if (response.LastIncluded().Status() == false) return response.Failed();

            response.Includes(_GroupUserService.Insert(
                user, group, AccountRoleEnum.RoleUser));
            if (response.LastIncluded().Status() == false) return response.Failed();

            return response.Successfull();
        }

        public IResponse JoinGroup(User sender, int groupId, int userId, string message)
        {
            IResponse response = this.CreateResponse();

            User user = response.Includes(_UserService.GetUserById(userId)).LastIncluded().Contents<User>();

            Group group = response.Includes(_GroupService.Create("Invite chat", "", 2,
                GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate)).LastIncluded().Contents<Group>();

            response.Includes(_GroupUserService.Join(group, sender, AccountRoleEnum.RoleAdmin));

            response.Includes(_GroupUserService.Join(group, user, AccountRoleEnum.RoleUser));


            Invite invite = new Invite() { GroupId = group.GroupId, Message = message };

            response.Includes(_InviteService.Register(invite));

            response.Includes(_MessageService.SendMessage(message, sender, GroupTypeEnum.OptionPrivate, group.GroupId));
            response.Includes(_MessageService.SendMessage($"Invite: {invite.Id}", sender, GroupTypeEnum.OptionPrivate, group.GroupId));

            return response.Successfull();
        }

        public IResponse RemoveGroup(int groupId)
        {
            IResponse response = this.CreateResponse();

            Group group = response.Includes(_GroupService.GetGroupById(groupId)).LastIncluded().Contents<Group>();
            if (response.LastIncluded().Status() == false) return response.Failed();

            response.Includes(_GroupUserService.RemoveGroup(group));
            if (response.LastIncluded().Status() == false) return response.Failed();

            response.Includes(_GroupService.RemoveGroup(groupId));
            if (response.LastIncluded().Status() == false) return response.Failed();

            return response.Successfull();
        }

        public IResponse RemoveUserFromGroup(User user, int groupId)
        {
            IResponse response = this.CreateResponse();

            Group group = response.Includes(_GroupService.GetGroupById(groupId)).LastIncluded().Contents<Group>();
            if (response.LastIncluded().Status() == false) return response.Failed();

            AccountRoleEnum userAccountRole = response.Includes(_GroupUserService.GetAccountRoleByUser(user, group))
                .LastIncluded().Contents<AccountRoleEnum>();
            if ((int)userAccountRole < (int)AccountRoleEnum.RoleModerator)
            {
                return response.Failed(null, System.Net.HttpStatusCode.Unauthorized);
            }

            response.Includes(_GroupUserService.RemoveUser(user, group));

            return response.Successfull();
        }
    }
}
