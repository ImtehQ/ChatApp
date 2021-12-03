using ChatApp.API.MIP.HttpContextExtensions;
using ChatApp.Business.Core.AppServices;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces.Services;
using FluentResponses.Extensions.Initializers;
using FluentResponses.Extensions.Reports;
using FluentResponses.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        IAppService _appService;

        public MessagesController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        [Route("messages")]
        public IActionResult SendMessage(string Message, int Sender, int Type, int TypeId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.SendMessage(Message, HttpContext.GetUser(), Type, TypeId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }

        [HttpGet]
        [Route("messages")]
        public IActionResult PullMessages(int pageNr, GroupTypeEnum groupType, int groupId)
        {
            IResponse response = this.CreateResponse();
            response.Includes(_appService.PullMessages(pageNr, groupId));
            return StatusCode((int)response.Code(), response.ReportFullDetails());
        }
    }
}
