using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public class RegisterResponse : Response
    {
        public ValidatorResponse ValidatorResponse { get; init; }

        public static RegisterResponse Check(ValidatorResponse validatorResponse)
        {
            if (validatorResponse.IsValid)
                return Successfull();
            return Error(validatorResponse);
        }

        public static RegisterResponse Successfull()
        {
            return new RegisterResponse() { IsValid = true };
        }

        public static RegisterResponse Error(ValidatorResponse validatorResponse, string message = "")
        {
            return new RegisterResponse() { IsValid = false, ValidatorResponse = validatorResponse, Message = message };
        }
        public static RegisterResponse Error(string message = "")
        {
            return new RegisterResponse() { IsValid = false, Message = message };
        }
    }
}
