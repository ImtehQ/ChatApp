using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;

namespace ChatApp.Business.Core.Validator
{
    public static class UserContentValidator
    {
        public static IResponse RegisterName(string name)
        {
            IResponse response = new Response(MethodCode.Register, LayerCode.Validator, name);

            if (name.Length <= 1)
                return response.Failed(System.Net.HttpStatusCode.BadRequest, "Length <= 1");
            return response.Successfull();
        }

        public static IResponse RegisterUsername(string username)
        {
            IResponse response = new Response(MethodCode.Register, LayerCode.Validator, username);

            if (username.Length < 8)
                return response.Failed(System.Net.HttpStatusCode.BadRequest, "Length <= 1");
            return response.Successfull();
        }

        public static IResponse RegisterPassword(string password)
        {
            IResponse response = new Response(MethodCode.Register, LayerCode.Validator, password);

            if (password.Length < 8)
                return response.Failed(System.Net.HttpStatusCode.BadRequest, "Length <= 1");
            return response.Successfull();
        }

        public static IResponse RegisterEmailAddress(string email)
        {
            IResponse response = new Response(MethodCode.Register, LayerCode.Validator, email);
            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest, e.Message);
            }
            catch (ArgumentException e)
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest, e.Message);
            }

            try
            {
                if (Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) == true)
                    return response.Successfull(System.Net.HttpStatusCode.Accepted);

            }
            catch (RegexMatchTimeoutException)
            {
                return response.Failed(System.Net.HttpStatusCode.BadRequest, "RegexMatchTimeoutException");
            }

            return response.Failed(System.Net.HttpStatusCode.BadRequest, "RegexMatchTimeoutException");
        }
    }
}
