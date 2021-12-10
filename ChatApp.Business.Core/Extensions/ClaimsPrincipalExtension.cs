using System.Linq;
using System.Security.Claims;

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
