using ChatApp.Business.Core.DbContexts;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Repositorys
{
    public class EFInviteRepository : IInviteRepository
    {
        private ChatAppContext context;

        public EFInviteRepository(ChatAppContext context)
        {
            this.context = context;
        }

        public Invite GetInviteById(int id)
        {
            return context.Invites.Find(id);
        }

        public void Insert(Invite invite)
        {
            context.Invites.Add(invite);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
