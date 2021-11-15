using ChatApp.Business.Core.Responses;
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
        public static ValidatorResponse IsValidName(string name)
        {
            if (name.Length <= 1)
                return ValidatorResponse.Error("Name is to short");
            return ValidatorResponse.Successfull();
        }

        public static ValidatorResponse IsValidUsername(string username)
        { 
            if (username.Length < 8)
                return ValidatorResponse.Error("Username is to short");
            return ValidatorResponse.Successfull();
        }

        public static ValidatorResponse IsValidPassword(string password)
        {
            if (password.Length < 8)
                return ValidatorResponse.Error("Password is to short");
            return ValidatorResponse.Successfull();
        }

        public static ValidatorResponse IsValidEmailAddress(string email)
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
                return ValidatorResponse.Error(e.Message);
            }
            catch (ArgumentException e)
            {
                return ValidatorResponse.Error(e.Message);
            }

            try
            {
                if (Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)) == true)
                    return ValidatorResponse.Successfull();

            }
            catch (RegexMatchTimeoutException)
            {
                return ValidatorResponse.Error("RegexMatchTimeoutException");
            }

            return ValidatorResponse.Error("Not valid email address");
        }
    }
}
