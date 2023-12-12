using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
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

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);

            if (user == null)
            {
                return BadRequest("Username Incorrect");
            }
            else
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkPasswordResult)
                {
                    // Get roles for user
                    var userRoles = await userManager.GetRolesAsync(user);

                    // Create Token
                    if(userRoles != null && userRoles.Any())
                    {
                        var token = tokenRepository.CreateJwtToken(user, userRoles.ToList());

                        if (token != null)
                        {
                            var responseDto = new LoginResponseDto()
                            {
                                JwtToken = token,
                                Message = "You Can use this token for login it is valid for 15 min."
                            };

                            return Ok(responseDto);
                        }
                        else
                        {
                            return BadRequest("Token not generated for user");
                        }
                    }
                    else
                    {
                        return BadRequest("User dont have any roles");
                    }
                }
                else
                {
                    return BadRequest("Password is incorrect");
                }
            }
        }
    }
}
