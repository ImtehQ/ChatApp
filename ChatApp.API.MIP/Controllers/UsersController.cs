using ChatApp.API.MIP.HttpContextExtensions;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using FluentResponses.Extensions.MarkExtentions;

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


            response.Include(_appService.ListUsers(HttpContext.GetUser(), groupType));


            response.Successfull();
            return StatusCode((int)response.GetStatusCode(), response.ReportMessage());
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult LoginUser(string Username, string Password)
        {
            IResponse response = this.CreateResponse().
                Include(_appService.LoginUser(Username, Password));
            response.Successfull();
            return StatusCode((int)response.GetStatusCode(), response.ReportMessage());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult RegisterUser(string Name, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse();
            response.Include(
                _appService.RegisterUser(Name, Username, Emailaddress, Password));
            response.Successfull();
            return StatusCode((int)response.GetStatusCode(), response.ReportMessage());
        }

        [HttpPut]
        public IActionResult AccountUpdateUser(int id, string Username, string Emailaddress, string Password)
        {
            IResponse response = this.CreateResponse().
                Include(_appService.AccountUpdateUser(id, Username, Emailaddress, Password));
            response.Successfull();
            return StatusCode((int)response.GetStatusCode(), response.ReportMessage());
        }

        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public IActionResult BlockUser(int userId)
        {
            IResponse response = this.CreateResponse().
                Include(_appService.BlockUser(userId));
            response.Successfull();
            return StatusCode((int)response.GetStatusCode(), response.ReportMessage());
        }
    }
}
