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
    public class RateController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRateService _rateService;
        public RateController(ILogger<RateController> logger,IRateService rateService)
        {
            _rateService = rateService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RatePostDto entity)
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                return StatusCode(201, new ResponseDataDto { data = await _rateService.Insert(entity, currentUserId) });
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[POST] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status, new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[POST] - " + ex.Message);
                return Problem();
            }

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid Id, [FromBody] RatePostDto entity)
        {
            var currentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //entity.createdBy = currentUserId;
            entity.id = Id.ToString();
            try
            {
                return StatusCode(200, new ResponseDataDto { data = await _rateService.Update(entity, currentUserId) });
            }
            catch (InvalidDynamicCommandException ex)
            {
                _logger.LogError("Method[PUT] - " + ex.Message);
                return StatusCode(ex.ResponseOperation.status,
                    new DataErrorDto { errors = ex.ResponseOperation.errors });
            }
            catch (Exception ex)
            {
                _logger.LogError("Method[PUT] - " + ex.Message);
                return Problem();
            }

            
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string placeType)
        {
            return StatusCode(200, new ResponseDataDto { data = await _rateService.GetByPlaceType(placeType) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return StatusCode(200, new ResponseDataDto { data = await _rateService.GetById(Guid.Parse(id)) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Guid idGuid = Guid.Parse(id);
            await _rateService.Delete(idGuid);
            return StatusCode(200);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAll()
        {
            return StatusCode(200, new ResponseDataDto { data = await _rateService.GetRates() });
        }
    }
}
