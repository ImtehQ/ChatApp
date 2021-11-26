using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace FluentResponses
{
    public class Response : IResponse
    {
        public bool IsValid { get; set; }
        public bool isNotValid { get { return !IsValid; } }

        public Type Source { get; init; }
        public ParameterInfo[] Parameters { get; init; }
        public MethodInfo Invoker { get; init; }

        public HttpStatusCode Code { get; set; }
        public string Report { get; private set; }
        private object _Content { get; set; }
        public List<IResponse> Includes { get; private set; }

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
            Parent.Includes.Add(this);
        }

        public IResponse SetContent(object Content)
        {
            _Content = Content;
            return this;
        }
        public T SetContent<T>(object Content)
        {
            _Content = Content;
            return (T)Content;
        }
        public object GetContent()
        {
            return _Content;
        }
        public E GetContent<E>()
        {
            return (E)_Content;
        }

        public IResponse GetLastInclude()
        {
            return Includes.Last();
        }

        public IResponse GetInclude(int index)
        {
            return Includes[index];
        }

        public IResponse Include(IResponse response)
        {
            Includes.Add(response);
            return this;
        }
        public IResponse Include(IResponse[] responses)
        {
            Includes.AddRange(responses);
            return this;
        }

        public void SetReport(string reportMessage)
        {
            Report = reportMessage;
        }
    }
}
