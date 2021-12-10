using ChatApp.Business.Core.DbContexts;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Business.Core.Repositorys
{
    public class EFGroupUserRepository : IGroupUserRepository
    {
        private ChatAppContext context;

        public EFGroupUserRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public void DeleteGroupUser(Group group)
        {
            List<GroupUser> results = context.GroupUsers.Where(gu => gu.Group == group).ToList();

            context.GroupUsers.RemoveRange(results);
        }

        public void DeleteUserFromGroupUsers(User user, Group group)
        {
            List<GroupUser> results = context.GroupUsers.Where(gu => gu.User == user && gu.Group == group).ToList();

            context.GroupUsers.RemoveRange(results);
        }

        public GroupUser GetGroupUser(User user, Group group)
        {
            return context.GroupUsers.Where(gu => gu.User == user && gu.Group == group).FirstOrDefault();
        }

        public IEnumerable<GroupUser> GetGroupUsers()
        {
            return context.GroupUsers.Include(g => g.Group).ToList();
        }

        public void Insert(User user, Group group, AccountRoleEnum accountRoleWithinGroup)
        {
            GroupUser groupUser = new GroupUser() { User = user, Group = group, AccountRole = accountRoleWithinGroup };
            context.GroupUsers.Add(groupUser);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
