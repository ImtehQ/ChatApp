using ChapApp.Business.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Business.Core.Validator;
using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Models;
using ChatApp.Business.Core.Cryptography;
using Microsoft.Extensions.Options;
using ChatApp.Business.Core.Authentication;

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

        public IResponse Login(string username, string password)
        {
            var userFound = _userRepository.GetUsers().First(u => u.UserName == username);

            if(userFound == null)
                return LoginResponse.Error("User not found");
            if (userFound.isBlocked == true)
                return LoginResponse.Error("User is blocked");
            if (userFound.PasswordHash != Rfc2898.Convert(password, username))
                return LoginResponse.Error("password is wrong");

            var Auth = _JWTAuthService.GetToken(userFound, _JWTToken);

            return LoginResponse.Successfull(Auth);
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            var namevr = UserContentValidator.IsValidName(name);
            if (namevr.IsNotValid) return RegisterResponse.Error(namevr);

            var usernamevr = UserContentValidator.IsValidUsername(username);
            if (usernamevr.IsNotValid) return RegisterResponse.Error(usernamevr);

            var emailvr = UserContentValidator.IsValidEmailAddress(emailaddress);
            if (emailvr.IsNotValid) return RegisterResponse.Error(emailvr);

            var passwordvr = UserContentValidator.IsValidPassword(password);
            if (passwordvr.IsNotValid) return RegisterResponse.Error(passwordvr);

            _userRepository.InsertUser(new User() 
            { FirstName = name, UserName = username, Email = emailaddress, PasswordHash = Rfc2898.Convert(password, username) });
            _userRepository.Save();

            return RegisterResponse.Successfull();
        }

        private bool Exist(string username, string password)
        {
            return _userRepository.GetUsers().Any(user =>
            user.UserName == username &&
            user.PasswordHash == password) ? true : false;
        }
    }
}
