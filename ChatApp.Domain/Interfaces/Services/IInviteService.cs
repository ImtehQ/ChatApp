using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IInviteService
    {
        IResponse Register(Invite invite);
        IResponse GetInviteById(int inviteId);
    }
}