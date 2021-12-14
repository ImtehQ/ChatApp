using ChatApp.XUnitTests;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Xunit;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Models;
using System.Collections.Generic;

namespace ChatApp.XUnitTests.GroupAppServiceTests
{
    public class Join : TestBase
    {
        [Fact]
        public void Group_Join_Successfuly()
        {
            IResponse response = this.CreateResponse();

            Group testGroup = new Group() { 
                GroupId = 1, 
                MaxUsers = 2, 
                Name = "partyRoomYay", 
                type = GroupTypeEnum.OptionGeneral, 
                VisibilityType = GroupVisibilityEnum.OptionPublic, 
                Password = "" };

            User defaultUser = new User() { 
                UserId = 1, 
                Email = "Test@Test.com", 
                UserName = "auto", 
                PasswordHash = "12345" };

            genericRepositoryGroup.CallBase = true;
            genericRepositoryGroup.Setup(x => x.GetById(1))
                .Returns(testGroup);

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            response.Include(appService.JoinGroupSelf(defaultUser, 1, ""));

            Assert.True(response.GetValid());
        }

        [Fact]
        public void Group_Join_Group_Not_Found()
        {
            IResponse response = this.CreateResponse();

            User defaultUser = new User()
            {
                UserId = 1,
                Email = "Test@Test.com",
                UserName = "auto",
                PasswordHash = "12345"
            };

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            response.Include(appService.JoinGroupSelf(defaultUser, 1, ""));

            Assert.Equal("Group not found", response.ReportMessage()); // Anomimomus not allowed
        }

        [Fact]
        public void Group_Join_Anomimomus_Not_Allowed()
        {
            IResponse response = this.CreateResponse();

            Group testGroup = new Group()
            {
                GroupId = 1,
                MaxUsers = 2,
                Name = "partyRoomYay",
                type = GroupTypeEnum.OptionGeneral,
                VisibilityType = GroupVisibilityEnum.OptionPublic,
                Password = ""
            };

            genericRepositoryGroup.CallBase = true;
            genericRepositoryGroup.Setup(x => x.GetById(1))
                .Returns(testGroup);

            response.Include(appService.JoinGroupSelf(null, 1, ""));

            Assert.Equal("Anomimomus not allowed", response.ReportMessage());
        }

        [Fact]
        public void Group_Join_Other_Successfuly()
        {
            IResponse response = this.CreateResponse();

            Group testGroup = new Group()
            {
                GroupId = 1,
                MaxUsers = 2,
                Name = "partyRoomYay",
                type = GroupTypeEnum.OptionGeneral,
                VisibilityType = GroupVisibilityEnum.OptionPublic,
                Password = ""
            };

            User defaultUser = new User()
            {
                UserId = 1,
                Email = "Test@Test.com",
                UserName = "auto",
                PasswordHash = "12345"
            };

            User defaultUser2 = new User()
            {
                UserId = 2,
                Email = "Test@Test.com",
                UserName = "fiets",
                PasswordHash = "12345"
            };

            genericRepositoryGroup.CallBase = true;
            genericRepositoryGroup.Setup(x => x.GetById(1))
                .Returns(testGroup);

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(2))
                .Returns(defaultUser2);

            response.Include(appService.JoinGroup(defaultUser, 1, defaultUser2.UserId, ""));

            Assert.True(response.GetValid());
        }
    }
}
