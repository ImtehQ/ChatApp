using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using ChatApp.Domain.Interfaces.EchoResponse;
using ChatApp.Business.Core.EchoResponse;
using ChatApp.Business.Core.EchoResponse.Extensions;
using FluentResponses.Interfaces;
using FluentResponses.Extensions;

namespace ChatApp.Business.Core.AppServices
{
    //User
    public partial class AppService : IAppService
    {
        public IResponse List(int userId, GroupTypeEnum groupType)
        {
            IResponse response = new Response(MethodCode.List, LayerCode.Service, userId);
            IResponse userResponse = _UserService.GetUserById(userId);
            response.Link(userResponse);
            if (userResponse.Valid == false) return response;

            return response.Link(
                _GroupUserService.GetAllUsersByGroupType(response.GetResponseObject<User>(), groupType));
        }

        public IResponse Login(string Username, string Password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return response.ResultFailed(System.Net.HttpStatusCode.BadRequest);
            }
             
            return response.Include(_UserService.Login(Username, Password));
        }

        public IResponse Register(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = new Response(MethodCode.Register, LayerCode.Service, Username);
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Link(_UserService.Register(Name, Username, Emailaddress, Password));
        }

        public IResponse AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = new Response(MethodCode.AccountUpdate, LayerCode.Service, Username);

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Link(_UserService.AccountUpdate(id, Username, Emailaddress, Password));
        }

        public IResponse BlockUser(int userId)
        {
            IResponse response = new Response(MethodCode.BlockUser, LayerCode.Service, userId);
            if (userId <= 0)
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            return response.Link(_UserService.BlockUserById(userId));
        }
    }
}
