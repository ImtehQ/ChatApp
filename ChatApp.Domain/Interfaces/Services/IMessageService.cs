using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IMessageService
    {
        IResponse SendMessage(string message, User sender, GroupTypeEnum groupType, int groupId);
    }
}
