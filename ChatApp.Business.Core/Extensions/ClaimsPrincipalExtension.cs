using ChatApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetUserID(this ClaimsPrincipal principal)
        {
            if (principal.HasClaim(c => c.Type == "UserId"))
            {
                return System.Convert.ToInt32(
                    principal.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
            }
            return -1;
        }
    }
}
