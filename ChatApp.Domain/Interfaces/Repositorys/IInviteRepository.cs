using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.Repositorys
{
    public interface IInviteRepository
    {
        Invite GetInviteById(int id);
        void Insert(Invite invite);
        void Save();
    }
}