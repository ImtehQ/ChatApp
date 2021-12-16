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

            if (groupId <= 0)
                return response.Failed("GroupId cant be 0 or less");

            if (userId <= 0)
                return response.Failed("userId cant be 0 or less");

            User user = response.Include(_UserService.GetUserById(userId)).GetAttachment<User>();
            _GroupUserService.GetGroupsByUser(user);
            return response.Successfull();
        }

        public IResponse RegisterGroup(User user, string name, string password, int maxUsers = 0,
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();

            if (user == null)
                return response.Failed("Anomimomus not allowed");

            Group group = response.Include(_GroupService.Register(name, password, maxUsers, Visibility, GroupType)).GetAttachment<Group>();

            if (response.GetValid() == false) return response.Failed();

            return response.Include(_GroupUserService.AddGroupUser(
                user, group, AccountRoleEnum.RoleAdmin)).Successfull();
        }


        public IResponse InviteToGroup(User user, int inviteId)
        {
            IResponse response = this.CreateResponse();

            if (user == null)
                return response.Failed("Anomimomus not allowed");

            Invite invite = response.Include(_InviteService.GetInviteById(inviteId)).GetAttachment<Invite>();
            if (response.GetValid() == false) return response.Failed();

            Group group = response.Include(_GroupService.GetGroupById(invite.GroupId)).GetAttachment<Group>();
            if (response.GetValid() == false) return response.Failed();

            response.Include(_GroupUserService.AddGroupUser(
                user, group, AccountRoleEnum.RoleUser));
            if (response.GetValid() == false) return response.Failed();

            return response.Successfull();
        }

        public IResponse JoinGroupSelf(User sender, int groupId, string message)
        {
            IResponse response = this.CreateResponse();

            if (sender == null)
                return response.Failed("Anomimomus not allowed");

            Group group = response.Include(_GroupService.GetGroupById(groupId)).GetAttachment<Group>();
            if(group == null)
                return response.Failed("Group not found");

            response.Include(_GroupUserService.Join(group, sender, AccountRoleEnum.RoleAdmin));

            response.Include(_MessageService.SendMessage(message, sender, GroupTypeEnum.OptionPrivate, group.GroupId));

            return response.Successfull();
        }

        public IResponse JoinGroup(User sender, int groupId, int userId, string message)
        {
            IResponse response = this.CreateResponse();
            if (sender == null)
                return response.Failed("Anomimomus not allowed");

            User user = response.Include(_UserService.GetUserById(userId)).GetAttachment<User>();
            if (user == null)
                return response.Failed("User not found");

            Group group = response.Include(_GroupService.Register("Invite chat", "", 2,
                GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate)).GetAttachment<Group>();
            if (group == null)
                return response.Failed("Group not found");

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

        public IResponse RemoveOtherUserFromGroup(User sender, int userId, int groupId)
        {
            IResponse response = this.CreateResponse();
            if (sender == null)
                return response.Failed("Anomimomus not allowed");

            User user = response.Include(_UserService.GetUserById(userId)).GetAttachment<User>();
            if (user == null)
                return response.Failed("User not found");

            Group group = response.Include(_GroupService.GetGroupById(groupId)).GetAttachment<Group>();
            if(group == null)
                return response.Failed("Group not found");

            AccountRoleEnum senderAccountRole = response.Include(_GroupUserService.GetAccountRoleByUser(sender, group))
                .GetAttachment<AccountRoleEnum>();

            AccountRoleEnum userAccountRole = response.Include(_GroupUserService.GetAccountRoleByUser(user, group))
                .GetAttachment<AccountRoleEnum>();


            if ((int)senderAccountRole < (int)AccountRoleEnum.RoleModerator)
            {
                return response.Failed("Missing account role required", System.Net.HttpStatusCode.Unauthorized);
            }
            if ((int)senderAccountRole <= (int)userAccountRole)
            {
                return response.Failed("sender account role less then or same as other user", System.Net.HttpStatusCode.Unauthorized);
            }

            response.Include(_GroupUserService.RemoveUser(user, group));

            return response.Successfull();
        }
        public IResponse RemoveSelfFromGroup(User sender, int groupId)
        {
            IResponse response = this.CreateResponse();
            if (sender == null)
                return response.Failed("Anomimomus not allowed");

            Group group = response.Include(_GroupService.GetGroupById(groupId)).GetAttachment<Group>();
            if (group == null)
                return response.Failed("Group not found");

            response.Include(_GroupUserService.RemoveUser(sender, group));

            return response.Successfull();
        }
    }
}
