using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Interfaces;
using System;

namespace ChatApp.Business.Core.MessageValidator
{
    public static class MessageValidator
    {
        internal static IResponse CheckContent(this IResponse response, string message)
        {
            return response.Successfull();
        }
    }
}
