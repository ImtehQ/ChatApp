using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.AppServices
{
    //User
    public partial class AppService : IAppService
    {
        public IResponse GetUserById(int userId)
        {
            return this.CreateResponse().Includes(_UserService.GetUserById(userId)).Successfull();
        }
        public IResponse ListUsers(User user, GroupTypeEnum groupType)
        {
            return this.CreateResponse()
                .Includes(_GroupUserService.GetAllUsersByGroupType(user, groupType))
                .Successfull();
        }

        public IResponse LoginUser(string Username, string Password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Includes(_UserService.Login(Username, Password)).Successfull();
        }

        public IResponse RegisterUser(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Includes(_UserService.Register(Name, Username, Emailaddress, Password)).Successfull();
        }

        public IResponse AccountUpdateUser(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse();

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            }

            return response.Includes(_UserService.AccountUpdate(id, Username, Emailaddress, Password)).Successfull();
        }

        public IResponse BlockUser(int userId)
        {
            IResponse response = this.CreateResponse();
            if (userId <= 0)
                return response.Failed(System.Net.HttpStatusCode.BadRequest);
            return response.Includes(_UserService.BlockUserById(userId)).Successfull();
        }

        public IResponse ListGroups(int GroupId, User user)
        {
            throw new System.NotImplementedException();
        }

        public IResponse RegisterGroup(int UserId, string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            throw new System.NotImplementedException();
        }

        public IResponse RemoveUserFromGroup(int userId, int GroupId)
        {
            throw new System.NotImplementedException();
        }
    }
}
