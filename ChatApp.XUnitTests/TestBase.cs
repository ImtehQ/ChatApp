using ChatApp.Business.Core.AppServices;
using ChatApp.Business.Core.Authentication;
using ChatApp.Business.Core.Services;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;
using Microsoft.Extensions.Configuration;
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

        public Mock<IGenericRepository<User>> genericRepositoryUser;
        public Mock<IGenericRepository<Group>> genericRepositoryGroup;
        public Mock<IGenericRepository<GroupUser>> genericRepositoryGroupUser;
        public Mock<IGenericRepository<Message>> genericRepositoryMessage;
        public Mock<IGenericRepository<Invite>> genericRepositoryInvite;

        public IJWTAuthService iJWTAuthService;

        public TestBase()
        {
            genericRepositoryUser = new Mock<IGenericRepository<User>>();
            genericRepositoryGroup = new Mock<IGenericRepository<Group>>();
            genericRepositoryGroupUser = new Mock<IGenericRepository<GroupUser>>();
            genericRepositoryMessage = new Mock<IGenericRepository<Message>>();
            genericRepositoryInvite = new Mock<IGenericRepository<Invite>>();

            var conf = new Mock<IConfiguration>();
            conf.SetupGet(x => x[It.Is<string>(s => s == "Jwt:key")]).Returns("C1CF4B7DC4C4175B6618DE4F55CA4");
            conf.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Issuer")]).Returns("LocalDuh");
            conf.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Audience")]).Returns("LocalDuhUser");
            conf.SetupGet(x => x[It.Is<string>(s => s == "Jwt:DurationInMinutes")]).Returns("30");
            //          "JWT": {
            //              "key": "C1CF4B7DC4C4175B6618DE4F55CA4",
            //  "Issuer": "LocalDuh",
            //  "Audience": "LocalDuhUser",
            //  "DurationInMinutes": 30
            //}

            var JWT = new Mock<IOptions<JWTToken>>();

            iJWTAuthService = new JWTAuthService(conf.Object);

            

            userService = new UserService(genericRepositoryUser.Object, JWT.Object, iJWTAuthService);
            GroupService = new GroupService(genericRepositoryGroup.Object);
            groupUserService = new GroupUserService(genericRepositoryGroupUser.Object);
            messageService = new MessageService(genericRepositoryMessage.Object);
            inviteService = new InviteService(genericRepositoryInvite.Object);

            appService = new AppService(GroupService, userService, groupUserService, messageService, inviteService);
        }
    }
}
