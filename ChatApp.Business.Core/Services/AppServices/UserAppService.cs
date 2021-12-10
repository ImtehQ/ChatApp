using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Net;

namespace ChatApp.Business.Core.AppServices
{
    //User
    public partial class AppService : IAppService
    {
        public IResponse GetUserById(int userId)
        {
            return this.CreateResponse().Include(_UserService.GetUserById(userId));
        }
        public IResponse ListUsers(User user, GroupTypeEnum groupType)
        {
            return this.CreateResponse()
                .Include(_GroupUserService.GetAllUsersByGroupType(user, groupType));
        }

        public IResponse LoginUser(string username, string password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Include(_UserService.Login(username, password));
        }

        public IResponse RegisterUser(string name, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(name))
                return response.Failed(message: "name is empty", HttpStatusCode.NotAcceptable);
            if (string.IsNullOrEmpty(username))
                return response.Failed(message: "username is empty", HttpStatusCode.NotAcceptable);
            if (string.IsNullOrEmpty(emailaddress))
                return response.Failed(message: "emailaddress is empty", HttpStatusCode.NotAcceptable);
            if (string.IsNullOrEmpty(password))
                return response.Failed(message: "password is empty", HttpStatusCode.NotAcceptable);

            return response.Include(_UserService.Register(name, username, emailaddress, password));
        }

        public IResponse AccountUpdateUser(int id, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Include(_UserService.AccountUpdate(id, username, emailaddress, password));
        }

        public IResponse BlockUser(int userId)
        {
            IResponse response = this.CreateResponse();
            if (userId <= 0)
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            return response.Include(_UserService.BlockUserById(userId));
        }

        public IResponse ListGroups(int groupId, User user)
        {
            throw new System.NotImplementedException();
        }

        public IResponse RegisterGroup(int userId, string name, string password, int maxUsers = 0, GroupVisibilityEnum visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum groupType = GroupTypeEnum.OptionGroup)
        {
            throw new System.NotImplementedException();
        }

        public IResponse RemoveUserFromGroup(int userId, int GroupId)
        {
            throw new System.NotImplementedException();
        }
    }
}
