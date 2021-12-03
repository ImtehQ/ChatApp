using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Business.Core.Extensions;
using FluentResponses.Interfaces;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using ChatApp.Domain.Models;
using FluentResponses.TraceExtensions;
using ChatApp.API.MIP.HttpContextExtensions;

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
        public IActionResult ListUsers(GroupTypeEnum groupType)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.ListUsers(HttpContext.GetUser(), groupType));
            response.Successfull();
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult LoginUser(string Username, string Password)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.LoginUser(Username, Password));
            response.Successfull();
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult RegisterUser(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse();
            response.Includes(
                _appService.RegisterUser(Name, Username, Emailaddress, Password));
            response.Successfull();
            return StatusCode((int)response.Code(), response.TraceCodes());
        }

        [HttpPut]
        public IActionResult AccountUpdateUser(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.AccountUpdateUser(id, Username, Emailaddress, Password));
            response.Successfull();
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public IActionResult BlockUser(int userId)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.BlockUser(userId));
            response.Successfull();
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }
    }
}
