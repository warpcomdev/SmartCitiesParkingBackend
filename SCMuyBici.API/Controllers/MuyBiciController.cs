using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SCParking.Core.Interfaces;
using System.Threading.Tasks;

namespace SCMuyBici.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class MuyBiciController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMuyBiciService _muyBiciService;
        public MuyBiciController(ILogger<MuyBiciController> logger,IMuyBiciService muyBiciService)
        {
            _logger = logger;
            _muyBiciService = muyBiciService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _muyBiciService.SendEntities();
            return Ok();
        }
    }
}
