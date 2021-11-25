using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Enums.ResponseCodes
{
    public enum MethodCode
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
        GetInviteById = 9,
        GetUserById = 10,
        CheckUserExist = 11,
        GetGroupById = 12,
        GetGroupsByUser = 13,
        GetGroupsRoleByUser = 14,
        AccountUpdate = 15,
        BlockUser = 16,
        Insert = 17,
        Invite = 18,
    }
}
