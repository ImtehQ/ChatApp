using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
namespace ChatApp.Business.Core.Validator
{
    public static class MessageContentValidator
    {
        public static IResponse CheckContent(string message)
        {
            IResponse response = new Response(MethodCode.CheckMessageContent, LayerCode.Validator, message);

            message = message.ToLower();
            if (message.Contains("kut"))
                return response.Failed(System.Net.HttpStatusCode.Forbidden);

            return response.Successfull();
        }
    }
}
