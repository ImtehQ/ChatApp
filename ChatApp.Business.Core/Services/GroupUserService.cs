using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ChatApp.Business.Core.Services
{
    public class GroupUserService : IGroupUserService
    {
        IGenericRepository<GroupUser> _GroupUserRepository;

        public GroupUserService(IGenericRepository<GroupUser> groupUserRepository)
        {
            _GroupUserRepository = groupUserRepository;
        }

        public IResponse GetGroupsByUser(User user)
        {
            IResponse response = this.CreateResponse();

            return response.SetAttachment(_GroupUserRepository.GetAll()
                .Where(u => u.Id == user.UserId)
                .Select(x => x.Group).ToList()).Successfull();
        }

        public IResponse AddGroupUser(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            GroupUser groupUser = new GroupUser() { Group = group, User = user, AccountRole = accountRoleWithinGroup };
            _GroupUserRepository.Insert(groupUser);
            _GroupUserRepository.Save();
            return this.CreateResponse().Successfull();
        }
        public IResponse GetAllUsersByGroupType(User user, GroupTypeEnum groupType)
        {
            IResponse response = this.CreateResponse();

            List<GroupUser> Users = new List<GroupUser>();

            var groupUsers = _GroupUserRepository.GetAll().ToList();

            if (groupType == GroupTypeEnum.OptionGeneral)
            {
                Users = groupUsers.Where(g => g.Group.type == groupType).ToList();
            }
            else if (groupType == GroupTypeEnum.OptionPrivate || groupType == GroupTypeEnum.OptionGroup)
            {
                Users = _GroupUserRepository.GetAll()
                    .Where(g => g.Group.type == groupType && g.User.UserId == user.UserId).ToList();
            }

            var something = Users.Select(u => new { userId = u.User.UserId, role = u.AccountRole });

            response.SetAttachment(something);

            return response.Successfull();
        }

        public IResponse Join(Group group, User user, AccountRoleEnum accountRole)
        {

            GroupUser groupUser = new GroupUser() { Group = group, User = user, AccountRole = accountRole };
            _GroupUserRepository.Insert(groupUser);

            return this.CreateResponse().Successfull();
        }

        public IResponse RemoveGroup(Group group)
        {
            _GroupUserRepository.Delete(_GroupUserRepository.GetAll().Where(g => g.Group == group).FirstOrDefault());
            return this.CreateResponse().Successfull();
        }

        public IResponse RemoveUser(User user, Group group)
        {
            GroupUser gu = _GroupUserRepository.GetAll().Where(g => g.Group == group && g.User == user).FirstOrDefault();
            _GroupUserRepository.Delete(gu);

            return this.CreateResponse().Successfull();
        }

        public IResponse GetAccountRoleByUser(User user, Group group)
        {
            IResponse response = this.CreateResponse();
            GroupUser groupUser = _GroupUserRepository.GetAll().FirstOrDefault(g => g.Group.GroupId == group.GroupId && g.User.UserId == user.UserId);
            if (groupUser == null)
                return response.Failed("groupUser not found");

            response.SetAttachment(groupUser.AccountRole);
            return response.Successfull();
        }
    }
}
