using ChatApp.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatApp.Business.Core.Authentication
{
    public class JWTAuthService : IJWTAuthService
    {
        //https://codewithmukesh.com/blog/aspnet-core-api-with-jwt-authentication/
        //https://codewithmukesh.com/blog/refresh-tokens-in-aspnet-core/

        private readonly IConfiguration _configuration;

        public JWTAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticationModel GetToken(User user)
        {
            var authenticationModel = new AuthenticationModel();
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.UserId = user.UserId;
            authenticationModel.UserName = user.UserName;
            return authenticationModel;
        }
        private JwtSecurityToken CreateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("UserId", user.UserId.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
