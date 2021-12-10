using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.MarkExtentions;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;

namespace ChatApp.Business.Core.Validator
{
    public static class UserContentValidator
    {
        public static IResponse RegisterName(string value)
        {
            IResponse response = InitializeExtensions.CreateResponse(typeof(UserContentValidator));
            if (value.Length <= 1)
                return response.Failed();
            return response.Successfull();
        }

        public static IResponse RegisterUsername(string value)
        {
            IResponse response = InitializeExtensions.CreateResponse(typeof(UserContentValidator));
            if (value.Length <= 1)
                return response.Failed();
            return response.Successfull();
        }

        public static IResponse RegisterPassword(string value)
        {
            IResponse response = InitializeExtensions.CreateResponse(typeof(UserContentValidator));
            if (value.Length <= 1)
                return response.Failed();
            return response.Successfull();
        }

        public static IResponse RegisterEmailAddress(string value)
        {
            IResponse response = InitializeExtensions.CreateResponse(typeof(UserContentValidator));
            if (value.Length <= 1)
                return response.Failed();
            return response.Successfull();
        }
    }
}
