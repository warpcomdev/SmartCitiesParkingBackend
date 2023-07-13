using System;
using System.Threading.Tasks;
using SCParking.Core.Interfaces;
using SCParking.Domain.Validation;
using SCParking.Domain.Views.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCParking.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger _logger;
        public static IWebHostEnvironment _environment;

        public AuthenticationController(IUserService UserService, ILogger<UsersController> logger, IWebHostEnvironment environment)
        {
            _userService = UserService;
            _logger = logger;
            _environment = environment;
        }       
           
        [HttpPost("login")]
        [AllowAnonymous]          
        public async Task<IActionResult> Login([FromBody] AuthenticationDto request)
        {
            try
            {              
               
                var token = await _userService.Authenticate(request.email, request.password);
                return Ok(new TokenDto
                {
                    token = token.AccessToken,
                    //  RefreshToken = token.RefreshToken.TokenString
                });              
               
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Login] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Login] - " + ex.Message);
                return Problem();
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            var userName = User.Identity.Name;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
           // var token = await _userService.AddTokenBlackList(accessToken);               
            return Ok();

            /*try
            {
                IActionResult response = Unauthorized();
                string tokenString = string.Empty;
                if (string.IsNullOrEmpty(user.Name)
                    || string.IsNullOrEmpty(user.FirstName)
                    || string.IsNullOrEmpty(user.Email)                    
                    )
                    return BadRequest();


                
                var userCreated = await _userService.Insert(user);                
                return Ok(userCreated);

            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Post] - " + ex.Message);
                return Problem();

            }*/
        }

        /*[HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request)
        {
            try
            {
                await _userService.ForgotPassword(request, Request.Headers["origin"]);
                return Ok();
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[ForgotPassword] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[ForgotPassword] - " + ex.Message);
                return Problem();
            }
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            try
            {
                
                await _userService.ResetPassword(request);
                return Ok();
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[ResetPassword] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[ResetPassword] - " + ex.Message);
                return Problem();
            }
           
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(TokenDto request)
        {
            try
            {

                await _userService.UpdateProfileEmailByToken(request.token);
                return Ok();
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[ConfirmEmail] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[ConfirmEmail] - " + ex.Message);
                return Problem();
            }

        }


        [HttpGet, Route("me")]
        public async Task<IActionResult> CurrentProfile()
        {
             if(User!=null)
              {

                var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userName = User.FindFirst(ClaimTypes.Name).Value;
                var role = User.FindFirst(ClaimTypes.Role).Value;
                var profile = await _userService.GetProfile(Guid.Parse(id));
                profile.role = role;               
                return Ok(new ResponseDataDto { data=profile });
              }
            return StatusCode(401);
        }


        [HttpPut, Route("me")]
        public async Task<IActionResult> UpdateProfile([FromForm] CurrentSessionPutDto request)
        {
            try
            {
                if (User != null) 
                {
                    var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userName = User.FindFirst(ClaimTypes.Name).Value;
                    var role = User.FindFirst(ClaimTypes.Role).Value;
                    var userModel = new User();
                    userModel.Id = Guid.Parse(id);
                    userModel.FirstName = request.firstName;
                    userModel.LastName = request.lastName;
                    userModel.LastName = request.lastName;
                    userModel.Phone = request.phone;
                    userModel.SecondPhone = request.secondPhone;
                    userModel.AvatarUrl = string.Empty;

                    await _userService.UpdateProfile(userModel, request.avatar);
                    return StatusCode(204);
                }
                return StatusCode(401);

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                return Problem();
            }
        }


        [HttpPatch, Route("me")]
        public async Task<IActionResult> UpdateProfilePatch([FromForm]  CurrentSessionPutDto request)
        {
            try
            {
                if (User != null)
                {
                    var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userName = User.FindFirst(ClaimTypes.Name).Value;
                    var role = User.FindFirst(ClaimTypes.Role).Value;
                    var userModel = new User();
                    userModel.Id = Guid.Parse(id);
                    userModel.FirstName = request.firstName;
                    userModel.LastName = request.lastName;
                    userModel.LastName = request.lastName;
                    userModel.Phone = request.phone;
                    userModel.SecondPhone = request.secondPhone;
                    userModel.AvatarUrl = string.Empty;

                    await _userService.UpdateProfilePatch(userModel, request.avatar);
                    return StatusCode(204);
                }
                return StatusCode(401);

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfile] - " + ex.Message);
                return Problem();
            }
        }

        [HttpPut, Route("me/email")]
        public async Task<IActionResult> UpdateProfileEmail([FromBody] CurrentSessionEmailPutDto request)
        {
            try
            {
                if (User != null)
                {
                    var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userName = User.FindFirst(ClaimTypes.Name).Value;
                    var role = User.FindFirst(ClaimTypes.Role).Value;
                    var userModel = new User();
                    userModel.Id = Guid.Parse(id);
                    userModel.Email = request.email;
                    userModel.Password = request.password;              

                    await _userService.UpdateProfileEmail(userModel);
                    return StatusCode(204);
                }
                return StatusCode(401);
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfileEmail] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfileEmail] - " + ex.Message);
                return Problem();
            }
        }

        [HttpPut, Route("me/password")]
        public async Task<IActionResult> UpdateProfilePassword([FromBody] ChangePasswordDto request)
        {
            try
            {
                if (User != null)
                {
                    var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var userName = User.FindFirst(ClaimTypes.Name).Value;
                    var role = User.FindFirst(ClaimTypes.Role).Value;              
                    await _userService.ChangePassword(Guid.Parse(id), request);               
                    return StatusCode(204);
                }
                return StatusCode(401);
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[UpdateProfilePassword] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[UpdateProfilePassword] - " + ex.Message);
                return Problem();
            }
        }*/


    }
}
