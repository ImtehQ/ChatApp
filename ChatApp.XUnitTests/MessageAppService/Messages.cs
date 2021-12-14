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
    public class Messages : TestBase
    {
        [Fact]
        public void Get_Messages_Successfully()
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
                UserName = "auto",
                PasswordHash = "12345"
            };


            GroupUser groupUser = new GroupUser()
            {
                Id = 1,
                Group = testGroup,
                User = defaultUser,
                AccountRole = AccountRoleEnum.RoleModerator
            };
            GroupUser groupUser2 = new GroupUser()
            {
                Id = 2,
                Group = testGroup,
                User = defaultUser2,
                AccountRole = AccountRoleEnum.RoleUser
            };

            Message message = new Message()
            {
                MessageId = 1,
                Content = "Hello world 1",
                GroupId = 1,
                SenderId = defaultUser
            };

            Message message2 = new Message()
            {
                MessageId = 2,
                Content = "Hello world 2",
                GroupId = 1,
                SenderId = defaultUser2
            };

            genericRepositoryUser.CallBase = true;
            genericRepositoryGroup.CallBase = true;
            genericRepositoryGroupUser.CallBase = true;
            genericRepositoryMessage.CallBase = true;

            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            genericRepositoryUser.Setup(x => x.GetById(2))
                .Returns(defaultUser2);

            genericRepositoryGroup.Setup(x => x.GetById(1))
                .Returns(testGroup);

            genericRepositoryGroupUser.Setup(x => x.GetAll())
                .Returns(new List<GroupUser>() { groupUser, groupUser2 });

            genericRepositoryMessage.Setup(x => x.GetAll())
    .Returns(new List<Message>() { message, message2 });

            response.Include(appService.PullMessages(1, 1));

            Assert.True(response.GetValid());
        }
        [Fact]
        public void Send_Message_Successfully()
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
                UserName = "auto",
                PasswordHash = "12345"
            };


            GroupUser groupUser = new GroupUser()
            {
                Id = 1,
                Group = testGroup,
                User = defaultUser,
                AccountRole = AccountRoleEnum.RoleModerator
            };
            GroupUser groupUser2 = new GroupUser()
            {
                Id = 2,
                Group = testGroup,
                User = defaultUser2,
                AccountRole = AccountRoleEnum.RoleUser
            };


            genericRepositoryUser.CallBase = true;
            genericRepositoryGroup.CallBase = true;
            genericRepositoryGroupUser.CallBase = true;

            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);

            genericRepositoryUser.Setup(x => x.GetById(2))
                .Returns(defaultUser2);

            genericRepositoryGroup.Setup(x => x.GetById(1))
                .Returns(testGroup);

            genericRepositoryGroupUser.Setup(x => x.GetAll())
                .Returns(new List<GroupUser>() { groupUser, groupUser2 });


            response.Include(appService.SendMessage("Hello world!", defaultUser, 0, 1));

            Assert.True(response.GetValid());
        }
    }
}
