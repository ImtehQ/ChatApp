using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace FluentResponses.Interfaces
{
    public interface IResponse
    {
        List<IResponse> Includes { get; }
        HttpStatusCode Code { get; set; }
        MethodInfo Invoker { get; init; }
        bool IsValid { get; set; }
        bool isNotValid { get; }
        ParameterInfo[] Parameters { get; init; }
        string Report { get; }
        Type Source { get; init; }

        object GetContent();
        E GetContent<E>();
        IResponse Include(IResponse response);
        IResponse Include(IResponse[] responses);
        IResponse SetContent(object Content);
        T SetContent<T>(object Content);
        void SetReport(string reportMessage);
        IResponse GetLastInclude();
        IResponse GetInclude(int index);
    }
}
