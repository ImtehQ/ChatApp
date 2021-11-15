using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Responses
{
    public class ValidatorResponse : Response
    {
        public string Field { get; private set; }

        public static ValidatorResponse Successfull()
        {
            return new ValidatorResponse() { IsValid = true };
        }

        public static ValidatorResponse Error(string message)
        {
            return new ValidatorResponse() { IsValid = false, Message = message };
        }
    }
}
