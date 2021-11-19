using ChatApp.Business.Core.DbContexts;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Repositorys
{
    public class EFGroupUserRepository : IGroupUserRepository
    {
        private ChatAppContext context;

        public EFGroupUserRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public List<GroupUser> GetGroupUsersByUserId(int userId)
        {
            return context.GroupUsers.Where(x => x.User.UserId == userId).ToList();
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
