using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public class AuthorizeResponse : Response
    {
        public static AuthorizeResponse Successfull()
        {
            return new AuthorizeResponse() { IsValid = true };
        }

        public static AuthorizeResponse Error(string message = "")
        {
            return new AuthorizeResponse() { IsValid = false, Message = message };
        }
    }
}
