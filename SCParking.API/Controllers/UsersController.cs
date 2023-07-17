using SCParking.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCParking.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly ILogger _logger;

        public UsersController(IUserService UserService, ILogger<UsersController> logger)
        {
            _userService = UserService;
            _logger = logger;
        }

//        [HttpPost]
       /* public async Task<IActionResult> Post([FromBody] UserRequestPostDto user)
        {
           
            try
            {
                if (User != null)
                {
                    /*var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                    var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                    var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    user.currentAccountId = currentAccountId;
                    user.currentRoleId = currentRoleId;
                    user.currentUserId = currentUserId;
                    user.createdBy = currentUserId;          
                    */

                  /*  var userCreated = await _userService.Insert(user);
                    return StatusCode(201, new ResponseDataDto { data = userCreated } );
                }
                return StatusCode(401);
               

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Post] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Post] - " + ex.Message);
                return Problem();
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserFilterDto userfilter)
        {            
            var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
            var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            userfilter.currentAccountId = currentAccountId;
            userfilter.currentRoleId = currentRoleId;
            userfilter.currentUserId = currentUserId;

            var users = await _userService.GetUsersByFilter(userfilter);
              var meta = new
              {
                  currentPage = users.CurrentPage,
                  totalPages = users.TotalPages,
                  totalElements = users.TotalCount,
                  pageSize = users.PageSize
              };
              return Ok(new { data = users, meta });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {          
            
            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
               
                var user = await _userService.GetById(id, currentAccountId, currentRoleId);
                return Ok(new ResponseDataDto { data = user });
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[GetById] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[GetById] - " + ex.Message);
                return Problem();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                var record = await _userService.Delete(id, currentAccountId, currentRoleId);
                return StatusCode(204);
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Delete] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Delete] - " + ex.Message);
                return Problem();
            }
           
        }

        
        [HttpGet, Route("by-role/{role_id}")]
        public async Task<IActionResult> GetByRole(Guid role_id, [FromQuery] RoleFilterDto rolefilter)
        {

            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                rolefilter.currentAccountId = currentAccountId;
                rolefilter.currentRoleId = currentRoleId;
                rolefilter.currentUserId = currentUserId;
                var users = await _userService.GetUsersByRoleFilter(role_id, rolefilter);
                var meta = new
                {
                    currentPage = users.CurrentPage,
                    totalPages = users.TotalPages,
                    totalElements = users.TotalCount,
                    pageSize = users.PageSize
                };
                return Ok(new { data = users, meta });
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[GetByRole] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[GetByRole] - " + ex.Message);
                return Problem();
            }

        }

        [HttpGet, Route("assign-to-campaign")]
        public async Task<IActionResult> GetUsersToAssign([FromQuery] UserFilterDto userfilter)
        {

            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                userfilter.currentAccountId = currentAccountId;
                userfilter.currentRoleId = currentRoleId;
                userfilter.currentUserId = currentUserId;
                var users = await _userService.GetUsersToAssign(userfilter);
                var meta = new
                {
                    currentPage = users.CurrentPage,
                    totalPages = users.TotalPages,
                    totalElements = users.TotalCount,
                    pageSize = users.PageSize
                };
                return Ok(new { data = users, meta });
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[GetUsersToAssign] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[GetUsersToAssign] - " + ex.Message);
                return Problem();
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserRequestPutDto user)
        {
            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                user.currentAccountId = currentAccountId;
                user.currentRoleId = currentRoleId;
                user.currentUserId = currentUserId;

                user.id = id;
                user.modifiedBy = currentUserId;
                var userUpdated = await _userService.Update(user, currentAccountId, currentRoleId);
                return StatusCode(204);
               

            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Put] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Put] - " + ex.Message);
                return Problem();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UserRequestPutDto user)
        {
            try
            {
                var currentAccountId = Guid.Parse(User.FindFirst(ClaimTypes.UserData).Value);
                var currentRoleId = Guid.Parse(User.FindFirst(ClaimTypes.GroupSid).Value);
                var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                user.currentAccountId = currentAccountId;
                user.currentRoleId = currentRoleId;
                user.currentUserId = currentUserId;

                user.id = id;
                user.modifiedBy = currentUserId;
                var userUpdated = await _userService.Patch(user, currentAccountId, currentRoleId);
                return StatusCode(204);


            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Patch] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Patch] - " + ex.Message);
                return Problem();
            }
        }*/



    }
}
