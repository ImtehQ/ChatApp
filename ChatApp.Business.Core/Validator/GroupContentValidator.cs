using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Validator
{
    public static class GroupContentValidator
    {
        public static IResponse RegisterGroupName(string value)
        {
            if (value.Length <= 1)
                return Response.Error(ResponseCode.ValidatorNameInvalid);
            return Response.Successfull();
        }

        public static IResponse RegisterGroupPassword(string value)
        {
            if (value.Length <= 1)
                return Response.Error(ResponseCode.ValidatorNameInvalid);
            return Response.Successfull();
        }
    }
}
