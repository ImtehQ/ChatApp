using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Cryptography;
using ChatApp.Business.Core.Validator;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;

namespace ChatApp.Business.Core.Services
{
    public class UserService : IUserService
    {
        IGenericRepository<User> _userRepository;
        IJWTAuthService _JWTAuthService;
        JWTToken _JWTToken;

        public UserService(IGenericRepository<User> userRepository, IOptions<JWTToken> jwt, IJWTAuthService JWTAuthService)
        {
            _userRepository = userRepository;
            _JWTAuthService = JWTAuthService;
            _JWTToken = jwt.Value;
        }

        public IResponse GetUserById(int Id)
        {
            IResponse response = this.CreateResponse();
            if (Id <= 0)
                return response.Failed("Invalid user Id");

            response.SetAttachment(_userRepository.GetById(Id));
            if (response.GetValid() == false)
                return response.Failed("User not found!");
            return response.Successfull();
        }

        public IResponse Login(string username, string password)
        {
            IResponse response = this.CreateResponse();

            User user = response.SetAttachmentReturn<User>(_userRepository.GetAll().FirstOrDefault(u => u.UserName == username));

            if (user == null)
                return response.Failed("user not found", HttpStatusCode.NotFound);

            if (user.isBlocked == true)
                return response.Failed(username, HttpStatusCode.Unauthorized);
            string pwsHash = Rfc2898.Convert(password, username);

            if (user.PasswordHash != pwsHash)
                return response.Failed(username, HttpStatusCode.Unauthorized);

            response.SetAttachment(_JWTAuthService.GetToken(user, _JWTToken));

            return response.Successfull();
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();

            if (response.Include(UserContentValidator.RegisterName(name)).GetValid() == false)
                return response.Failed(message: "name to short", HttpStatusCode.NotAcceptable);

            if (response.Include(UserContentValidator.RegisterUsername(username)).GetValid() == false)
                return response.Failed(message: "username to short", HttpStatusCode.NotAcceptable);

            if (response.Include(UserContentValidator.RegisterEmailAddress(emailaddress)).GetValid() == false)
                return response.Failed(message: "emailaddress to short", HttpStatusCode.NotAcceptable);

            if (response.Include(UserContentValidator.RegisterPassword(password)).GetValid() == false)
                return response.Failed(message: "password to short", HttpStatusCode.NotAcceptable);

            if (_userRepository.GetAll().Where(x => x.Email == emailaddress).Count() > 0)
                return response.Failed(message: "emailaddress already exists", HttpStatusCode.NotAcceptable);

            if (_userRepository.GetAll().Where(x => x.UserName == username).Count() > 0)
                return response.Failed(message: "username already exists", HttpStatusCode.NotAcceptable);

            //===================================================================

            _userRepository.Insert(
                response.SetAttachmentReturn<User>(new User()
                {
                    FirstName = name,
                    UserName = username,
                    Email = emailaddress,
                    PasswordHash = Rfc2898.Convert(password, username)
                }
            ));

            _userRepository.Save();

            return response.Successfull();
        }

        private bool Exist(string username, string password)
        {
            return _userRepository.GetAll().Any(user =>
            user.UserName == username &&
            user.PasswordHash == password) ? true : false;
        }

        public IResponse BlockUserById(int userId)
        {
            IResponse response = this.CreateResponse();
            if (userId <= 0)
                return response.Failed("Invalid user Id");

            User user = response.SetAttachmentReturn<User>(_userRepository.GetById(userId));

            if (user == null)
                return response.Failed("User not found!");

            user.isBlocked = true;
            _userRepository.Update(user);

            return response.Successfull();
        }

        public IResponse AccountUpdate(int userId, string username, string emailaddress, string password)
        {
            IResponse response = this.CreateResponse();
            if (userId <= 0)
                return response.Failed("Invalid user Id");

            User user = response.Include(GetUserById(userId)).GetAttachment<User>();
            if (response.GetValid() == false)
                return response;

            if (response.Include(UserContentValidator.RegisterUsername(username)).GetValid() == false)
                return response.Failed(message: "username to short", HttpStatusCode.NotAcceptable);

            if (response.Include(UserContentValidator.RegisterEmailAddress(emailaddress)).GetValid() == false)
                return response.Failed(message: "emailaddress to short", HttpStatusCode.NotAcceptable);

            if (response.Include(UserContentValidator.RegisterPassword(password)).GetValid() == false)
                return response.Failed(message: "password to short", HttpStatusCode.NotAcceptable);


            user.UserName = username;
            user.Email = emailaddress;
            user.PasswordHash = Rfc2898.Convert(password, emailaddress);

            _userRepository.Update(user);

            return response.Successfull();
        }
    }
}
