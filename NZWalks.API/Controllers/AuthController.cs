using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //Post for registration
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityresult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityresult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityresult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityresult.Succeeded)
                    {
                        return Ok("Succesfully Registered You can now login");
                    }
                }
            }

            return BadRequest("Something Went Wrong");
        }
    }
}
