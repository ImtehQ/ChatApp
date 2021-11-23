using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Enums.ResponseCodes
{
    public enum ResponseMethodCode
    {
        Register = 0,
        Login = 1,
        Block = 2,
        List = 3,
        Update = 4,
        SendMessage = 5,
        CheckMessageContent = 6,
        GetMessages = 7,
        Join = 8,
        RegisterInvite = 9,
    }
}
