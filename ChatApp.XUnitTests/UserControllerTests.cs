using ChatApp.Business.Core.AppServices;
using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Repositorys;
using ChatApp.Business.Core.Services;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ChatApp.XUnitTests
{
    public class UserControllerTests
    {
        User user;
        GroupTypeEnum groupType;

        IGroupService GroupService;
        IUserService userService;
        IGroupUserService groupUserService;
        IMessageService messageService;
        IInviteService inviteService;
        IAppService appService;

        Mock<IGroupRepository> groupRepository;
        Mock<IUserRepository> userRepository;
        Mock<IGroupUserRepository> userGroupRepository;
        Mock<IMessageRepository> messageRepository;
        Mock<IInviteRepository> inviteRepository;

        private void GetMocks()
        {
            groupRepository = new Mock<IGroupRepository>();
            userRepository = new Mock<IUserRepository>();
            userGroupRepository = new Mock<IGroupUserRepository>();
            messageRepository = new Mock<IMessageRepository>();
            inviteRepository = new Mock<IInviteRepository>();

            var iJWTAuthService = new Mock<IJWTAuthService>();

            var JWT = new Mock<IOptions<JWTToken>>();

            GroupService = new GroupService(groupRepository.Object);
            userService = new UserService(userRepository.Object, JWT.Object, iJWTAuthService.Object);
            groupUserService = new GroupUserService(userGroupRepository.Object);
            messageService = new MessageService(messageRepository.Object);
            inviteService = new InviteService(inviteRepository.Object);

            appService = new AppService(GroupService, userService, groupUserService, messageService, inviteService);
        }

        [Theory]
        [InlineData(0)]
        public void GetUserById(int id)
        {
            GetMocks();

            IResponse response = this.CreateResponse();
            userRepository.CallBase = true;
            userRepository.Setup(x => x.GetUserByID(id)).Returns(new User() { UserName = "testUser" });

            response.Include(userService.GetUserById(id));
            User testUser = response.GetAttachment<User>();

            Assert.Equal("testUser", testUser.UserName);
        }
    }
}
