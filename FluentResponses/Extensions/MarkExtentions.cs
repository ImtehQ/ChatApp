using FluentResponses.Interfaces;
using System.Net;

namespace FluentResponses.Extensions.MarkExtentions
{
    public static class MarkExtentions
    {

        public static IResponse Successfull(this IResponse response)
        {
            return response.Successfull(HttpStatusCode.OK);
        }
        public static IResponse Successfull(this IResponse response, HttpStatusCode httpStatusCode)
        {
            Response r = (Response)response;
            r.Valid = true;
            r.StatusCode = httpStatusCode;
            return response;
        }

        public static IResponse Failed(this IResponse response)
        {
            Response r = (Response)response;
            r.Valid = false;
            return response;
        }
        public static IResponse Failed(this IResponse response, string message)
        {
            Response r = (Response)response;
            r.Valid = false;
            r.Message = message;
            return response;
        }
        public static IResponse Failed(this IResponse response, HttpStatusCode httpStatusCode)
        {
            Response r = (Response)response;
            r.Valid = false;
            r.StatusCode = httpStatusCode;
            return response;
        }

        public static IResponse Failed(this IResponse response, string message, HttpStatusCode httpStatusCode)
        {
            Response r = (Response)response;
            r.Message = message;
            r.Valid = false;
            r.StatusCode = httpStatusCode;
            return response;
        }
    }
}
