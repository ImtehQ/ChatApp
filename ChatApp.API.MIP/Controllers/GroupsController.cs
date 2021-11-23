using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Business.Core.Authentication;
using System.Linq;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Models;
using ChatApp.Domain.Interfaces.Services;
using System.Collections.Generic;
using ChatApp.Business.Core.Extensions;
using ChatApp.Business.Core.Services;

namespace ChatApp.API.MIP.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        IGroupService _GroupService { get; set; }
        IUserService _UserService { get; set; }
        IGroupUserService _GroupUserService { get; set; }
        IMessageService _MessageService { get; set; }
        IInviteService _InviteService { get; set; }

        public GroupsController(
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

        [HttpGet]
        [Route("list")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult List(int GroupId)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID());

            List<Group> groups = _GroupUserService.GetGroupsByUser(user);
                return Ok(groups);
     
            return Ok("Reponse not implemented");
        }

        [HttpPost]
        [Route("groups")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult Register(string Name, string Password, int MaxUsers = 0, 
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic, 
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID());
            Group group = _GroupService.Create(Name, Password, MaxUsers, Visibility, GroupType);

            _GroupUserService.Insert(user, group, AccountRoleEnum.RoleAdmin);

            return Ok("Reponse not implemented");
        }


        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult Invite(int inviteId)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID());

            Invite invite = _InviteService.GetInviteById(inviteId);
            if (invite == null)
                return BadRequest("Invite not valid");

            Group group = _GroupService.GetGroupById(invite.GroupId);

            if(group == null)
                return BadRequest("Invite not valid");

            _GroupUserService.Insert(user, group, AccountRoleEnum.RoleUser);

            return Ok();
        }

        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleModerator, true)]
        public IActionResult Join(int GroupId, int UserId, string Message)
        {
            User User = _UserService.GetUserById(UserId);

            User senderUser = _UserService.GetUserById(HttpContext.User.GetUserID());

            Group group = _GroupService.Create("Invite chat", "", 2, GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate);

            _GroupUserService.Join(group, senderUser, AccountRoleEnum.RoleAdmin);

            _GroupUserService.Join(group, User, AccountRoleEnum.RoleUser);

            Invite invite = new Invite() { GroupId = group.GroupId, Message = Message };

            _InviteService.Register(invite);

            _MessageService.SendMessage(Message, senderUser, GroupTypeEnum.OptionPrivate, group.GroupId);
            _MessageService.SendMessage($"Invite: {invite.Id}", senderUser, GroupTypeEnum.OptionPrivate, group.GroupId);

            return Ok("Not implimantata");
        }

        [HttpPost]
        [Route("groups/remove")]
        [Authorize(AccountRoleEnum.RoleModerator, true)]
        public IActionResult RemoveGroup(int GroupId)
        {
            Group group = _GroupService.GetGroupById(GroupId);
            if (group == null)
                return NotFound("Group");

            _GroupUserService.RemoveGroup(group);
            _GroupService.RemoveGroup(group.GroupId);

            return Ok();
        }

        [HttpPost]
        [Route("groups/remove")]
        [Authorize(AccountRoleEnum.RoleModerator, true)]
        public IActionResult RemoveUser(int userId, int GroupId)
        {
            Group group = _GroupService.GetGroupById(GroupId);
            if (group == null)
                return NotFound("Group");

            User user = _UserService.GetUserById(userId);
            if(user == null)
                return NotFound("User");

            _GroupUserService.RemoveUser(user, group);

            return Ok();
        }
    }

}
