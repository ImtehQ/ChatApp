using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Extensions.Checks
{
    public static class CheckExtensions
    {
        public static IResponse CheckValidIfContentNotNull(this IResponse response)
        {
            if (response.Contents() != null)
                response.Status(true);
            else
                response.Status(false);
            return response;
        }

        public static IResponse CheckValidIfContentNull(this IResponse response)
        {
            if (response.Contents() == null)
                response.Status(true);
            else
                response.Status(false);
            return response;
        }
    }
}
