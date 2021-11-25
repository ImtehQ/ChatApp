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
        IAppService _appService;

        public UsersController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        [Route("/api/list")]
        [Authorize(AccountRoleEnum.RoleUser)]
        public IActionResult List(GroupTypeEnum groupType)
        {
            IResponse response = _appService.List(HttpContext.User.GetUserID(), groupType);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult Login(string Username, string Password)
        {
            IResponse response =  _appService.Login(Username, Password);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult Register(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = _appService.Register(Name, Username, Emailaddress, Password);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut]
        public IActionResult AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = _appService.AccountUpdate(id, Username, Emailaddress, Password);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public IActionResult Block(int userId)
        {
            IResponse response = _appService.BlockUser(userId);
            return StatusCode((int)response.Code, response);
        }
    }
}
