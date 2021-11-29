using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Cryptography;
using ChatApp.Business.Core.Validator;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions;
using FluentResponses.Extensions.Summary;
using FluentResponses.Interfaces;
using FluentResponses.Extensions.Initialize;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;

namespace ChatApp.Business.Core.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IJWTAuthService _JWTAuthService;
        JWTToken _JWTToken;

        public UserService(IUserRepository userRepository, IOptions<JWTToken> jwt, IJWTAuthService JWTAuthService)
        {
            _userRepository = userRepository;
            _JWTAuthService = JWTAuthService;
            _JWTToken = jwt.Value;
        }

        public IResponse GetUserById(int Id)
        {
            IResponse response = this.CreateResponse().Contents(_userRepository.GetUserByID(Id));
            if(response.Status() == false)
                this.CreateResponse().Report("User is not found").Code(HttpStatusCode.NotFound);

        }

        public IResponse Login(string username, string password)
        {
            IResponse response = this.CreateResponse();


            User user = response.SetContent(_userRepository.GetUsers().First(u => u.UserName == username))
                .IfNotNull()
                .GetContent<User>();

            if (user == null)
                return response.ResultFailed(HttpStatusCode.NotFound, username);

            if (user.isBlocked == true)
                return response.ResultFailed(HttpStatusCode.Unauthorized, username);

            if (user.PasswordHash != Rfc2898.Convert(password, username))
                return response.ResultFailed(HttpStatusCode.Unauthorized, username);

            response.SetContent(_JWTAuthService.GetToken(user, _JWTToken));

            return response.Result();
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            response.Include(UserContentValidator.RegisterName(name));
            if (response.GetLastInclude().isNotValid)
                return response.ResultFailed();

            response.Include(UserContentValidator.RegisterUsername(username));
            if (response.GetLastInclude().isNotValid)
                return response.ResultFailed();

            response.Include(UserContentValidator.RegisterEmailAddress(emailaddress));
            if (response.GetLastInclude().isNotValid)
                return response.ResultFailed();

            response.Include(UserContentValidator.RegisterPassword(password));
            if (response.GetLastInclude().isNotValid)
                return response.ResultFailed();


            //===================================================================

            _userRepository.InsertUser(response.SetContent<User>(new User()
            {
                FirstName = name,
                UserName = username,
                Email = emailaddress,
                PasswordHash = Rfc2898.Convert(password, username)
            }));

            _userRepository.Save();

            return response.ResultSuccessfull();
        }

        private bool Exist(string username, string password)
        {
            return _userRepository.GetUsers().Any(user =>
            user.UserName == username &&
            user.PasswordHash == password) ? true : false;
        }

        public IResponse BlockUserById(int userId)
        {
            IResponse response = this.CreateResponse();

            User user = response
                .SetContent(_userRepository
                .GetUserByID(userId))
                .IfNotNull()
                .GetContent<User>();

            if (user != null)
            {
                user.isBlocked = true;
                _userRepository.UpdateUser(user);
            }

            return response.Result();
        }

        public IResponse AccountUpdate(int userId, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            response.Include(UserContentValidator.RegisterUsername(username));
            if (response.GetLastInclude().isNotValid)
                return response;

            response.Include(UserContentValidator.RegisterEmailAddress(emailaddress));
            if (response.GetLastInclude().isNotValid)
                return response;

            response.Include(UserContentValidator.RegisterPassword(password));
            if (response.GetLastInclude().isNotValid)
                return response;

            response.Include(GetUserById(userId));
            if (response.GetLastInclude().isNotValid)
                return response;

            User user = response.GetLastInclude().GetContent<User>();

            user.UserName = username;
            user.Email = emailaddress;
            user.PasswordHash = Rfc2898.Convert(password, emailaddress);

            _userRepository.UpdateUser(user);

            return response.ResultSuccessfull();
        }
    }
}
