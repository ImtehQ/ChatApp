using FluentResponses.Interfaces;
using FluentResponses.Models;
using System.Net;
using System.Runtime.CompilerServices;

namespace FluentResponses.Extensions.Initialize
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


namespace FluentResponses.Extensions
{
    public static class ResponseExtensions
    {

        public static Contents Add(this Contents contents, object content)
        {
            return new Contents(content);
        }

        public static bool Status(this IResponse response)
        {
            return response.Status.Value;
        }

        public static void Content(this IResponse response, object content)
        {
            response.Contents = new Contents( content );
        }

        public static object Content(this IResponse response)
        {
            return response.Contents.Content;
        }

        public static IResponse Result(this IResponse response)
        {
            if (response.status)
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
            response.Status.Value = false;
            response.Code = FailedCode;
            response.SetReport(reportMessage);
            return response;
        }

    }
}
