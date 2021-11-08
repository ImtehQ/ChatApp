using ChapApp.Domain.Interfaces;
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
    [Route("[messages]")]
    public class MessagesController : ControllerBase
    {
        IDataService DataService { get; set; }

        [HttpPost]
        [Route("messages")]
        public void Register(string Message, int Sender, int Type, int TypeId)
        {

        }

        [HttpPost]
        [Route("messages/(?page={int}&type={n}&id=n)")]
        public void Register(int nl, int n, int N)
        {
            //TODO: WTF?!
        }
    }
}
