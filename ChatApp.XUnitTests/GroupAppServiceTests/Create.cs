using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Xunit;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace GroupAppServiceTests
{
    public class Create : TestBase
    {
        [Fact]
        public void Group_Name_To_Short()
        {
            IResponse response = this.CreateResponse();
            response.Include(GroupService.Register("", ""));

            Assert.Equal("name is invalid", response.ReportMessage());
        }

        [Fact]
        public void Group_Invalid_MaxUsers()
        {
            IResponse response = this.CreateResponse();
            response.Include(GroupService.Register("appelmoes", "", 0));

            Assert.Equal("maxUsers can't be below 2", response.ReportMessage());
        }

        [Fact]
        public void Group_Register_Successfully()
        {
            var response = this.CreateResponse();
            response.Include(GroupService.Register(
                "Applemoes", "", 5, GroupVisibilityEnum.OptionPublic, GroupTypeEnum.OptionGroup));
            Assert.True(response.GetValid());
        }

        [Fact]
        public void Group_RegisterGroup_Successfully()
        {
            var response = this.CreateResponse();

            User defaultUser = new User() { UserId = 0, Email = "Test@Test.com" };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetAll())
                .Returns(new List<User>() { defaultUser });

            response.Include(appService.RegisterGroup(
                defaultUser, "Applemoes", "", 5, GroupVisibilityEnum.OptionPublic, GroupTypeEnum.OptionGroup));
            Assert.True(response.GetValid());
        }
    }
}
