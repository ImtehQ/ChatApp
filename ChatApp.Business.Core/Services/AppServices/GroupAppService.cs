using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.AppServices
{
    //Group
    public partial class AppService : IAppService
    {
        public IResponse ListGroupsFromUser(User user)
        {
            return this.CreateResponse().Include(_GroupUserService.GetGroupsByUser(user)).Successfull();
        }

        public IResponse ListGroups(int groupId, int userId)
        {
            IResponse response = this.CreateResponse();
            User user = response.Include(_UserService.GetUserById(userId)).GetAttachment<User>();
            _GroupUserService.GetGroupsByUser(user);
            return response.Successfull();
        }

        public IResponse RegisterGroup(User user, string name, string password, int maxUsers = 0,
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();

            Group group = response.Include(_GroupService.Register(name, password, maxUsers, Visibility, GroupType)).GetAttachment<Group>();

            if (response.GetValid() == false) return response.Failed();

            return response.Include(_GroupUserService.AddGroupUser(
                user, group, AccountRoleEnum.RoleAdmin)).Successfull();
        }


        public IResponse InviteToGroup(User user, int inviteId)
        {
            IResponse response = this.CreateResponse();

            Invite invite = response.Include(_InviteService.GetInviteById(inviteId)).GetAttachment<Invite>();
            if (response.GetValid() == false) return response.Failed();

            Group group = response.Include(_GroupService.GetGroupById(invite.GroupId)).GetAttachment<Group>();
            if (response.GetValid() == false) return response.Failed();

            response.Include(_GroupUserService.AddGroupUser(
                user, group, AccountRoleEnum.RoleUser));
            if (response.GetValid() == false) return response.Failed();

            return response.Successfull();
        }

        public IResponse JoinGroup(User sender, int groupId, int userId, string message)
        {
            IResponse response = this.CreateResponse();

            User user = response.Include(_UserService.GetUserById(userId)).GetAttachment<User>();

            Group group = response.Include(_GroupService.Register("Invite chat", "", 2,
                GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate)).GetAttachment<Group>();

            response.Include(_GroupUserService.Join(group, sender, AccountRoleEnum.RoleAdmin));

            response.Include(_GroupUserService.Join(group, user, AccountRoleEnum.RoleUser));


            Invite invite = new Invite() { GroupId = group.GroupId, Message = message };

            response.Include(_InviteService.Register(invite));

            response.Include(_MessageService.SendMessage(message, sender, GroupTypeEnum.OptionPrivate, group.GroupId));
            response.Include(_MessageService.SendMessage($"Invite: {invite.Id}", sender, GroupTypeEnum.OptionPrivate, group.GroupId));

            return response.Successfull();
        }

        public IResponse RemoveGroup(int groupId)
        {
            IResponse response = this.CreateResponse();

            Group group = response.Include(_GroupService.GetGroupById(groupId)).GetAttachment<Group>();
            if (response.GetValid() == false) return response.Failed();

            response.Include(_GroupUserService.RemoveGroup(group));
            if (response.GetValid() == false) return response.Failed();

            response.Include(_GroupService.RemoveGroup(groupId));
            if (response.GetValid() == false) return response.Failed();

            return response.Successfull();
        }

        public IResponse RemoveUserFromGroup(User user, int groupId)
        {
            IResponse response = this.CreateResponse();

            Group group = response.Include(_GroupService.GetGroupById(groupId)).GetAttachment<Group>();
            if (response.GetValid() == false) return response.Failed();

            AccountRoleEnum userAccountRole = response.Include(_GroupUserService.GetAccountRoleByUser(user, group))
                .GetAttachment<AccountRoleEnum>();
            if ((int)userAccountRole < (int)AccountRoleEnum.RoleModerator)
            {
                return response.Failed(null, System.Net.HttpStatusCode.Unauthorized);
            }

            response.Include(_GroupUserService.RemoveUser(user, group));

            return response.Successfull();
        }
    }
}
