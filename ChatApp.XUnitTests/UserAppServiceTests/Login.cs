using ChatApp.Domain.Models;
using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace UserAppServiceTests
{
    public class Login : TestBase
    {
        [Fact]
        public void Login_User_userName_ToShort()
        {
            IResponse result = appService.LoginUser("", "password");
            Assert.Equal("Username is empty", result.ReportMessage());
        }

        [Fact]
        public void Login_User_Password_ToShort()
        {
            IResponse result = appService.LoginUser("DefoNotBob", "");
            Assert.Equal("Password is empty", result.ReportMessage());
        }

        [Fact]
        public void Login_User_Not_Exist()
        {
            IResponse result = appService.LoginUser("DefoNotBob", "password123");
            Assert.Equal("user not found", result.ReportMessage());
        }

        [Fact]
        public void Login_User_Successfully()
        {
            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetAll())
                .Returns(new List<User>() { new User() { UserName = "DefoNotBob", PasswordHash = "bH/kVwshHUKEPR2o0rwhI5GHbVnAn7Xu+WWSffP8HAA=" } });

            IResponse result = appService.LoginUser("DefoNotBob", "password123");
            Assert.True(result.GetValid());
        }
    }
}
