using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.Services
{
    public interface IInviteService
    {
        IResponse Register(Invite invite);
        Invite GetInviteById(int inviteId);
    }
}