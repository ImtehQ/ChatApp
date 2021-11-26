using FluentResponses.Interfaces;
using System.Net;
using System.Runtime.CompilerServices;

namespace FluentResponses.Extensions
{
    public static class ResponseExtensions
    {
        public static Response CreateResponse(this object Caller, IResponse Parent, [CallerMemberName] string MethodName = "")
        {
            return new Response(Parent, Caller, MethodName);
        }
        public static Response CreateResponse(this object Caller, [CallerMemberName] string MethodName = "")
        {
            return new Response(Caller, MethodName);
        }

        public static IResponse Result(this IResponse response)
        {
            if (response.IsValid)
                return response.ResultSuccessfull();
            else
                return response.ResultFailed();
        }

        public static IResponse ResultSuccessfull(this IResponse response,
            HttpStatusCode SuccessfullCode = HttpStatusCode.OK, string reportMessage = "")
        {
            response.IsValid = true;
            response.Code = SuccessfullCode;
            response.SetReport(reportMessage);
            return response;
        }
        public static IResponse ResultFailed(this IResponse response,
            HttpStatusCode FailedCode = HttpStatusCode.BadRequest, string reportMessage = "")
        {
            response.IsValid = false;
            response.Code = FailedCode;
            response.SetReport(reportMessage);
            return response;
        }

        public static IResponse IfNull(this IResponse response)
        {
            if (response.GetContent() == null)
                response.IsValid = true;
            else
                response.IsValid = false;
            return response;
        }
        public static IResponse IfNotNull(this IResponse response)
        {
            if (response.GetContent() != null)
                response.IsValid = true;
            else
                response.IsValid = false;
            return response;
        }
    }
}
