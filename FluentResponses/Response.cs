using FluentResponses.Conditions;
using FluentResponses.Interfaces;
using FluentResponses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace FluentResponses
{
    public class Response : IResponse
    {
        public Type Source { get; init; }
        public ParameterInfo[] Parameters { get; init; }
        public MethodInfo Invoker { get; init; }

        private HttpStatusCode _Code { get; set; }
        public Response Code(HttpStatusCode statusCode)
        {
            _Code = statusCode;
            return this;
        }
        public HttpStatusCode Code()
        {
            return _Code;
        }

        private Report _Report { get; set; }
        public Response Report(string content)
        {
            _Report = new Report(content);
            return this;
        }
        public string Report()
        {
            return _Report.Content;
        }

        private Contents _Contents { get; set; }
        public Response Contents(object content)
        {
            _Contents = new Contents(content);
            return this;
        }
        public T Contents<T>(object content)
        {
            _Contents = new Contents(content);
            return (T)_Contents.Content;
        }
        public object Contents()
        {
            return _Contents.Content;
        }
        public T Contents<T>()
        {
            return (T)_Contents.Content;
        }

        private Responses _Includes { get; set; }
        public Response Includes(IResponse response)
        {
            _Includes = new Responses(response);
            return this;
        }
        public List<IResponse> Includes()
        {
            return _Includes.responses;
        }
        public IResponse LastIncluded()
        {
            return _Includes.Last();
        }

        private Status _Status { get; set; }
        public Response Status(bool value)
        {
            _Status = new Status(value);
            return this;
        }
        public bool Status()
        {
            return _Status.Value;
        }

        internal Response(object Caller, string MethodName)
        {
            Type Source = Caller.GetType();
            Invoker = Source.GetMethod(MethodName);
            Parameters = Invoker.GetParameters();
        }

        internal Response(IResponse Parent, object Caller, string MethodName)
        {
            Type Source = Caller.GetType();
            Invoker = Source.GetMethod(MethodName);
            Parameters = Invoker.GetParameters();
            Parent.Includes(this);
        }
    }
}
