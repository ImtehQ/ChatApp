using ChatApp.Business.Core.DbContexts;
using ChatApp.Domain.Models;

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
