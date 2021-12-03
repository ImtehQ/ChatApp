using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.AppServices
{
    //Messages
    public partial class AppService : IAppService
    {
        public IResponse SendMessage(string Message, User Sender, int Type, int TypeId)
        {
            IResponse response = this.CreateResponse();
            return response.Includes(_MessageService.SendMessage(Message, Sender, (GroupTypeEnum)Type, TypeId)).Successfull();
        }

        public IResponse PullMessages(int pageNr, int groupId)
        {
            IResponse response = this.CreateResponse();

            return response.Includes(_MessageService.GetAllMessages(groupId, pageNr)).Successfull();

        }
    }
}
