using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.TraceExtensions
{
    public static class TraceExtensions
    {
        public static string FullTrace(this IResponse response)
        {
            return "Nothing here yet!";
        }

        public static List<HttpStatusCode> TraceCodes(this IResponse response)
        {
            List<HttpStatusCode> codes = new List<HttpStatusCode>();
            var responses = response.Includes();
            if (responses.Count > 0)
            {
                codes.Add(response.Code());
                foreach (var item in responses)
                {
                    codes.AddRange(item.TraceCodes());
                }
            }
            return codes;
        }

        public static List<T> TraceContents<T>(this IResponse response)
        {
            List<T> traceContents = new List<T>();
            var responses = response.Includes();
            if (responses.Count > 0)
            {
                if(response.Contents().GetType() == typeof(T))
                    traceContents.Add((T)response.Contents());
                foreach (var item in responses)
                {
                    traceContents.AddRange(item.TraceContents<T>());
                }
            }
            return traceContents;
        }
    }
}
