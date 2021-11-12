using ChapApp.Business.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Business.Core.Validator;
using ChatApp.Business.Core.Responses;

namespace ChatApp.Business.Core.Services
{
   
    public class UserService : IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Login(string username, string password)
        {
            if (Exist(username, password))
                return "Key token thingy";
            return "";
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

            //stuff

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
