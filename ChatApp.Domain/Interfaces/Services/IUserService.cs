using FluentResponses.Interfaces;

namespace ChatApp.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IResponse GetUserById(int Id);
        IResponse Login(string username, string password);
        IResponse Register(string name, string username, string emailaddress, string password);
        IResponse BlockUserById(int userId);
        IResponse AccountUpdate(int id, string username, string emailaddress, string password);
    }
}
