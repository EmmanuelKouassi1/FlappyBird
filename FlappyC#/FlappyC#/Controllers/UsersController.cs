using FlappyC_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlappyC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserManager<User> UserManager;
        public UsersController(UserManager<User> userManager)
        {
            this.UserManager = UserManager;
        }

        public ActionResult Register()
        {
            return Ok();
        }
    }
}
