using ChatApp.Domain.Interfaces;
using System.Linq;
using ChatApp.Business.Core.Validator;
using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Models;
using ChatApp.Business.Core.Cryptography;
using Microsoft.Extensions.Options;
using ChatApp.Business.Core.Authentication;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using System;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Net;
using ChatApp.Domain.Interfaces.EchoResponse;
using ChatApp.Business.Core.EchoResponse;
using ChatApp.Business.Core.EchoResponse.Extensions;
using FluentResponses.Interfaces;
using FluentResponses.Extensions;
using FluentResponses;

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
            return this.CreateResponse()
                .SetContent(_userRepository.GetUserByID(Id))
                .IfNotNull()
                .Result();
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

            _userRepository.InsertUser(response.SetContent<User>( new User()
                { FirstName = name, 
                UserName = username, 
                Email = emailaddress, 
                PasswordHash = Rfc2898.Convert(password, username) }));

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

            if(user != null)
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
