using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace ChatApp.Business.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        AccountRoleEnum accountRoleRequired;

        public AuthorizeAttribute(AccountRoleEnum accountRole)
        {
            accountRoleRequired = accountRole;
        }

        private static bool HasAllowAnonymous(AuthorizationFilterContext context)
        {
            var filters = context.Filters;
            return filters.OfType<IAllowAnonymousFilter>().Any();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (HasAllowAnonymous(context))
                return;

            var account = (User)context.HttpContext.Items["User"];

            if (account == null)
            {
                context.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (account.Role >= accountRoleRequired)
                {
                    return;
                }
                else
                {
                    context.Result = new JsonResult(string.Empty) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}