using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.Services
{
    public class InviteService : IInviteService
    {
        IGenericRepository<Invite> _InviteRepository;

        public InviteService(IGenericRepository<Invite> inviteRepository)
        {
            _InviteRepository = inviteRepository;
        }

        public IResponse GetInviteById(int inviteId)
        {
            IResponse response = this.CreateResponse();
            response.SetAttachment(_InviteRepository.GetById(inviteId));
            return response.Successfull();
        }

        public IResponse Register(Invite invite)
        {
            IResponse response = this.CreateResponse();

            _InviteRepository.Insert(invite);
            _InviteRepository.Save();

            return response.Successfull();
        }
    }
}
