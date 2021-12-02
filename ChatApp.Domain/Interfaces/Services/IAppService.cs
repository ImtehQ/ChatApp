using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IAppService
    {
        IResponse AccountUpdate(int id, string Username, string Emailaddress, string Password);
        IResponse BlockUser(int userId);
        IResponse List(User user, GroupTypeEnum groupType);
        IResponse Login(string Username, string Password);
        IResponse Register(string Name, string Username, string Emailaddress, string Password);
    }
}
