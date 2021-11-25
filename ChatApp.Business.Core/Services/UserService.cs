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

        public IResponse GetUserById(int Id)
        {
            Bfet response = new Bfet(MethodCode.GetUserById, LayerCode.Service,
                new object[] { Id });

            return response.Link(_userRepository.GetUserByID(Id));
        }

        public IResponse Login(string username, string password)
        {
            Bfet response = new Bfet(MethodCode.Login, LayerCode.Service,
                new object[] { username, password });

            var response2 = response.Link(MethodCode.Login, LayerCode.Service, null);

            var response3 = new Response(response2, MethodCode.Login, LayerCode.Service, null);

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
            Bfet response = new Bfet(
                MethodCode.Register, LayerCode.Service,
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
            IResponse response = new Bfet(MethodCode.Block, LayerCode.Service, userId);

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

            IResponse response = new Bfet(MethodCode.Update, LayerCode.Service,
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
