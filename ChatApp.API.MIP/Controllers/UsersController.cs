using ChatApp.Business.Core.Authentication;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AuthorizeAttribute = ChatApp.Business.Core.Authentication.AuthorizeAttribute;
using ChatApp.Domain.Interfaces.Services;
namespace ChatApp.API.MIP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        IUserService _UserService { get; set; }
        IJWTAuthService _JWTAuthService { get; set; }

        public UsersController(IUserService UserService, IJWTAuthService jWTAuthService)
        {
            _UserService = UserService;
            _JWTAuthService = jWTAuthService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("token/")]
        public IActionResult Login(string Username, string Password)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Username))
                return Ok("");

            return Ok(_UserService.Login(Username, Password).ToCodes());
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("reg/")]
        public IActionResult Register(string Name, string Username, string Emailaddress, string Password)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Emailaddress))
            {
                return Ok("IsNullOrEmpty");
            }

            return Ok(_UserService.Register(Name, Username, Emailaddress, Password).ToCodes());
        }

        [HttpPost]
        public void AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {

        }





        [HttpGet]
        [Route("block/{userId}")]
        [Authorize(AccountRoleEnum.RoleModerator)]
        public void Block(int userId)
        {

        }
    }
}
