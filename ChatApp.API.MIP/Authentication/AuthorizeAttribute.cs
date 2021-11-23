using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace ChatApp.Business.Core.Authentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        AccountRoleEnum accountRoleRequired;
        bool isGroupRole = false;

        public AuthorizeAttribute(AccountRoleEnum accountRole, bool groupRole = false)
        {
            accountRoleRequired = accountRole;
            isGroupRole = groupRole;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
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
            }
        }
    }
}