using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Geolocation.Domain.Interfaces;
using Geolocation.Infrastructure.Entities;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IGeolocationContext _context;

        private readonly ILogger _logger;

        public AddressesController(ILogger<AddressesController> logger, IGeolocationContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Get();
            return Ok(result);
        }

        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Insert(AddressEntity address)
        {
            await _context.Insert(address);
            return Ok();
        }
    }
}
