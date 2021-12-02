using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Cryptography;
using ChatApp.Business.Core.Validator;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Checks;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
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
            IResponse response = this.CreateResponse()
                .Contents(_userRepository.GetUserByID(Id))
                .CheckValidIfContentNotNull();

            return response.Return(httpStatusFailed: HttpStatusCode.NotFound);
        }

        public IResponse Login(string username, string password)
        {
            IResponse response = this.CreateResponse();

            User user = response.Contents<User>(_userRepository.GetUsers().First(u => u.UserName == username));

            if (user == null)
                return response.Failed(username, HttpStatusCode.NotFound);

            if (user.isBlocked == true)
                return response.Failed(username, HttpStatusCode.Unauthorized);

            if (user.PasswordHash != Rfc2898.Convert(password, username))
                return response.Failed(username, HttpStatusCode.Unauthorized);

            response.Contents(_JWTAuthService.GetToken(user, _JWTToken));

            return response.Successfull();
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            if (response.Includes(UserContentValidator.RegisterName(name)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            if (response.Includes(UserContentValidator.RegisterUsername(username)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            if (response.Includes(UserContentValidator.RegisterEmailAddress(emailaddress)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            if (response.Includes(UserContentValidator.RegisterPassword(password)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);


            //===================================================================

            _userRepository.InsertUser(response.Contents<User>(new User()
            {
                FirstName = name,
                UserName = username,
                Email = emailaddress,
                PasswordHash = Rfc2898.Convert(password, username)
            }));

            _userRepository.Save();

            return response.Successfull();
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

            User user = response.Contents<User>(_userRepository.GetUserByID(userId));

            if (response.CheckValidIfContentNotNull().Status() == true)
            {
                user.isBlocked = true;
                _userRepository.UpdateUser(user);
            }

            return response.Successfull();
        }

        public IResponse AccountUpdate(int userId, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            User user = response.Includes(GetUserById(userId)).LastIncluded().Contents<User>();
            if (response.LastIncluded().Status() == false)
                return response.Failed();

            if (response.Includes(UserContentValidator.RegisterUsername(username)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            if (response.Includes(UserContentValidator.RegisterEmailAddress(emailaddress)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            if (response.Includes(UserContentValidator.RegisterPassword(password)).LastIncluded().Status() == false)
                return response.Failed(username, HttpStatusCode.NotAcceptable);

            user.UserName = username;
            user.Email = emailaddress;
            user.PasswordHash = Rfc2898.Convert(password, emailaddress);

            _userRepository.UpdateUser(user);

            return response.Successfull();
        }
    }
}
