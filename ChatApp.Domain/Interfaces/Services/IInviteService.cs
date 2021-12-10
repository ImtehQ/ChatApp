using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IInviteService
    {
        IResponse GetInviteById(int inviteId);
        IResponse Register(Invite invite);
    }
}