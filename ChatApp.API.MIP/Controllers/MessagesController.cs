using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        IMessageService _MessageService { get; set; }

        public MessagesController(IMessageService MessageService)
        {
            _MessageService = MessageService;
        }

        [HttpPost]
        [Route("messages")]
        public void Register(string Message, int Sender, int Type, int TypeId)
        {

        }

        [HttpPost]
        [Route("messages/page")]
        //[Route("messages/(?page={int}&type={n}&id=n)")]
        public void Register(int nl, int n, int N)
        {
            //TODO: WTF?!
        }
    }
}
