using FluentResponses.Interfaces;
using FluentResponses.TraceExtensions;
using System.Net;

namespace FluentResponses.Extensions.Reports
{
    public static class ReportExtentions
    {
        public static string ReportFullDetails(this IResponse response, bool CheckIfValid = true)
        {
            if (CheckIfValid)
            {
                if (response.Status() == true)
                {
                    return response.GenerateFullDetailedReport();
                }
            }
            else
            {
                return response.GenerateFullDetailedReport();
            }
            return null;
        }

        public static string ReportMessage(this IResponse response)
        {
            return response.ContentMessage();
        }

        private static string GenerateFullDetailedReport(this IResponse response)
        {
            string responseParameters = "";
            foreach (var item in response.Parameters)
            {
                responseParameters += $"[{item.ParameterType}]({item.Name})";
            }
            string responseString = "";
            if (response.Source != null)
                responseString += $"Source:{response.Source.FullName},";
            if (response.Invoker != null)
                responseString += $"Invoker:{response.Invoker.Name}," +
                $"{responseParameters}";

            response.Report(responseString);
            return response.Report();
        }

        public static bool ReportStatus(this Response response, bool FalloutIfFalse, bool defaultReturnValue = true)
        {
            if (response.Status() == false && FalloutIfFalse)
                return false;
            if (response.Status() == true && FalloutIfFalse == false)
                return true;

            foreach (var item in response.Includes())
            {
                if (item.Status() == false && FalloutIfFalse)
                    return false;
                if (item.Status() == true && FalloutIfFalse == false)
                    return true;
            }
            return defaultReturnValue;
        }


        public static IResponse Return(this IResponse response,
            HttpStatusCode httpStatusSuccessfull = HttpStatusCode.OK,
            HttpStatusCode httpStatusFailed = HttpStatusCode.BadRequest)
        {
            return response.ReturnCheckStatus(response.Status().Value, httpStatusSuccessfull, httpStatusFailed);
        }

        public static IResponse ReturnCheckStatus(this IResponse response, bool check,
           HttpStatusCode httpStatusSuccessfull = HttpStatusCode.OK,
           HttpStatusCode httpStatusFailed = HttpStatusCode.BadRequest)
        {
            if (check == true)
                return response.Successfull(httpStatusSuccessfull);
            else
                return response.Failed(httpStatusFailed);
        }

        public static IResponse ReturnCheckStatusLast(this IResponse response,
           HttpStatusCode httpStatusSuccessfull = HttpStatusCode.OK,
           HttpStatusCode httpStatusFailed = HttpStatusCode.BadRequest)
        {
            return response.ReturnCheckStatus(response.LastIncluded().Status().Value,
                httpStatusSuccessfull,
                httpStatusFailed);
        }

        public static IResponse ReturnCheckStatusAll(this IResponse response,
   HttpStatusCode httpStatusSuccessfull = HttpStatusCode.OK,
   HttpStatusCode httpStatusFailed = HttpStatusCode.BadRequest)
        {
            return response.ReturnCheckStatus(response.TraceValid(),
                httpStatusSuccessfull,
                httpStatusFailed);
        }

        public static IResponse Successfull(this IResponse response, HttpStatusCode httpStatus = HttpStatusCode.OK)
        {
            return response.Status(true).Code(httpStatus);
        }

        public static IResponse Failed(this IResponse response)
        {
            if (response.LastIncluded() != null)
                return response.Status(false).Code(response.LastIncluded().Code());
            return response.Status(false);
        }
        public static IResponse Failed(this IResponse response, object failedContent, HttpStatusCode httpStatus = HttpStatusCode.BadRequest)
        {
            return response.Status(false).Contents(failedContent).Code(httpStatus);
        }

        public static IResponse Failed(this IResponse response, string message, HttpStatusCode httpStatus = HttpStatusCode.BadRequest)
        {
            return response.Status(false).ContentMessage(message).Code(httpStatus);
        }
    }
}
