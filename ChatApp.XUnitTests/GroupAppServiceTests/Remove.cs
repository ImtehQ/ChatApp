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
    public class Remove : TestBase
    {
        [Fact]
        public void Remove_Group_Successfully()
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

            response.Include(appService.RemoveGroup(1));

            Assert.True(response.GetValid());
        }

        [Fact]
        public void Remove_Group_Self_User_Successfully()
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

            GroupUser groupUser = new GroupUser() { Id = 1, Group = testGroup, User = defaultUser, AccountRole = AccountRoleEnum.RoleModerator };

            genericRepositoryGroupUser.CallBase = true;
            genericRepositoryGroupUser.Setup(x => x.GetById(1))
                .Returns(groupUser);

            response.Include(appService.RemoveSelfFromGroup(defaultUser, 1));

            Assert.True(response.GetValid());
        }

        [Fact]
        public void Remove_Group_Other_User_Successfully()
        {
            IResponse response = this.CreateResponse();

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
            genericRepositoryUser.CallBase = true;
            genericRepositoryUser.Setup(x => x.GetById(1))
                .Returns(defaultUser);
            genericRepositoryUser.Setup(x => x.GetById(2))
                .Returns(defaultUser2);

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

            GroupUser groupUser = new GroupUser() { Id = 1, Group = testGroup, User = defaultUser, AccountRole = AccountRoleEnum.RoleModerator };
            GroupUser groupUser2 = new GroupUser() { Id = 2, Group = testGroup, User = defaultUser2, AccountRole = AccountRoleEnum.RoleUser };

            genericRepositoryGroupUser.CallBase = true;
            genericRepositoryGroupUser.Setup(x => x.GetAll())
                .Returns(new List<GroupUser>() { groupUser, groupUser2 });

            response.Include(appService.RemoveOtherUserFromGroup(defaultUser,defaultUser2.UserId, 1));

            Assert.True(response.GetValid());
        }
    }
}
