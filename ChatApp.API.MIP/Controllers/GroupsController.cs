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

namespace ChatApp.API.MIP.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        IGroupService _GroupService { get; set; }
        IUserService _UserService { get; set; }
        IGroupUserService _GroupUserService { get; set; }

        public GroupsController(IGroupService GroupService, IUserService userService, IGroupUserService groupUserService)
        {
            _GroupService = GroupService;
            _UserService = userService;
            _GroupUserService = groupUserService;
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
        public void Join(int GroupId, int UserId, string Message)
        {
            
        }

        [HttpPost]
        [Route("groups/remove")]
        public void Remove(int UserId, int GroupId)
        {
              
        }
    }

}
