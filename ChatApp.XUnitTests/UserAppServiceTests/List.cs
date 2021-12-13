using ChatApp.Domain.Models;
using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using System.Collections.Generic;
using Xunit;
using ChatApp.Domain.Enums;

namespace UserAppServiceTests
{
    public class List : TestBase
    {
        [Fact]
        public void List_Anomimomus_Not_Allowed()
        {
            IResponse result = appService.ListUsers(null, (int)GroupTypeEnum.OptionGeneral);
            Assert.Equal("Anomimomus not allowed", result.ReportMessage());
        }

        [Fact]
        public void List_Invalid_GroupType()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            IResponse result = appService.ListUsers(defaultUser, 8);
            Assert.Equal("Invalid GroupTypeEnum value", result.ReportMessage());
        }

        [Fact]
        public void List_Successfuly_General()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = appService.ListUsers(defaultUser, 0);
            Assert.True(result.GetValid());
        }

        [Fact]
        public void List_Successfuly_Private()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = appService.ListUsers(defaultUser, 1);
            Assert.True(result.GetValid());
        }

        [Fact]
        public void List_Successfuly_Group()
        {
            User defaultUser = new User() { UserId = 1, Email = "Test@Test.com", UserName = "auto", PasswordHash = "12345" };
            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            IResponse result = appService.ListUsers(defaultUser, 2);
            Assert.True(result.GetValid());
        }
    }
}
