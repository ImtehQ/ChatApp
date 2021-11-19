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
            Response R = ResponseBuilder.new(Layer.Service);

            R.Error(Login)

            
            Response r = new Response() { Code = new Domain.Responses.ResponseCode() { LayerCode = ChatApp.Domain.Enums.ResponseCodes.ResponseLayerCode.Service } }
            var userFound = _userRepository.GetUsers().First(u => u.UserName == username);

            if(userFound == null)
                return r.Error(r, ResponseCode.LoginUserNotFound);
            if (userFound.isBlocked == true)
                return Response.Error(ResponseCode.LoginUserIsBlocked);
            if (userFound.PasswordHash != Rfc2898.Convert(password, username))
                return Response.Error(ResponseCode.LoginUserPasswordWrong);

            var Auth = _JWTAuthService.GetToken(userFound, _JWTToken);

            return Response.Successfull(Auth);
        }

        public IResponse Register(string name, string username, string emailaddress, string password)
        {
            var nameValidator = UserContentValidator.IsValidName(name);
            if (nameValidator.Valid == false) return Response.Error(nameValidator, 
                ResponseCode.RegisterNameInvalid);

            var usernameValidator = UserContentValidator.IsValidUsername(username);
            if (usernameValidator.Valid == false) return Response.Error(usernameValidator, 
                ResponseCode.RegisterUserNameInvalid);

            var emailValidator = UserContentValidator.IsValidEmailAddress(emailaddress);
            if (emailValidator.Valid == false) return Response.Error(emailValidator, 
                ResponseCode.RegisterEmailInvalid);

            var passwordValidator = UserContentValidator.IsValidPassword(password);
            if (passwordValidator.Valid == false) return Response.Error(passwordValidator, 
                ResponseCode.RegisterPasswordInvalid);

            //===================================================================

            _userRepository.InsertUser(new User() 
            { FirstName = name, UserName = username, Email = emailaddress, PasswordHash = Rfc2898.Convert(password, username) });
            _userRepository.Save();

            return Response.Successfull();
        }

        private bool Exist(string username, string password)
        {
            return _userRepository.GetUsers().Any(user =>
            user.UserName == username &&
            user.PasswordHash == password) ? true : false;
        }
    }
}
