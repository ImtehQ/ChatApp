using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace ChatApp.API.MIP.HttpContextExtensions
{
    public static class HttpContextExtensions
    {
        public static User GetUser(this HttpContext client)
        {
            return (User)client.Items["User"];
        }
    }
}
