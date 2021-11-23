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

        public User GetUserById(int Id)
        {
            return _userRepository.GetUserByID(Id);
        }

        public IResponse Login(string username, string password)
        {
            Response response = new Response(ResponseMethodCode.Login, ResponseLayerCode.Service,
                new object[] { username, password });
            
            var userFound = _userRepository.GetUsers().First(u => u.UserName == username);

            if (userFound == null)
                return response.Failed(System.Net.HttpStatusCode.NotFound, username);

            if (userFound.isBlocked == true)
                return response.Failed(System.Net.HttpStatusCode.Unauthorized, username);

            if (userFound.PasswordHash != Rfc2898.Convert(password, username))
                return response.Failed(System.Net.HttpStatusCode.Unauthorized, username);

            var Auth = _JWTAuthService.GetToken(userFound, _JWTToken);

            return response.Successfull(Auth);
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            Response response = new Response(
                ResponseMethodCode.Register, ResponseLayerCode.Service,
                new object[] { name, username, emailaddress, password });

            var nameValidator = UserContentValidator.RegisterName(name);
            if (nameValidator.Valid == false) return nameValidator;

            var usernameValidator = UserContentValidator.RegisterUsername(username);
            if (usernameValidator.Valid == false) return usernameValidator;

            var emailValidator = UserContentValidator.RegisterEmailAddress(emailaddress);
            if (emailValidator.Valid == false) return emailValidator;

            var passwordValidator = UserContentValidator.RegisterPassword(password);
            if (passwordValidator.Valid == false) return passwordValidator;

            //===================================================================

            _userRepository.InsertUser(new User() 
            { FirstName = name, UserName = username, Email = emailaddress, PasswordHash = Rfc2898.Convert(password, username) });
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
            IResponse response = new Response(ResponseMethodCode.Block, ResponseLayerCode.Service, userId);

            User user = _userRepository.GetUserByID(userId);
            if (user == null)
            {
                return response.Failed(HttpStatusCode.NotFound);
            }

            user.isBlocked = true;
            _userRepository.UpdateUser(user);
            return response.Successfull(HttpStatusCode.OK);
        }

        public IResponse AccountUpdate(int userId, string username, string emailaddress, string password)
        {
            var usernameValidator = UserContentValidator.RegisterUsername(username);
            if (usernameValidator.Valid == false) return usernameValidator;

            var emailValidator = UserContentValidator.RegisterEmailAddress(emailaddress);
            if (emailValidator.Valid == false) return emailValidator;

            var passwordValidator = UserContentValidator.RegisterPassword(password);
            if (passwordValidator.Valid == false) return passwordValidator;

            IResponse response = new Response(ResponseMethodCode.Update, ResponseLayerCode.Service,
            new object[] { userId, username, emailaddress, password });

            User user = _userRepository.GetUserByID(userId);

            if (user == null)
                response.Failed(HttpStatusCode.NotFound);

            user.UserName = username;
            user.Email = emailaddress;
            user.PasswordHash = Rfc2898.Convert(password, emailaddress);

            _userRepository.UpdateUser(user);

            return response.Successfull();
        }
    }
}
