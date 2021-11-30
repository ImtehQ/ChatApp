using FluentResponses.Conditions;
using FluentResponses.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace FluentResponses.Interfaces
{
    public interface IResponse
    {
        MethodInfo Invoker { get; init; }
        ParameterInfo[] Parameters { get; init; }
        Type Source { get; init; }

        HttpStatusCode Code();
        Response Code(HttpStatusCode statusCode);
        object Contents();
        Response Contents(object content);
        T Contents<T>(object content);
        T Contents<T>();
        Response Includes(IResponse response);
        IResponse LastIncluded();
        string Report();
        Response Report(string content);
        bool Status();
        Response Status(bool value);
    }
}
