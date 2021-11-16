using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
namespace ChatApp.Domain.Attributes
{
    internal class AuthRoleAttribute : AuthorizeAttribute
    {
        //https://www.c-sharpcorner.com/article/jwt-validation-and-authorization-in-net-5-0/
        const string POLICY_PREFIX = "AccountRole";

        public AuthRoleAttribute(AccountRoleEnum role) => Role = role;

        public AccountRoleEnum Role
        {
            get
            {
                if(  )
                if (int.TryParse(Policy.Substring(POLICY_PREFIX.Length), out var _role))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value.ToString()}";
            }
        }
    }
}