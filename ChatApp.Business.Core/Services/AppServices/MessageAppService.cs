using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.AppServices
{
    //Messages
    public partial class AppService : IAppService
    {
        public IResponse SendMessage(string message, User sender, int type, int typeId)
        {
            IResponse response = this.CreateResponse();
            return response.Include(_MessageService.SendMessage(message, sender, (GroupTypeEnum)type, typeId)).Successfull();
        }

        public IResponse PullMessages(int pageNr, int groupId)
        {
            IResponse response = this.CreateResponse();

            return response.Include(_MessageService.GetAllMessages(groupId, pageNr)).Successfull();

        }
    }
}
