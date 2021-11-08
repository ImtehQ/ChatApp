using ChapApp.Business.Domain.Extensions;
using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[users]")]
    public class UsersController : ControllerBase
    {
        IDataService _DataService {  get; set; }
        IJwtAuth _JwtAuth { get; set; }

        public UsersController(IDataService dataService, IJwtAuth Auth)
        {
            _DataService = dataService;
            _JwtAuth = Auth;
        }

        [HttpPost]
        [Route("users")]
        public void Register(string Name, string Username, string Emailaddress, string Password)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
                return;

            _DataService.Register(Name, Username, Emailaddress, Password);
        }

        [HttpPost]
        [Route("users")]
        public void AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {

        }

        [HttpPost]
        [Route("users/token")]
        public void Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Username))
                return;

            _DataService.Login(Username, Password);
        }

        [HttpGet]
        [Route("users/list")]
        public void List(string Username, string Password)
        {

        }

        [HttpGet]
        [Route("users/users/black/{userId}")]
        public void List(int userId)
        {

        }
    }
}
