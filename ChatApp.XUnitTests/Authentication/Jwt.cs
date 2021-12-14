using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Xunit;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace AuthenticationTests
{
    public class Create : TestBase
    {
        [Fact]
        public void Create_Jwt_Token()
        {
            User defaultUser = new User()
            {
                UserId = 1,
                Email = "Test@Test.com",
                UserName = "auto",
                PasswordHash = "12345"
            };

            var token = iJWTAuthService.GetToken(defaultUser);

            Assert.NotEqual(
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhdXRvIiwiVXNlcklkIjoiMSIsImV4cCI6MTYzOTQ4ODI2MCwiaXNzIjoiTG9jYWxEdWgiLCJhdWQiOiJMb2NhbER1aFVzZXIifQ.zDqFdEf3RLnFHsA-Py2ooDQ51w4i_fRVxhclYADLghU", 
                token.Token);
        }
    }
}
