using ChapApp.Business.Domain.Extensions;
using ChatApp.Business.Core.Authentication;
using ChatApp.Domain.Attributes;
using ChatApp.Domain.Enums;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.API.MIP.Controllers
{
    [Authorize]
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

            return Ok(_UserService.Login(Username, Password));
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

            return Ok(_UserService.Register(Name, Username, Emailaddress, Password));
        }

        [HttpPost]
        public void AccountUpdate(int id, string Username, string Emailaddress, string Password)
        {

        }



        [HttpGet]
        [Route("list")]
        [Auth(AccountRoleEnum.RoleUser)]
        [Auth(AccountRoleEnum.RoleModerator)]
        [Auth(AccountRoleEnum.RoleUser)]
        public IActionResult List()
        {
            this.
            AuthAttribute.IsAuthenticated = false;
            var currentUser = HttpContext.User;
            if (currentUser.HasClaim(c => c.Type == "UserId"))
            {
                int currentUserId = System.Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                return Ok(currentUserId);
            }
            return Ok("Meh");
        }

        [HttpGet]
        [Route("block/{userId}")]
        public void Block(int userId)
        {

        }
    }
}
