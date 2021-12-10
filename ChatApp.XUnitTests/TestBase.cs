using ChatApp.Business.Core.AppServices;
using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Repositorys;
using ChatApp.Business.Core.Services;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Repositorys;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using Microsoft.Extensions.Options;
using Moq;

namespace ChatApp.XUnitTests
{
    public class TestBase
    {
        public IGroupService GroupService;
        public IUserService userService;
        public IGroupUserService groupUserService;
        public IMessageService messageService;
        public IInviteService inviteService;
        public IAppService appService;

        public Mock<IGroupRepository> groupRepository;
        public Mock<IUserRepository> userRepository;
        public Mock<IGroupUserRepository> userGroupRepository;
        public Mock<IMessageRepository> messageRepository;
        public Mock<IInviteRepository> inviteRepository;

        public TestBase()
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
    }
}
