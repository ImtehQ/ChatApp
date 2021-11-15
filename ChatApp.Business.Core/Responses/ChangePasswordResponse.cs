using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public class ChangePasswordResponse : Response
    {
        public ValidatorResponse ValidatorResponse { get; init; }

        public static ChangePasswordResponse Successfull()
        {
            return new ChangePasswordResponse() { IsValid = true };
        }

        public static ChangePasswordResponse Error(ValidatorResponse validatorResponse, string message = "")
        {
            return new ChangePasswordResponse() { IsValid = false, ValidatorResponse = validatorResponse, Message = message };
        }
    }
}
