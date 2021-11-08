using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[groups]")]
    public class GroupsController : ControllerBase
    {
        IDataService DataService { get; set; }

        [HttpPost]
        [Route("groups")]
        public void Register(string Name, string Password, int MaxUsers = 0, GroupVisibilityEnum Visibility = GroupVisibilityEnum.OptionPublic)
        {
            
        }

        [HttpPost]
        [Route("groups/join")]
        public void Join(int GroupId, int UserId, string Message)
        {
            
        }

        [HttpPost]
        [Route("groups/remove")]
        public void Remove(int UserId, int GroupId)
        {
              
        }
    }

}
