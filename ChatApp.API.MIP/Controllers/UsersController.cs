using ChapApp.Business.Domain.Extensions;
using ChapApp.Domain.Interfaces;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        IUserService _UserService  {  get; set; }

        IJwtAuth _JwtAuth { get; set; }

        public UsersController(IUserService UserService, IJwtAuth Auth)
        {
            _UserService = UserService;
            _JwtAuth = Auth;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public string Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Username))
                return "";


            return "Token hier duh";
        }

        [HttpGet]
        public string Register(string Name, string Username, string Emailaddress, string Password)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
                return "poop";
            return "";

        }

        [HttpPost]
        public void AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {

        }



        [HttpGet]
        [Route("list")]
        public void List(string Username, string Password)
        {

        }

        [HttpGet]
        [Route("block/{userId}")]
        public void Block(int userId)
        {

        }
    }
}
