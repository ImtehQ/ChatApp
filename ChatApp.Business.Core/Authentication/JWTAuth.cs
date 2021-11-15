using ChatApp.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;
using System.Security.Cryptography;

namespace ChatApp.Business.Core.Authentication
{
    public class JWTAuth
    {
        public JWTAuth()
        {
            //https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
            //https://github.com/ruidfigueiredo/RefreshTokensWebApiExample
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12345678910111213141516"));

            var jwt = new JwtSecurityToken(
                issuer: "LocalHost Duh",
                audience: "Everyone",
                claims: new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("username", user.UserName)
                },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
