using FluentResponses.Interfaces;
using FluentResponses.Models;
using System.Linq;
using System.Net;

namespace FluentResponses.Extensions.Reports
{
    public static class ReportExtentions
    {
        public static string ReportFullDetails(this Response response)
        {
            string responseParameters = "";
            foreach (var item in response.Parameters)
            {
                responseParameters += $"[{item.ParameterType}]({item.Name})";
            }
            response.Report($"" +
                $"Source:{response.Source.FullName}," +
                $"Invoker:{response.Invoker.Name}," +
                $"{responseParameters}");

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

        public static IResponse Successfull(this IResponse response, HttpStatusCode httpStatus = HttpStatusCode.OK)
        {
            return response.Status(true).Code(httpStatus);
        }

        public static IResponse Failed(this IResponse response)
        {
            return response.Status(false).Code(response.LastIncluded().Code());
        }
        public static IResponse Failed(this IResponse response, object failedContent, HttpStatusCode httpStatus = HttpStatusCode.BadRequest)
        {
            return response.Status(false).Contents(failedContent).Code(httpStatus);
        }
    }
}
