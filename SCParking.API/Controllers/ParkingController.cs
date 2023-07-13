using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SCParking.Core.Interfaces;
using SCParking.Domain.Validation;
using SCParking.Domain.Views.DTOs;

namespace SCParking.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ParkingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IParkingService _parkingService;
        private readonly IParkingReservationService _parkingReservation;
        public ParkingController(ILogger<ParkingController> logger, IParkingService parkingService,IParkingReservationService parkingReservation)
        {
            _parkingService = parkingService;
            _logger = logger;
            _parkingReservation = parkingReservation;
        }        

        [HttpGet("parkingSlots")]
        public async Task<IActionResult> Get([FromQuery] string status, [FromQuery] string placeType)
        {
            var response = await _parkingService.GetbyStatus(status,placeType);
            return Ok(new {data=response});
        }
        [HttpGet("parkingSlots/{id}")]
        public async Task<IActionResult> GetbyId(string id)
        {
            var response = await _parkingService.GetbyId(id);
            return Ok(new { data = response });
        }
        

        [HttpPost("parkingSlots/{id}/reserve")]
        public async Task<IActionResult> Reserve(string id,[FromBody]ReservationParkingDto reservationParking)
        {

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                await _parkingService.Reserve(id, reservationParking);
                return Ok();
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[Get{id}] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[Get{id}] - " + ex.Message);
                return Problem();
            }
            
        }

        [HttpPut("parkingSlots/{id}/changeStatus")]
        public async Task<IActionResult> ChangeStatus(string id, [FromBody] ParkingSlotStatusChangeDto parkingSlotStatusChange)
        {

            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _parkingService.ChangeStatus(id, parkingSlotStatusChange);
            return Ok();
        }
    }
}
