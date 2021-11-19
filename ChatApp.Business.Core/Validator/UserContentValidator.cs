using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Validator
{
    public static class UserContentValidator
    {
        public static Response RegisterName(string name)
        {
            if (name.Length <= 1)
                return Response.Error(ResponseCode.ValidatorNameInvalid);
            return Response.Successfull();
        }

        public static Response RegisterUsername(string username)
        { 
            if (username.Length < 8)
                return Response.Error(ResponseCode.ValidatorUserNameInvalid);
            return Response.Successfull();
        }

        public static Response RegisterPassword(string password)
        {
            if (password.Length < 8)
                return Response.Error(ResponseCode.ValidatorPasswordInvalid);
            return Response.Successfull();
        }

        public static Response RegisterEmailAddress(string email)
        {
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
                return Response.Error(ResponseCode.ValidatorEmailInvalid, e.Message);
            }
            catch (ArgumentException e)
            {
                return Response.Error(ResponseCode.ValidatorEmailInvalid, e.Message);
            }

            try
            {
                if (Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) == true)
                    return Response.Successfull();

            }
            catch (RegexMatchTimeoutException)
            {
                return Response.Error(ResponseCode.ValidatorEmailInvalid ,"RegexMatchTimeoutException");
            }

            return Response.Error(ResponseCode.ValidatorEmailInvalid, "Not valid email address");
        }
    }
}
