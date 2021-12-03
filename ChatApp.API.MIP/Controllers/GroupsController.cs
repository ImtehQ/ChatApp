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
using FluentResponses.TraceExtensions;
using ChatApp.API.MIP.HttpContextExtensions;

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
        public IActionResult ListGroups(int groupId)
        {
            IResponse response = this.CreateResponse();
            response
                .Includes(_appService.ListGroups(groupId, HttpContext.GetUser()))
                .LastIncluded();
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [Route("groups")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult RegisterGroup(string Name, string Password, int MaxUsers = 0,
            GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic,
            GroupTypeEnum GroupType = GroupTypeEnum.OptionGroup)
        {
            IResponse response = this.CreateResponse();

            response.Includes(_appService.RegisterGroup(HttpContext.GetUser(), Name, Password, MaxUsers, Visibility, GroupType));

            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }


        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult InviteGroup(int inviteId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.InviteGroup(HttpContext.GetUser(), inviteId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [Route("groups/join")]
        [Authorize(AccountRoleEnum.RoleUser)] // Missing group id so no idea where you need to be rolemod for...
        public IActionResult JoinGroup(int GroupId, int UserId, string Message)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.JoinGroup(HttpContext.GetUser(), GroupId, UserId, Message));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [Route("groups/remove")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult RemoveGroup(int GroupId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.RemoveGroup(GroupId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [Route("groups/remove")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult RemoveUserFromGroup(int userId, int GroupId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.RemoveUserFromGroup(userId, GroupId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }
    }

}
