using ChatApp.Domain.Models;
using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace UserAppServiceTests
{
    public class Update : TestBase
    {

        [Fact]
        public void Block_User_Invalid_Id()
        {
            IResponse result = userService.BlockUserById(0);
            Assert.Equal("Invalid user Id", result.ReportMessage());
        }

        [Fact]
        public void Block_User_User_Not_Found()
        {
            IResponse result = userService.BlockUserById(5);
            Assert.Equal("User not found!", result.ReportMessage());
        }

        [Fact]
        public void Block_User_sucessfully()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);
            
            IResponse result = userService.BlockUserById(1);
            Assert.True(result.GetValid());
        }

        //-------------------------------------------

        [Fact]
        public void Update_User_sucessfully()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            User UpdateInfo = new User() { UserId = 1, Email = "Pannekoek@Test.com", UserName = "kaas", PasswordHash = "12345" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = userService.AccountUpdate(UpdateInfo.UserId, UpdateInfo.UserName, UpdateInfo.Email, UpdateInfo.PasswordHash);

            Assert.True(result.GetValid());
        }

        [Fact]
        public void Update_User_Invalid_Id()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            User UpdateInfo = new User() { UserId = 0, Email = "", UserName = "kaas", PasswordHash = "12345" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = userService.AccountUpdate(UpdateInfo.UserId, UpdateInfo.UserName, UpdateInfo.Email, UpdateInfo.PasswordHash);

            Assert.Equal("Invalid user Id", result.ReportMessage());
        }

        [Fact]
        public void Update_User_Invalid_Email()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            User UpdateInfo = new User() { UserId = 1, Email = "", UserName = "kaas", PasswordHash = "12345" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = userService.AccountUpdate(UpdateInfo.UserId, UpdateInfo.UserName, UpdateInfo.Email, UpdateInfo.PasswordHash);

            Assert.Equal("emailaddress to short", result.ReportMessage());
        }

        [Fact]
        public void Update_User_Invalid_Username()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            User UpdateInfo = new User() { UserId = 1, Email = "Test@Test.com", UserName = "", PasswordHash = "12345" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = userService.AccountUpdate(UpdateInfo.UserId, UpdateInfo.UserName, UpdateInfo.Email, UpdateInfo.PasswordHash);

            Assert.Equal("username to short", result.ReportMessage());
        }

        [Fact]
        public void Update_User_Invalid_Password()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            User UpdateInfo = new User() { UserId = 1, Email = "Test@Test.com", UserName = "Kass", PasswordHash = "" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = userService.AccountUpdate(UpdateInfo.UserId, UpdateInfo.UserName, UpdateInfo.Email, UpdateInfo.PasswordHash);

            Assert.Equal("password to short", result.ReportMessage());
        }
    }
}
