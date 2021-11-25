using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Business.Domain.Responses;

namespace ChatApp.Business.Core.Validator
{
    public static class GroupContentValidator
    {
        public static IResponse RegisterGroupName(string value)
        {
            IResponse response = new Bfet(MethodCode.Register, LayerCode.Validator, value);
            if (value.Length <= 1)
                return response.Failed(System.Net.HttpStatusCode.BadGateway, "Length <= 1");
            return response.Successfull(System.Net.HttpStatusCode.Accepted);
        }

        public static IResponse RegisterGroupPassword(string value)
        {
            IResponse response = new Bfet(MethodCode.Register, LayerCode.Validator, value);
            if (value.Length <= 1)
                return response.Failed(System.Net.HttpStatusCode.BadGateway, "Length <= 1");
            return response.Successfull(System.Net.HttpStatusCode.Accepted);
        }
    }
}
