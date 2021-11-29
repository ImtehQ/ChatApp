using FluentResponses.Interfaces;
using FluentResponses.Models;
using System.Linq;
using System.Net;

namespace FluentResponses.Extensions.Summary
{
    public static class ResponseSummaryExtentions
    {
        public static string ReportToString(this Report report)
        {
            return report.ToString();
        }

        public static bool SumResultIsValid(this IResponse response)
        {
            if (response.isNotValid && response.Includes.Any(x => x.IsValid == false))
                return false;
            return true;
        }
        public static bool SumResultIsNotValid(this IResponse response)
        {
            return !response.SumResultIsValid();
        }

        public static string[] SumResultReports(this IResponse response)
        {
            string[] Reports = new string[response.Includes.Count + 1];
            Reports = response.Includes.Select(x => x.Report).ToArray();
            Reports[Reports.Length - 1] = response.Report;
            return Reports;
        }

        public static HttpStatusCode[] SumResultCodes(this IResponse response)
        {
            HttpStatusCode[] Codes = new HttpStatusCode[response.Includes.Count + 1];
            Codes = response.Includes.Select(x => x.Code).ToArray();
            Codes[Codes.Length - 1] = response.Code;
            return Codes;
        }
    }
}
