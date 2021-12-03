using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        IResponse GetAllMessages(int groupId, int pageNr);
        IResponse GetAllMessages(int groupId, int pageNr, string Query);
        IResponse SendMessage(string message, User sender, GroupTypeEnum groupType, int groupId);
    }
}
