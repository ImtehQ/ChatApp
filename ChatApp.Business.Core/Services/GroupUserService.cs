using ChatApp.Domain.Models;
using System.Collections.Generic;
using ChatApp.Domain.Interfaces.Repositorys;
using System.Linq;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Enums;

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
            return _GroupUserRepository.GetGroupUsersByUserId(user.UserId).Select(x => x.Group).ToList();
        }
        public void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            _GroupUserRepository.Insert(user, group, accountRoleWithinGroup);
            _GroupUserRepository.Save();
        }
    }
}
