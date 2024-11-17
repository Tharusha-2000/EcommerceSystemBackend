using Ecommerce.userManage.Domain.Models.DTO;
using Ecommerce.userManage.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;

namespace Ecommerce.userManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<IdentityUser> userManager;
        public readonly ITokenRepository tokenRepository;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.configuration=configuration;

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




        [HttpPost]
        [Route("forgot-password")]

        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            try {
                var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);  //[userManage].[dbo].[AspNetUsers]

                if (user == null)
                {
                    return Ok(new { Message = "If your email is registerd, you will receive a password reset link" });
                }

                // Generate password reset token
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                // Encode token as it may contain special characters
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                // Create reset link
                var resetLink = $"{configuration["AppSettings:ClientBaseUrl"]}/reset-password?email={HttpUtility.UrlEncode(user.Email)}&token={encodedToken}";

                // Send email
                 await SendPasswordResetEmail(user.Email, resetLink);

                return Ok(new { Message = "If your email is registered, you will receive a password reset link." });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = " An error occurred while processing your request." });
            }
               


        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    return BadRequest(new { Message = "Invalid request." });
                }
                //if (!IsBase64String(resetPasswordDto.Token))
                //{
                //    return BadRequest(new { Message = "Invalid token format." });
                //}
                //var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));

                // Decode token
                var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDto.Token));

                // Reset password
                var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPasswordDto.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Password has been reset successfully." });
                }

                return BadRequest(new { Message = "Error resetting password.", Errors = result.Errors });
            }
            catch (Exception ex)
            {
                // Log the exception details here
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }


        private async Task SendPasswordResetEmail(string email, string resetLink)
        {
            var smtpServer = configuration["SmtpSettings:Server"];
            var smtpPort = int.Parse(configuration["SmtpSettings:Port"]);
            var smtpUsername = configuration["SmtpSettings:Username"];
            var smtpPassword = configuration["SmtpSettings:Password"];
            var senderEmail = configuration["SmtpSettings:SenderEmail"];

            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Password Reset Request",
                    Body = $@"
                        <h2>Password Reset Request</h2>
                        <p>You have requested to reset your password. Please click the link below to reset your password:</p>
                        <p><a href='{resetLink}'>Reset Password</a></p>
                        <p>If you didn't request this, please ignore this email.</p>
                        <p>This link will expire in 24 hours.</p>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }

        
    }

}

