using FlappyC_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        public async Task<ActionResult> Register(RegisteDTO register)
        {
            if(register.Password != register.PasswordConfirm)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                          new { Message = "Les deux mots de passe spécifiés sont différents." });
            }

            User user = new User()
            {
                UserName = register.Username,
                Email = register.Email

            };
            IdentityResult identityResult = await this.UserManager.CreateAsync(user, register.Password);
            if (!identityResult.Succeeded) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "La création de l'utilisateur a échouer" });
            }
            return Ok();
        }
        }
 }
