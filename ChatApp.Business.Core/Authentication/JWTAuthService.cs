using ChatApp.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Core.Authentication
{
    public class JWTAuthService : IJWTAuthService
    {
        //https://codewithmukesh.com/blog/aspnet-core-api-with-jwt-authentication/
        //https://codewithmukesh.com/blog/refresh-tokens-in-aspnet-core/

        public AuthenticationModel GetToken(User user, JWTToken _jwt)
        {
            var authenticationModel = new AuthenticationModel();
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(user, _jwt);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.UserId = user.UserId;
            authenticationModel.UserName = user.UserName;
            return authenticationModel;
        }
        private JwtSecurityToken CreateJwtToken(User user, JWTToken _jwt)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("UserId", user.UserId.ToString()),
            };

            var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
