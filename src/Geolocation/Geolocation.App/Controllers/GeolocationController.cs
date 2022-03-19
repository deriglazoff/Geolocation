using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeolocationController : ControllerBase
    {


        private readonly ILogger _logger;

        public GeolocationController(ILogger<GeolocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult > Get()
        {
            _logger.LogInformation("start");
            return Ok();
        }
    }
}
