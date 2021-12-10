using ChatApp.Domain.Models;
using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace UserAppServiceTests
{
    public class Create : TestBase
    {
        [Fact]
        public void Name_Length_is_ToShort()
        {
            IResponse result = userService.Register("a", "Username", "E@mail.com", "password");
            Assert.Equal("name to short", result.ReportMessage());
        }

        [Fact]
        public void Username_Length_is_ToShort()
        {
            IResponse result = userService.Register("firstname", "a", "E@mail.com", "password");
            Assert.Equal("username to short", result.ReportMessage());
        }

        [Fact]
        public void EmailAddress_Length_is_ToShort()
        {
            IResponse result = userService.Register("firstname", "Username", "a", "password");
            Assert.Equal("emailaddress to short", result.ReportMessage());
        }

        [Fact]
        public void Password_Length_is_ToShort()
        {
            IResponse result = userService.Register("firstname", "Username", "E@mail.com", "a");
            Assert.Equal("password to short", result.ReportMessage());
        }

        [Fact]
        public void Name_Is_Null()
        {
            IResponse result = appService.RegisterUser(null, "Username", "E@mail.com", "password");
            Assert.Equal("name is empty", result.ReportMessage());
        }

        [Fact]
        public void Username_Is_Null()
        {
            IResponse result = appService.RegisterUser("firstname", null, "E@mail.com", "password");
            Assert.Equal("username is empty", result.ReportMessage());
        }

        [Fact]
        public void EmailAddress_Is_Null()
        {
            IResponse result = appService.RegisterUser("firstname", "Username", null, "password");
            Assert.Equal("emailaddress is empty", result.ReportMessage());
        }

        [Fact]
        public void Password_Is_Null()
        {
            IResponse result = appService.RegisterUser("firstname", "Username", "E@mail.com", null);
            Assert.Equal("password is empty", result.ReportMessage());
        }

        [Fact]
        public void Email_Already_Exists()
        {
            userRepository.CallBase = true;
            userRepository.Setup(x => x.GetUsers())
                .Returns(new List<User>() { new User() { Email = "Test@Test.com" } });

            IResponse result = userService.Register("Name", "Username", "Test@Test.com", "123456789");

            Assert.Equal("emailaddress already exists", result.ReportMessage());
        }

        [Fact]
        public void Should_Return_Last_Invalid_When_Username_Already_Exists()
        {
            userRepository.CallBase = true;
            userRepository.Setup(x => x.GetUsers())
                .Returns(new List<User>() { new User() { UserName = "usernameBob" } });

            IResponse result = userService.Register("Name", "usernameBob", "Test@Test.com", "123456789");

            Assert.Equal("password already exists", result.ReportMessage());
        }

        [Fact]
        public void Should_Return_Valid_When_User_Is_Registered()
        {
            userRepository.CallBase = true;
            userRepository.Setup(x => x.GetUsers())
                .Returns(new List<User>() { new User() { UserName = "TestUserName" } });

            IResponse response = this.CreateResponse()
                .Includes(appService.RegisterUser("Name", "Test2UserName", "goodTest@Test2.com", "123456789"));

            Assert.True(response.LastIncluded().Status());
        }
    }
}
