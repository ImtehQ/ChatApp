using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public class LoginResponse : Response
    {
        public ValidatorResponse ValidatorResponse { get; init; }
        public AuthenticationModel _authentication { get; init; }

        public static LoginResponse Successfull(AuthenticationModel authentication)
        {
            return new LoginResponse() { IsValid = true, _authentication = authentication };
        }

        public static LoginResponse Error(ValidatorResponse validatorResponse, string message = "")
        {
            return new LoginResponse() { IsValid = false, ValidatorResponse = validatorResponse, Message = message, _authentication = null };
        }
        public static LoginResponse Error(string message = "")
        {
            return new LoginResponse() { IsValid = false, Message = message, _authentication = null };
        }
    }
}
