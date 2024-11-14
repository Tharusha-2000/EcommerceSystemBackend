using Ecommerce.userManage.Domain.Models.DTO;
using Ecommerce.userManage.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.userManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;

        }


        // POST api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestcsDto)
        {
            // Create a new IdentityUser based on the provided username and email
            var identityUser = new IdentityUser
            {
                UserName = registerRequestcsDto.Username,
                Email = registerRequestcsDto.Username
            };
            // Create the user in the database using the UserManager

            var result = await userManager.CreateAsync(identityUser, registerRequestcsDto.Password);
            if (result.Succeeded)
            {
                // If roles are specified, add the user to the first role in the list
                if (registerRequestcsDto.Roles != null && registerRequestcsDto.Roles.Any())
                {
                    result = await userManager.AddToRoleAsync(identityUser, registerRequestcsDto.Roles[0]);

                    // If the user was successfully added to the role, return a success message

                    if (result.Succeeded)
                    {
                        return Ok("User created");
                    }
                }
            }
            // If the user was created or added to a role, return a success message

            return Ok("User created");

        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            // Find the user by their username

            var user = await userManager.FindByNameAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //get roles for this user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //create token
                        var jwtToken = tokenRepository.CreateJWTtoken (user, roles.ToList());

                        var response = new
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }
            // If the login was unsuccessful, return a bad request message
            return BadRequest("Username or password incorrect");








        }
    }
}
