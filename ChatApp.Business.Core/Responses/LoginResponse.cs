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

        public string Token { get; init; }

        public static LoginResponse Successfull(string token)
        {
            return new LoginResponse() { IsValid = true, Token = token };
        }

        public static LoginResponse Error(ValidatorResponse validatorResponse, string message = "")
        {
            return new LoginResponse() { IsValid = false, ValidatorResponse = validatorResponse, Message = message };
        }
        public static LoginResponse Error(string message = "")
        {
            return new LoginResponse() { IsValid = false, Message = message };
        }
    }
}
