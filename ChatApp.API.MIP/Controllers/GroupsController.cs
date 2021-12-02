using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Models;
using ChatApp.Domain.Interfaces.Services;
using System.Collections.Generic;
using ChatApp.Business.Core.Extensions;
using FluentResponses.Interfaces;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;

namespace ChatApp.API.MIP.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        IAppService _appService;

        public GroupsController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("list")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult List(int GroupId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.GetUserById(HttpContext.User.GetUserID());
            User user = response.LastIncluded().Contents<User>();
            response.Includes(_appService.GetGroupsByUser(user));
            response.Successfull();
            return Ok();
        }

        [HttpPost]
        [Route("groups")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult Register(string Name, string Password, int MaxUsers = 0,
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID()).GetResponseObject<User>();
            Group group = _GroupService.Create(Name, Password, MaxUsers, Visibility, GroupType).GetResponseObject<Group>();

            _GroupUserService.Insert(user, group, AccountRoleEnum.RoleAdmin);

            return Ok("Reponse not implemented");
        }


        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult Invite(int inviteId)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID()).GetResponseObject<User>();

            Invite invite = _InviteService.GetInviteById(inviteId).GetResponseObject<Invite>();
            if (invite == null)
                return BadRequest("Invite not valid");

            Group group = _GroupService.GetGroupById(invite.GroupId).GetResponseObject<Group>();

            if (group == null)
                return BadRequest("Invite not valid");

            _GroupUserService.Insert(user, group, AccountRoleEnum.RoleUser);

            return Ok();
        }

        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleUser)] // Missing group id so no idea where you need to be rolemod for...
        public IActionResult Join(int GroupId, int UserId, string Message)
        {
            return appService.Join(int GroupId, int UserId, string Message);

            User User = _UserService.GetUserById(UserId).GetResponseObject<User>();

            User senderUser = _UserService.GetUserById(HttpContext.User.GetUserID()).GetResponseObject<User>();

            Group group = _GroupService.Create("Invite chat", "", 2,
                GroupVisibilityEnum.OptionPrivate, GroupTypeEnum.OptionPrivate).GetResponseObject<Group>();

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
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult RemoveGroup(int GroupId)
        {
            Group group = _GroupService.GetGroupById(GroupId).GetResponseObject<Group>();
            if (group == null)
                return NotFound("Group");

            _GroupUserService.RemoveGroup(group);
            _GroupService.RemoveGroup(group.GroupId);

            return Ok();
        }

        [HttpPost]
        [Route("groups/remove")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult RemoveUser(int userId, int GroupId)
        {
            Group group = _GroupService.GetGroupById(GroupId).GetResponseObject<Group>();
            if (group == null)
                return NotFound("Group");

            User user = _UserService.GetUserById(userId).GetResponseObject<User>();
            if (user == null)
                return NotFound("User");

            if ((int)_GroupUserService.GetAccountRoleByUser(user, group)
                .GetResponseObject<AccountRoleEnum>() < (int)AccountRoleEnum.RoleModerator)
            {
                return StatusCode(404);
            }

            _GroupUserService.RemoveUser(user, group);

            return Ok();
        }
    }

}
