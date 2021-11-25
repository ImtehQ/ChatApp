using ChatApp.Business.Core.Responses;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Enums.ResponseCodes;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Domain.Models;

namespace ChatApp.Business.Core.AppServices
{
    //base
    public partial class AppService : IAppService
    {
        IGroupService _GroupService { get; set; }
        IUserService _UserService { get; set; }
        IGroupUserService _GroupUserService { get; set; }
        IMessageService _MessageService { get; set; }
        IInviteService _InviteService { get; set; }


        public AppService(
            IGroupService GroupService,
            IUserService userService,
            IGroupUserService groupUserService,
            IMessageService messageService,
            IInviteService inviteService)
        {
            _GroupService = GroupService;
            _UserService = userService;
            _GroupUserService = groupUserService;
            _MessageService = messageService;
            _InviteService = inviteService;
        }
    }

    

    

    //GroupUser
    public partial class AppService : IAppService
    {

    }

    //Messages
    public partial class AppService : IAppService
    {

    }

    //Invites
    public partial class AppService : IAppService
    {

    }
}
