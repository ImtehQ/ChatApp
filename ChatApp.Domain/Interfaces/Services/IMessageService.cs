using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Interfaces
{
    public interface IMessageService
    {
        IResponse SendMessage(string message, User sender, GroupTypeEnum groupType, int GroupId);
        IResponse GetAllMessages(int groupId, int pageNr);
        IResponse GetAllMessages(int groupId, int pageNr, string Query);
    }
}
