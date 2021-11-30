using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Interfaces.Services;
using ChatApp.Business.Core.Extensions;
using FluentResponses.Interfaces;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;

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
            IResponse response = this.CreateResponse().
                Includes(_appService.List(HttpContext.User.GetUserID(), groupType));

            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult Login(string Username, string Password)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.Login(Username, Password));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult Register(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.Register(Name, Username, Emailaddress, Password));
            response.Successfull();
            return StatusCode((int)response.Code(), response.TraceCodes());
        }

        [HttpPut]
        public IActionResult AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.AccountUpdate(id, Username, Emailaddress, Password));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public IActionResult Block(int userId)
        {
            IResponse response = this.CreateResponse().
                Includes(_appService.BlockUser(userId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }
    }
}
