using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Business.Core.Extensions;
using ChatApp.Domain.Interfaces.EchoResponse;
using ChatApp.Business.Core.EchoResponse.Extensions;
using ChatApp.Business.Core.EchoResponse.Extensions.Summary;
using FluentResponses.Interfaces;
using FluentResponses.Extensions;
using FluentResponses.Extensions.Summary;

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
            IResponse response = this.CreateResponse().Include(_appService.Login(Username, Password));

            return StatusCode((int)response.Code, response.SumResultCodes());
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
