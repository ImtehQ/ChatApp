using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChatApp.Business.Core.Services
{
    public class GroupUserService : IGroupUserService
    {
        IGroupUserRepository _GroupUserRepository;

        public GroupUserService(IGroupUserRepository groupUserRepository)
        {
            _GroupUserRepository = groupUserRepository;
        }

        public IResponse GetGroupsByUser(User user)
        {
            IResponse response = this.CreateResponse();

            return response.SetAttachment(_GroupUserRepository.GetGroupUsers()
                .Where(u => u.Id == user.UserId)
                .Select(x => x.Group).ToList()).Successfull();
        }
        public IResponse AddGroupUser(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            _GroupUserRepository.Insert(user, group, accountRoleWithinGroup);
            _GroupUserRepository.Save();
            return this.CreateResponse().Successfull();
        }
        public IResponse GetAllUsersByGroupType(User user, GroupTypeEnum groupType)
        {
            IResponse response = this.CreateResponse();

            List<GroupUser> Users = new List<GroupUser>();

            var groupUsers = _GroupUserRepository.GetGroupUsers().ToList();

            if (groupType == GroupTypeEnum.OptionGeneral)
            {
                Users = groupUsers.Where(g => g.Group.type == groupType).ToList();
            }
            else if (groupType == GroupTypeEnum.OptionPrivate || groupType == GroupTypeEnum.OptionGroup)
            {
                Users = _GroupUserRepository.GetGroupUsers()
                    .Where(g => g.Group.type == groupType && g.User.UserId == user.UserId).ToList();
            }

            var something = Users.Select(u => new { userId = u.User.UserId, role = u.AccountRole });

            response.SetAttachment(Users);

            if (Users.Count > 0)
                return response.Successfull(HttpStatusCode.OK);
            else
                return response.Failed(HttpStatusCode.BadRequest);
        }

        public IResponse Join(Group group, User user, AccountRoleEnum accountRole)
        {

            _GroupUserRepository.Insert(user, group, accountRole);

            return this.CreateResponse().Successfull();
        }

        public IResponse RemoveGroup(Group group)
        {
            _GroupUserRepository.DeleteGroupUser(group);
            return this.CreateResponse().Successfull();
        }

        public IResponse RemoveUser(User user, Group group)
        {
            _GroupUserRepository.DeleteUserFromGroupUsers(user, group);
            return this.CreateResponse().Successfull();
        }

        public IResponse GetAccountRoleByUser(User user, Group group)
        {
            IResponse response = this.CreateResponse();
            var accountRole = _GroupUserRepository.GetGroupUser(user, group).AccountRole;
            response.SetAttachment(accountRole);
            return response.Successfull();
        }

        public IResponse Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            throw new System.NotImplementedException();
        }
    }
}
