using FluentResponses.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace FluentResponses.Extensions.Initializers
{
    public static class InitializeExtensions
    {
        public static Response CreateResponse(this object Caller, [CallerMemberName] string MethodName = "")
        {
            return new Response(Caller, MethodName);
        }

        public static Response CreateResponse(Type Caller, [CallerMemberName] string MethodName = "")
        {
            return new Response(Caller, MethodName);
        }

        public static Response Include(this IResponse parentResponse, IResponse childResponse)
        {
            Response r = (Response)parentResponse;
            r.Copy((Response)childResponse);
            return r;
        }
    }
}