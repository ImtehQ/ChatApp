using ChatApp.Domain.Models;
using System.Collections.Generic;
using ChatApp.Domain.Interfaces.Repositorys;
using System.Linq;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
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

        public List<Group> GetGroupsByUser(User user)
        {
            return _GroupUserRepository.GetGroupUsers()
                .Where(u => u.Id == user.UserId)
                .Select(x => x.Group).ToList();
        }
        public void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            _GroupUserRepository.Insert(user, group, accountRoleWithinGroup);
            _GroupUserRepository.Save();
        }
        public IResponse GetAllUsersByGroupType(User user, GroupTypeEnum groupType)
        {
            IResponse response = new Response(ResponseMethodCode.List, ResponseLayerCode.Service, groupType);

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

            if (Users.Count > 0)
                return response.Successfull(HttpStatusCode.OK, something);
            else
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
        }

        public IResponse Join(Group group, User user, AccountRoleEnum accountRole)
        {
            IResponse response = new Response(ResponseMethodCode.Join, ResponseLayerCode.Service, 
                new object[] { group, user, accountRole });

            _GroupUserRepository.Insert(user, group, accountRole);

            return response.Successfull();
        }

        public void RemoveGroup(Group group)
        {
            _GroupUserRepository.DeleteGroupUser(group);
        }

        public void RemoveUser(User user, Group group)
        {
            _GroupUserRepository.DeleteUserFromGroupUsers(user, group);
        }
    }
}
