using ChatApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        IMessageService _MessageService { get; set; }
        IUserService _UserService { get; set; }

        public MessagesController(IMessageService MessageService, IUserService userService)
        {
            _MessageService = MessageService;
            _UserService = userService;
        }

        //[HttpPost]
        //[Route("messages")]
        //public IActionResult SendMessage(string Message, int Sender, int Type, int TypeId)
        //{
        //    if (string.IsNullOrEmpty(Message) || Sender < 0 || Type < 0 || TypeId < 0)
        //    {
        //        return BadRequest("IsNullOrEmpty");
        //    }

        //    User user = _UserService.GetUserById(Sender);
        //    if(user == null)
        //    {
        //        return BadRequest("IsNullOrEmpty");
        //    }

        //    IResponse _result = _MessageService.SendMessage(Message, user, (GroupTypeEnum)Type, TypeId);

        //    return StatusCode((int)_result.Code, _result);
        //}

        //[HttpGet]
        //[Route("messages")]
        //public IActionResult PullMessages(int pageNr, GroupTypeEnum groupType, int groupId)
        //{
        //    if (groupId < 0)
        //    {
        //        return BadRequest("IsNullOrEmpty");
        //    }

        //    IResponse _result = _MessageService.GetAllMessages(groupId, pageNr);

        //    return StatusCode((int)_result.Code, _result);


        //}
    }
}
