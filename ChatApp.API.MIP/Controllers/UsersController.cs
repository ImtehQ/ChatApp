using ChatApp.Business.Core.Authentication;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Interfaces.Services;
using System.Net.Http;
using System.Net;
using ChatApp.Domain.Models;
using System.Collections.Generic;
using ChatApp.Business.Core.Extensions;
using ChatApp.Business.Core.Cryptography;
using System;
using RandomNameGeneratorLibrary;

namespace ChatApp.API.MIP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        IUserService _UserService { get; set; }
        IGroupService _GroupService { get; set; }
        IGroupUserService _GroupUserService { get; set; }

        IJWTAuthService _JWTAuthService { get; set; }

        public UsersController(IUserService UserService, IJWTAuthService jWTAuthService, IGroupUserService GroupUserService)
        {
            _UserService = UserService;
            _JWTAuthService = jWTAuthService;
            _GroupUserService = GroupUserService;
        }

        [HttpGet]
        [Route("/api/list")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult List(GroupTypeEnum groupType)
        {
            User user = _UserService.GetUserById(HttpContext.User.GetUserID());
            IResponse _result = _GroupUserService.GetAllUsersByGroupType(user, groupType);

            return StatusCode((int)_result.Code, _result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Username))
                return BadRequest("IsNullOrEmpty");

            IResponse _result = _UserService.Login(Username, Password);

            return StatusCode((int)_result.Code, _result);

        }


        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult Register(string Name, string Username, string Emailaddress, string Password)
        {

            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return BadRequest("IsNullOrEmpty");
            }

            IResponse _result = _UserService.Register(Name, Username, Emailaddress, Password);

            return StatusCode((int)_result.Code, _result);
        }

        [HttpPost]
        public void AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {

        }





        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public void Block(int userId)
        {
            //return _UserService.BlockUserById();
        }
    }
}
