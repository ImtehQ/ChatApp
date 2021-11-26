using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Business.Core.Repositorys;
using ChatApp.Domain.Models;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Interfaces;

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
            IResponse response = new Response(MethodCode.GetInviteById, LayerCode.Service, inviteId);

            return response.Successfull(_InviteRepository.GetInviteById(inviteId));
        }

        public IResponse Register(Invite invite)
        {
            IResponse response = new Response(MethodCode.GetInviteById, LayerCode.Service, invite);

            _InviteRepository.Insert(invite);
            _InviteRepository.Save();

            return response.Successfull();
        }
    }
}
