using FluentResponses.Interfaces;
using FluentResponses.Models;
using System.Net;
using System.Runtime.CompilerServices;

namespace FluentResponses.Extensions.Initializers
{
    public static class InitializeExtensions
    {
        public static Response CreateResponse(this object Caller, IResponse Parent, [CallerMemberName] string MethodName = "")
        {
            return new Response(Parent, Caller, MethodName);
        }
        public static Response CreateResponse(this object Caller, [CallerMemberName] string MethodName = "")
        {
            return new Response(Caller, MethodName);
        }
    }
}