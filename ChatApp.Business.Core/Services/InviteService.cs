using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Business.Core.Repositorys;
using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.Services
{
    public class InviteService : IInviteService
    {
        IInviteRepository _InviteRepository;

        public InviteService(IInviteRepository inviteRepository)
        {
            _InviteRepository = inviteRepository;
        }

        public Invite GetInviteById(int inviteId)
        {
            return _InviteRepository.GetInviteById(inviteId);
        }

        public IResponse Register(Invite invite)
        {
            IResponse response = new Response(ResponseMethodCode.RegisterInvite, ResponseLayerCode.Service, invite);

            _InviteRepository.Insert(invite);
            _InviteRepository.Save();

            return response.Successfull();
        }
    }
}
