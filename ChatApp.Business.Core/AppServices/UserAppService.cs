using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.AppServices
{
    //User
    public partial class AppService : IAppService
    {
        public IResponse List(int userId, GroupTypeEnum groupType)
        {
            IResponse response = new Bfet(MethodCode.List, LayerCode.Service, userId);
            IResponse userResponse = _UserService.GetUserById(userId);
            response.Link(userResponse);
            if (userResponse.Valid == false) return response;

            return response.Link(
                _GroupUserService.GetAllUsersByGroupType(response.GetResponseObject<User>(), groupType));
        }

        public IResponse Login(string Username, string Password)
        {
            IResponse response = new Bfet(MethodCode.Login, LayerCode.Service, Username);

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Username))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Link(_UserService.Login(Username, Password));
        }

        public IResponse Register(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = new Bfet(MethodCode.Register, LayerCode.Service, Username);
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Link(_UserService.Register(Name, Username, Emailaddress, Password));
        }

        public IResponse AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = new Bfet(MethodCode.AccountUpdate, LayerCode.Service, Username);

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Link(_UserService.AccountUpdate(id, Username, Emailaddress, Password));
        }

        public IResponse BlockUser(int userId)
        {
            IResponse response = new Bfet(MethodCode.BlockUser, LayerCode.Service, userId);
            if (userId <= 0)
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            return response.Link(_UserService.BlockUserById(userId));
        }
    }
}
