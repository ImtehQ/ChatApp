using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Domain.Interfaces.Services;

namespace ChatApp.API.MIP
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var accountId = jwtToken.Claims.First(x => x.Type == "UserId").Value;

                    if (string.IsNullOrEmpty(accountId) == false)
                    {
                        // attach account to context on successful jwt validation
                        context.Items["User"] = userService.GetUserById(Convert.ToInt32(accountId));
                    }
                }
                catch
                {
                    // do nothing if jwt validation fails
                    // account is not attached to context so request won't have access to secure routes
                }
            }

            await _next(context);
        }
    }
}
