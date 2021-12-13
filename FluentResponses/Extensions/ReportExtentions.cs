using FluentResponses.Interfaces;

namespace FluentResponses.Extensions.Reports
{
    public static class ReportExtentions
    {
        public static string ReportMessage(this IResponse response)
        {
            Response r = (Response)response;
            return r.Message;
        }
    }
}
