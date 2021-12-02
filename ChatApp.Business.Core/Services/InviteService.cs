using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Business.Core.Repositorys;
using ChatApp.Domain.Models;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Interfaces;
using FluentResponses.Interfaces;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;

namespace ChatApp.Business.Core.Services
{
    public class InviteService : IInviteService
    {
        IInviteRepository _InviteRepository;

        public InviteService(IInviteRepository inviteRepository)
        {
            _InviteRepository = inviteRepository;
        }

        public IResponse GetInviteById(int inviteId)
        {
            IResponse response = this.CreateResponse();
            response.Contents(_InviteRepository.GetInviteById(inviteId));
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
